using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Prototype.Core
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetQuerable();

        T GetById(object id);

        T GetSingle(Expression<Func<T, bool>> criteria);

        T GetFirst(Expression<Func<T, bool>> criteria);

        IQueryable<T> GetAll();

        IQueryable<T> Get(Expression<Func<T, bool>> criteria);

        IQueryable<T> Get(Expression<Func<T, object>> orderBy, SortOrder sortOrder);

        IQueryable<T> Get(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy, SortOrder sortOrder);

        IQueryable<T> Get(Expression<Func<T, object>> orderBy, SortOrder sortOrder, int pageIndex, int pageSize);

        IQueryable<T> Get(Expression<Func<T, bool>> criteria, Expression<Func<T, object>> orderBy, SortOrder sortOrder, int pageIndex, int pageSize);

        IEnumerable<T> GetWithRawSql(string query, params object[] parameters);

        int Count();

        int Count(Expression<Func<T, bool>> criteria);

        void Add(T entity);

        void Add(IEnumerable<T> entities);

        void Update(T entity);

        void Delete(T entity);

        void Delete(IEnumerable<T> entities);
    }
}