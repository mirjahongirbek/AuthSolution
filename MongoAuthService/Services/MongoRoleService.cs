using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthService.Interfaces.Service;
using MongoAuthService.Models;
using MongoRepositorys.Repository;
using RepositoryCore.Exceptions;
using RepositoryCore.Interfaces;

namespace MongoAuthService.Services
{
    public class MongoRoleService<T> : IRoleRepository<T, string>
        where T : MongoRole
    {
        IRepositoryCore<T, string> _repo;

        public MongoRoleService(IRepositoryCore<T, string> repo)
        {
            _repo = repo;
            //if (repo is MongoRepository<T>)
            //{
            //    _repo = repo;
            //}
            //else
            //{
            //    th
            //}
        }
        public bool Add(T model)
        {
            _repo.Add(model);
            return true;
        }

        public async Task<bool> AddRole(T role, string adUserId)
        {
           var exist= GetFirst(m => m.Name == role.Name && m.TableStatus != RepositoryCore.Enums.Enum.TableStatus.Deleted);
            if(exist!= null)
            {
                throw new CoreException("Role Excist",21);
            }
            role.AddUserId = adUserId;
            Add(role);
            return true;
        }

        public bool Delete(T model)
        {
            _repo.Delete(model);
            return true;
        }

        public T Delete(string id)
        {
            return _repo.Delete(id);
        }

        public async Task<bool> DeleteRole(string id, string deleteUserId)
        {
            _repo.Delete(id);
            return true;            
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _repo.Find(expression);
        }

        public IEnumerable<T> FindAll()
        {
            return _repo.FindAll();
        }

        public T Get(string id)
        {
            return _repo.Get(id);
        }

        public T GetFirst(Expression<Func<T, bool>> expression)
        {
            return _repo.GetFirst(expression);
        }

        public List<T> GetList(List<string> roles)
        {
            return _repo.Find(m => roles.Any(n => n == m.Id)).ToList();
        }
        public T Update(T model)
        {
            _repo.Update(model);
            return model;
        }
        public async Task<bool> UpdateRole(T model, string key)
        {
            _repo.Update(model);
            return true;
        }
    }
}
