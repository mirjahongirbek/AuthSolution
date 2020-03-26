using AuthService.Interfaces.Service;
using AuthService.Models;
using EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace EntityRepository.Services
{
   public class DeleteDataService<TDeleteData>: IDeleteDataService<TDeleteData>
        where TDeleteData :DeleteData
    {
        public DbSet<TDeleteData> _db;
        private DbContext _context;
        public DeleteDataService(IDbContext context)
        {
            _context = context.DataContext;
            _db = context.DataContext.Set<TDeleteData>();

        }
        protected void Add(TDeleteData model)
        {
            _db.Add(model);
            _context.SaveChanges();
        }
      public  DeleteData AddData(string tableName, int UserId, object data, string schemeName = "")
        {
           var deletedata=(TDeleteData) Activator.CreateInstance(typeof(TDeleteData));
            deletedata.TableName = tableName;
            deletedata.Data = JsonConvert.SerializeObject(data);
            deletedata.DateTime = DateTime.Now;
            deletedata.SchemeName = schemeName;
            deletedata.UserId = UserId;
            Add(deletedata);
            return deletedata;

        }

    }
}
