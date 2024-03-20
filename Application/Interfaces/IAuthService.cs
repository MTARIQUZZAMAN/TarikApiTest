using Domain.Models;
using Application.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<JwtToeknVM> Register(RegisterDTO registerDTO);
        Task<JwtToeknVM> Login(LoginDTO loginDTO);
    }
}
