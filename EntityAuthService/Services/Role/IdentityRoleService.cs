
using AuthService.Interfaces.Service;
using AuthService.Models;
using EntityRepository.Context;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthService
{
    //public class IdentityRoleService<TRole, TKey> : IRoleRepository<TRole, TKey>
    //   where TRole : IdentityRole<TKey>

    //{
    //    public DbSet<TRole> DbSet { get => _dbSet; }
    //    private DbSet<TRole> _dbSet;
    //    private DbContext _context;
    //    public IdentityRoleService(IDbContext context)
    //    {
    //        _dbSet = context.DataContext.Set<TRole>();
    //        _context = context.DataContext;
    //    }
    //    private void Save()
    //    {
    //        _context.SaveChanges();
    //    }
    //    public bool Add(TRole model)
    //    {
    //        _dbSet.Add(model);
    //        Save();
    //        return true;
    //    }

    //    public bool Delete(TRole model)
    //    {
    //        _dbSet.Remove(model);
    //        Save();
    //        return true;
    //    }

    //    public TRole Delete(int id)
    //    {
    //        var model = Get(id);
    //        Delete(model);
    //        return model;
    //    }

    //    public IEnumerable<TRole> Find(Expression<Func<TRole, bool>> expression)
    //    {
    //        return _dbSet.Where(expression);
    //    }

    //    public TRole Get(int id)
    //    {
    //        return GetFirst(m => m.Id == id);
    //    }

    //    public TRole GetFirst(Expression<Func<TRole, bool>> expression)
    //    {
    //        return _dbSet.FirstOrDefault(expression);
    //    }

    //    public List<TRole> GetList(List<int> roles)
    //    {
    //        var list = new List<TRole>();
    //        foreach (var i in roles)
    //        {
    //            list.Add(Get(i));
    //        }
    //        return list;
    //    }
    //    public TRole Update(TRole model)
    //    {
    //        _dbSet.Update(model);
    //        return model;
    //    }

    //    public IEnumerable<TRole> FindAll()
    //    {
    //        return _dbSet.Where(m => true);
    //    }
    //    public async Task<bool> UpdateRole(TRole model, int userId)
    //    {
    //       var role= Get(model.Id);
    //        role.AddChanges(userId, "Update Role");
    //        role.Name = model.Name;
    //        role.Actions = JsonConvert.SerializeObject(model.ActionsList);
    //        Update(role);
    //        return true;
    //    }

    //    public async Task<bool> AddRole(TRole model, int userId)
    //    {
    //       var role= _dbSet.FirstOrDefault(m => m.NormalizedName == model.Name.ToLower());
    //        if (role != null) return false;

    //        model.NormalizedName = model.Name.ToLower();
    //        model.AddChanges(userId, "add Role");
    //        Add(model);
    //        return true;

    //    }

    //    public async Task<bool> DeleteRole(int id, int userId)
    //    {
    //       var role= Get(id);
    //        role.TableStatus = RepositoryCore.Enums.Enum.TableStatus.Deleted;
    //        Update(role);
    //        return true;
    //    }
    //}
}
