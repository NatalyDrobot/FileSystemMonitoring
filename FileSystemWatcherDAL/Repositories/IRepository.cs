using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace FileSystemWatcherDAL.Repositories
{
    public interface IRepository<T>
    {
        void Add(T item);
        IEnumerable<T> GetList();
        IEnumerable<T> GetWithFilter(Expression<Func<T, bool>> expresion);
    }
}
