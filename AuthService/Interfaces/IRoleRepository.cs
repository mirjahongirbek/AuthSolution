using AuthService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthService.Interfaces.Service
{
    public interface IRoleRepository<T, TKey>
        where T: IdentityRole<TKey>
    {
     //   DbSet<T> DbSet { get; }
        bool Add(T model);
        bool Delete(T model);
        T Delete(TKey id);
        T  Update(T model);
        IEnumerable<T> FindAll();
        T Get(TKey id);
        T GetFirst(Expression<Func<T, bool>> expression);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        List<T> GetList(List<TKey> roles);
        //Task<bool> AddRole(T model, TKey UserId);
        //Task<bool> DeleteRole(TKey id, TKey userId);
        //Task<bool> UpdateRole(T role, TKey userId);
    }
}
