using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AuthService.Interfaces.Service;
using AuthService.Models;
using MongoRepositorys.Repository;
using RepositoryCore.Interfaces;

namespace MongoAuthService.Services
{
    public class MongoRoleService<T> : IRoleRepository<T, string>
        where T :  IdentityRole<string>
    {
        IRepositoryCore<T, string> _repo;

        public MongoRoleService(IRepositoryCore<T, string> repo)
        {
            if(repo is MongoRepository<T>)
            {
                _repo = repo;
            }
            else
            {

            }
        }
        public bool Add(T model)
        {
            _repo.Add(model);
            return true;
        }
       

        public bool Delete(T model)
        {
            _repo.Delete(model);
            return true;
        }

        public T Delete(string id)
        {
          return  _repo.Delete(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
           return  _repo.Find(expression);
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
           return _repo.Find(m => roles.Any(n=>n==m.Id)).ToList();
        }

        public T Update(T model)
        {
            _repo.Update(model);
            return model;
        }

      
    }
}
