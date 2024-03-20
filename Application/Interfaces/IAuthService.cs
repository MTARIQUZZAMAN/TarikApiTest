using Application.DTOs;
using Application.Entities;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<JwtToeknVM> Register(RegisterDTO registerDTO);
    }
}
