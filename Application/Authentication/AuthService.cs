using Application.AppSettings;
using Application.DTOs;
using Application.Entities;
using Application.Interfaces;
using Application.Repositories;
using Domain.Models;
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
        private readonly IRefreshTokensRepository _reposRefreshToken;

        public AuthService(UserManager<ApplicationUser> userManager, 
            IOptions<JwtSettings> jwtSettings,
            IRefreshTokensRepository reposRefreshToken)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _reposRefreshToken = reposRefreshToken;
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

            var refreshToken = await this.GenerateRefreshtoken(newUser.Id);

            //set these into jwttoken
            jwtTokenVM.JwtToken = jwtToken;
            jwtTokenVM.RefreshToekn = refreshToken;
            jwtTokenVM.Error = string.Empty;

            return jwtTokenVM;
        }

        public async Task<JwtToeknVM> Login(LoginDTO loginDTO)
        {
            var jwtTokenVM = new JwtToeknVM();
            if (loginDTO == null)
            {
                jwtTokenVM.Error = "Invalid Input";
                return jwtTokenVM;
            }

            if (string.IsNullOrEmpty(loginDTO.UserName))
            {
                jwtTokenVM.Error = "User Name is required";
                return jwtTokenVM;
            }

            if (string.IsNullOrEmpty(loginDTO.Password))
            {
                jwtTokenVM.Error = "Password Name is required";
                return jwtTokenVM;
            }


            var existingUser = await _userManager.FindByNameAsync(loginDTO.UserName);

            if (existingUser == null)
            {
                jwtTokenVM.Error = "User not Found";
                return jwtTokenVM;
            }

            var lockedOut = await _userManager.IsLockedOutAsync(existingUser);
            if(lockedOut)
            {
                jwtTokenVM.Error = "Allount Locked out";
                return jwtTokenVM;
            }
            var passwordInvalid = await _userManager.CheckPasswordAsync(existingUser, loginDTO.Password);
            if (passwordInvalid)
            {
                await _userManager.AccessFailedAsync(existingUser);
                jwtTokenVM.Error = "Invalid Password";
                return jwtTokenVM;
            }


            //generate claims
            var claimsIdentity = GenerateClaimsIdentity(existingUser);

            //generat ejwt token
            var jwtToken = GenerateAccessToken(claimsIdentity);

            var refreshToken = await this.GenerateRefreshtoken(existingUser.Id);

            //set these into jwttoken
            jwtTokenVM.JwtToken = jwtToken;
            jwtTokenVM.RefreshToekn = refreshToken;
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
                SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public async Task<string> GenerateRefreshtoken(string userId)
        {
            var existingRefreshToken = await _reposRefreshToken.GetRefreshTokenByUserId(userId);

            if (existingRefreshToken != null)
                _reposRefreshToken.Delete(existingRefreshToken.Id);

            var newRefreshToken = new RefreshTokensModel
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userId,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToInt32(_jwtSettings.RefreshTokenExpirationMinutes))
            };

            await _reposRefreshToken.Create(newRefreshToken);
            return newRefreshToken.Id;
        }
    }
}
