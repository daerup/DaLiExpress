using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DaLiExpress.Models;

namespace DaLiExpress.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbSet<T> entities;

        public RepositoryBase(DbContext context)
        {
            this.entities = context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return this.entities.ToList();
        }

        public T GetById(int id)
        {
            return this.entities.Find(id);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return this.entities.Where(predicate);
        }

        public void Add(T entity)
        {
            this.entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            this.entities.AddRange(entities);
        }

        public void Remove(T entity)
        {
            this.entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            this.entities.RemoveRange(this.entities);
        }
    }
}