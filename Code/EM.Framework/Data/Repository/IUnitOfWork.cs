#region using

using System;
using System.Collections.Generic;

#endregion

namespace EM.Framework.Data.Repository
{
    /// <summary>
    ///     The interface represents unit of work pattern implementation.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Saves unsaved entities to be written to the data storage.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        ///     Begins the transaction.
        /// </summary>
        /// <returns></returns>
        ITransaction BeginTransaction();

        /// <summary>
        ///     Ends transaction.
        ///     Note: suggested pattern to manage a transaction is via *using* construct.
        ///     You should set input param to null after calling the method.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <example>
        ///     using ( var tnx = uow.BeginTransaction() ) { /* do some work */ }
        /// </example>
        /// See also
        /// <seealso cref="ITransaction" />
        /// interface for more details.
        void EndTransaction(ref ITransaction transaction);

        /// <summary>                 
        /// Inserts entity to the storage.                 
        /// </summary>                 
        void Create<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>                 
        /// Updates entity in the storage.
        /// </summary>                 
        void Update<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>
        /// Attaches the entity without modifying it
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        void Attach<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>                 
        /// Deletes entity in the storage.                
        /// </summary>                 
        void Delete<TEntity>(TEntity entity) where TEntity : class;

        /// <summary>                 
        /// Gets all entities of the type from the storage.
        /// </summary>
        IList<TEntity> GetAll<TEntity>() where TEntity : class;

        /// <summary>
        /// Find the entity based on Primary Key.
        /// This search in local storage if not fetch from Database.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        TEntity Find<TEntity>(params object[] keyValues) where TEntity : class;

        /// <summary>
        /// Find a single entity based on predicate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        TEntity SingleOrDefault<TEntity>(Func<TEntity, bool> predicate) where TEntity : class;
    }
}