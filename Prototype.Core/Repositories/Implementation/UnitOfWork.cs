using Prototype.DatabaseMigration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prototype.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDatabaseContext _dbContext;

        private Dictionary<Type, object> repos;

        private bool disposed = false;

        public UnitOfWork(AppDatabaseContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException("Context was not supplied");
        }

        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            if (repos == null)
            {
                repos = new Dictionary<Type, object>();
            }

            var type = typeof(T);
            if (!repos.ContainsKey(type))
            {
                repos[type] = new GenericRepository<T>(_dbContext);
            }

            return (IGenericRepository<T>)repos[type];
        }

        public int SaveChanges() => _dbContext.SaveChanges();

        public Task<int> SaveChangesAsync() => _dbContext.SaveChangesAsync();

        public bool SaveInTransaction()
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    SaveChanges();
                    transaction.Commit();
                    return true;
                }
                catch
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                    this.disposed = true;
                }
            }
        }
    }
}