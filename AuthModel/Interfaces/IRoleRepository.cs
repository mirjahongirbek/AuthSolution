using AuthModel.Models.Entitys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthModel.Interfaces
{
    public interface IRoleRepository<T, TKey>
        where T : IdentityRole<TKey>
    {
        bool Add(T model);
        bool Delete(T model);
        T Delete(TKey id);
        T Update(T model);
        Task<bool> AddRole(T role, TKey adUserId);
        IEnumerable<T> FindAll();
        T Get(TKey id);
        T GetFirst(Expression<Func<T, bool>> expression);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        List<T> GetList(List<TKey> roles);
        Task<bool> DeleteRole(TKey id, TKey deleteUserId);
        Task<bool> UpdateRole(T model, TKey key);
    }
}
