using System;
using OtiumActio.Domain.Interfaces;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace OtiumActio.Infrastructure
{
    public class Repository<T> : IRepository<T> where T : class
    {


        private readonly OtiumActioContext _context;
        private readonly DbSet<T> _table;
        public Repository(OtiumActioContext otiumActioContext)
        {
            _context = otiumActioContext;
            _table = _context.Set<T>();
        }
        public void Add(T entity)
        {
            _table.Add(entity);
            Save();
        }

        public void Delete(object id)
        {
            T existing = _table.Find(id);
            _table.Remove(existing);
            Save();

        }

        public void Update(T entity)
        {
            _table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            Save();

        }

        public IEnumerable<T> GetAll()
        {
            return _table.ToList();
        }

        T IRepository<T>.GetById(object id)
        {
            return _table.Find(id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

    }
}
