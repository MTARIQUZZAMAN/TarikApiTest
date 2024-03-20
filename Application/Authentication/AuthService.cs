using Application.AppSettings;
using Application.DTOs;
using Application.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace Application.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSettings _jwtSettings;


        public AuthService(UserManager<ApplicationUser> userManager, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }


        public async Task<JwtToeknVM> Register(RegisterDTO registerDTO)
        {
            var jwtTokenVM = new JwtToeknVM();
            if (registerDTO == null)
            {
                jwtTokenVM.Error = "Invalid Input";
                return jwtTokenVM;
            }

            var existingUser = await _userManager.FindByNameAsync(registerDTO.UserName);
            if (existingUser != null)
            {
                jwtTokenVM.Error = "User Already Exists";
                return jwtTokenVM;
            }

            var newUser = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                FullName = registerDTO.FullName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

            };


            var identityResult = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (identityResult.Errors.Any())
            {
                jwtTokenVM.Error = string.Join(",", identityResult.Errors);
                return jwtTokenVM;
            }

            if (!identityResult.Succeeded)
            {
                jwtTokenVM.Error = "USer Registration Failed";
                return jwtTokenVM;
            }

            //generate claims
            var claimsIdentity = GenerateClaimsIdentity(newUser);

            //generat ejwt token
            var jwtToken = GenerateAccessToken(claimsIdentity);

            //set these into jwttoken
            jwtTokenVM.JwtToken = jwtToken;
            jwtTokenVM.RefreshToekn = string.Empty;
            jwtTokenVM.Error = string.Empty;

            return jwtTokenVM;
        }


        private static ClaimsIdentity GenerateClaimsIdentity(ApplicationUser newUser)
        {
            var secondsFromUnixEpoch = (DateTime.UtcNow - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds;
            var unixEpochdateStr = ((long)Math.Round(secondsFromUnixEpoch)).ToString();
            var claims = new List<Claim>
            {
                new Claim("userid", newUser.Id),
                new Claim("usernm", newUser.UserName),
                new Claim("email", newUser.Email),
                new Claim("phone", newUser.PhoneNumber),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (!string.IsNullOrEmpty(newUser.FullName))
            {
                claims.Add(new Claim("name", newUser.FullName));
            }

            return new ClaimsIdentity(new GenericIdentity(newUser.UserName, "Token"), claims);
        }

        private string GenerateAccessToken(ClaimsIdentity claimsIdentity)
        {
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claimsIdentity.Claims,
                DateTime.UtcNow,
                DateTime.UtcNow.Add(TimeSpan.FromMinutes(_jwtSettings.AccessTokenExpirationMinutes)),
                new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SingningKey)),
                SecurityAlgorithms.HmacSha512));
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }



    }
}
