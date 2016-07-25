using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using FileSystemWatcherDAL.Entities;

namespace FileSystemWatcherDAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public void Add(T item)
        {
            using (var context = new FileWatcherContext())
            {
                //context.Set<T>().AddOrUpdate(item);
                context.Entry(item).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public IEnumerable<T> GetList()
        {
            using (var context = new FileWatcherContext())
            {
                var events = context.Set<T>().ToList();
                return events;
            }
        }

        public IEnumerable<T> GetWithFilter(Expression<Func<T, bool>> expresion)
        {
            using (var context = new FileWatcherContext())
            {
                return context.Set<T>().Where(expresion).Cast<T>().ToArray<T>();
            }
        }
    }
}
