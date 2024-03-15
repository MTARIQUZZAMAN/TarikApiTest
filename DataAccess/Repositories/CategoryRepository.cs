using Application.Common.Interface;
using Application.Repositories;
using Dapper;
using DataAccess.Models;
using Domain.Models;
using System.Data;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ISqlDataAccess _db;


        public CategoryRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<CategoryModel>> GetItems()
        {
            var result = (await _db.LoadData<STR_CATEGORY_INFO, dynamic>("select ID,NAME from STR_CATEGORY_INFO", new { }, CommandType.Text))
                    .Select(dto => new CategoryModel
                    {
                        Id = dto.ID,
                        ItemName = dto.NAME,
                    }).ToList();

            return result;

        }

        public async Task<CategoryModel?> GetItem(int id)
        {
            var results = (await _db.LoadData<STR_CATEGORY_INFO, dynamic>("select ID,NAME from STR_CATEGORY_INFO where ID=@ID", new { ID = id }, CommandType.Text))
                    .Select(dto => new CategoryModel
                    {
                        Id = dto.ID,
                        ItemName = dto.NAME,
                    }).ToList();

            return results.FirstOrDefault();

        }


        public async Task<int> InsertItem(CategoryModel itemModel)
        {
            var p = new DynamicParameters();
            p.Add("P_name", itemModel.ItemName);

            var rowsAffected = await _db.SaveData("insert into STR_CATEGORY_INFO (NAME) values(@P_name)", p, CommandType.Text);
            return rowsAffected;
        }

        public async Task<int> UpdateItem(CategoryModel itemModel)
        {
            var p = new DynamicParameters();
            p.Add("P_name", itemModel.ItemName);
            p.Add("ID", itemModel.Id);

            var rowsAffected = await _db.SaveData("update STR_CATEGORY_INFO set NAME=@P_name where ID=@ID", p, CommandType.Text);
            return rowsAffected;
        }

        public async Task<int> DeleteItem(int id)
        {
            var p = new DynamicParameters();
            p.Add("ID", id);

            var rowsAffected = await _db.SaveData("delete STR_CATEGORY_INFO where ID=@ID", p, CommandType.Text);
            return rowsAffected;
        }

    }
}
