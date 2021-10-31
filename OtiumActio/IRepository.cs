using OtiumActio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OtiumActio
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(object id);
        void Update(T entity);
        //IQueryable List (Expression<Func<T, bool>> expression);
        IEnumerable<T> GetAll();
        T GetById(object id);
        void Save();
        string AddNewActivity(Activity activity);
        string UpdateActivity(Activity activity);
    }
}
