using Application.Common.Interface;
using Application.DTOs;
using Application.Repositories;
using Dapper;
using DataAccess.Models;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class RefreshTokensRepository: IRefreshTokensRepository
    {
        private readonly ISqlDataAccess _db;

        public RefreshTokensRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task Create(RefreshTokensModel model)
        {
            var p = new DynamicParameters();
            p.Add("ID", model.Id);
            p.Add("UID", model.UserId);
            p.Add("IDT", model.IssuedUtc);
            p.Add("EDT", model.ExpiresUtc);

            await _db.SaveData("insert into RefreshTokens(Id,UserId,IssuedUtc,ExpiresUtc) values(@ID,@UID,@IDT,@EDT)", p, CommandType.Text);

        }

        public void Delete(string id)
        {
            var p = new DynamicParameters();
            p.Add("ID", id);
            _db.SaveData("delete RefreshTokens where ID=@ID", p, CommandType.Text);
    
        }

        public async Task<RefreshTokensModel> GetRefreshTokenByUserId(string userId)
        {
            var results = (await _db.LoadData<RefreshTokens, dynamic>("select * from RefreshTokens where UserId=@UserId", new { UserId = userId }, CommandType.Text))
                    .Select(dto => new RefreshTokensModel
                    {
                        Id = dto.Id,
                        UserId = dto.UserId,
                        IssuedUtc = dto.IssuedUtc,
                        ExpiresUtc = dto.ExpiresUtc,
                    }).ToList();

            return results.FirstOrDefault();
        }
    }
}
