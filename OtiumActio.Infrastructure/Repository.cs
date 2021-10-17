using System;
using OtiumActio.Domain.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OtiumActio.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {

        //private readonly DbFactory _dbFactory;
        //private DbSet _dbSet;
        //protected DbSet DbSet
        //{
        //    get => _dbSet ?? (_dbSet = _dbFactory.DbContext.Set());
        //}
        //public Repository(DbFactory dbFactory)
        //{
        //    _dbFactory = dbFactory;
        //}
        private readonly OtiumActioContext _otiumActioContex;
        private readonly DbSet<T> _tablel;
        public Repository(OtiumActioContext otiumActioContext)
        {
            _otiumActioContex = otiumActioContext;
            _tablel = _otiumActioContex.Set<T>();
        }
        public void Add(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            return _tablel.ToList();
        }

        T IRepository<T>.GetById(object id)
        {
            throw new NotImplementedException();
        }

        void IRepository<T>.Save()
        {
            throw new NotImplementedException();
        }
        //public IQueryable List(Expression<Func<T, bool>> expression)
        //{
        //    return DbSet.Where(expression);
        //}
    }
}
