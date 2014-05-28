#region using

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using EM.Framework.Data.Repository;

#endregion

namespace EM.Framework.Data.Entity
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _isDisposed;

        public UnitOfWork(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected DbContext DbContext { get; set; }

        public virtual int SaveChanges()
        {
            return DbContext.SaveChanges();
        }

        public ITransaction BeginTransaction()
        {
            return new Transaction(this);
        }

        public void EndTransaction(ref ITransaction transaction)
        {
            if (transaction != null)
            {
                transaction.Dispose();
                transaction = null;
            }
        }

        public void Create<TEntity>(TEntity entity) where TEntity : class
        {
            var dbSet = DbContext.Set<TEntity>();
            dbSet.Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            if (dbEntityEntry.State == EntityState.Added || dbEntityEntry.State == EntityState.Modified)
            {
                return;
            }

            var dbSet = DbContext.Set<TEntity>();
            if (dbEntityEntry.State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbEntityEntry.State = EntityState.Modified;
        }

        public void Attach<TEntity>(TEntity entity) where TEntity : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            var dbSet = DbContext.Set<TEntity>();
            if (dbEntityEntry.State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : class
        {
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);
            var dbSet = DbContext.Set<TEntity>();

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                dbSet.Attach(entity);
                dbSet.Remove(entity);
            }
        }

        public IQueryable<TEntity> Queryable<TEntity>() where TEntity : class
        {
            var dbSet = DbContext.Set<TEntity>();
            return dbSet;
        }

        public IList<TEntity> GetAll<TEntity>() where TEntity : class
        {
            var dbSet = DbContext.Set<TEntity>();
            return dbSet.ToList();
        }

        public TEntity Find<TEntity>(params object[] keyValues) where TEntity : class
        {
            var dbSet = DbContext.Set<TEntity>();
            return dbSet.Find(keyValues);
        }

        public TEntity SingleOrDefault<TEntity>(Func<TEntity, bool> predicate) where TEntity : class
        {
            var dbSet = DbContext.Set<TEntity>();
            return dbSet.Local.SingleOrDefault(predicate) ?? dbSet.SingleOrDefault(predicate);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed && disposing)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();
                }
            }
            _isDisposed = true;
        }
    }
}