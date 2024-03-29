﻿using Application.Common.Interface;
using Application.Repositories;
using Dapper;
using DataAccess.Models;
using Domain.Models;
using System.Data;

namespace DataAccess.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ISqlDataAccess _db;


        public ItemRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<List<ItemModel>> GetAll()
        {
            var result = (await _db.LoadData<STR_ITEM_INFO, dynamic>("select  STR_ITEM_ID,ITEM_NAME from STR_ITEM_INFO", new { }, CommandType.Text))
                    .Select(dto => new ItemModel
                    {
                        Id = dto.STR_ITEM_ID,
                        ItemName = dto.ITEM_NAME,
                    }).ToList();

            return result;

        }

        public async Task<ItemModel?> GetById(int id)
        {
            var results = (await _db.LoadData<STR_ITEM_INFO, dynamic>("select * from STR_ITEM_INFO where STR_ITEM_ID=@ID", new { ID = id }, CommandType.Text))
                    .Select(dto => new ItemModel
                    {
                        Id = dto.STR_ITEM_ID,
                        ItemName = dto.ITEM_NAME,
                    }).ToList();

            return results.FirstOrDefault();

        }

        public async Task<int> Insert(ItemModel itemModel)
        {

            try
            {
                var p = new DynamicParameters();
                p.Add("P_name", itemModel.ItemName);
                var newid = await _db.InsertSingle("insert into STR_ITEM_INFO (ITEM_NAME) values(@P_name); " +
                    "SELECT CAST(SCOPE_IDENTITY() as int)", p, CommandType.Text);
                return newid;
            }
            catch
            {
                return -1;
            }
        }

        public async Task<int> Update(ItemModel itemModel)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("P_name", itemModel.ItemName);
                p.Add("ID", itemModel.Id);

                var rowsAffected = await _db.SaveData("update STR_ITEM_INFO set ITEM_NAME=@P_name where STR_ITEM_ID=@ID", p, CommandType.Text);
                return rowsAffected;
            }
            catch (Exception)
            {
                return -1;
            }

        }

        public async Task<int> Delete(int id)
        {
            var p = new DynamicParameters();
            p.Add("ID", id);

            var rowsAffected = await _db.SaveData("delete STR_ITEM_INFO where STR_ITEM_ID=@ID", p, CommandType.Text);
            return rowsAffected;
        }

        public async Task<List<ItemModel>> GetByCategoryId(int? cid)
        {
            var result = (await _db.LoadData<STR_ITEM_INFO, dynamic>("select  STR_ITEM_ID,ITEM_NAME from STR_ITEM_INFO where CATEGORY_ID=@CID", new { CID = cid }, CommandType.Text))
            .Select(dto => new ItemModel
            {
                Id = dto.STR_ITEM_ID,
                ItemName = dto.ITEM_NAME,
            }).ToList();

            return result;
        }
    }
}
