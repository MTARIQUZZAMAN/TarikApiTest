using Application.DTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IRefreshTokensRepository
    {
        Task<RefreshTokensModel> GetRefreshTokenByUserId(string userId);
        Task Create(RefreshTokensModel refreshTokensModel);
        void Delete(string id);

    }
}
