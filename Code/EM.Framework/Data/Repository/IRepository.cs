#region using

using System;
using System.Collections.Generic;
using EM.Framework.Data.Entity;

#endregion

namespace EM.Framework.Data.Repository
{
    /// <summary>
    ///     Generic repository interface (DDD) for reading and writing domain entities to a storage.
    /// </summary>
    /// <typeparam name="TEntity">Domain entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Create/Inserts entity to the storage.
        /// </summary>
        void Create(TEntity entity);

        /// <summary>
        ///     Gets specification interface for complex searching for an entity or entities.
        /// </summary>
        /// <typeparam name="TSpecification">
        ///     Concrete specification that will be resolved
        ///     and initialized with underlying unit of work instance. This ensures fluent
        ///     and strongly typed way of connecting repository (uow) and specifications.
        /// </typeparam>
        TSpecification Read<TSpecification>() where TSpecification : class, ISpecification<TEntity>;

        TSpecification Read<TSpecification>(TSpecification specification)
            where TSpecification : QueryableSpecification<TEntity>;

        /// <summary>
        ///     Updates entity in the storage.
        /// </summary>
        void Update(TEntity entity);

        /// <summary>
        ///     Deletes entity in the storage.
        /// </summary>
        void Delete(TEntity entity);


        /// <summary>
        /// Attaches the entity for discovery
        /// </summary>
        /// <param name="entity"></param>
        void Attach(TEntity entity);

        /// <summary>                 
        /// Gets all entities of the type from the storage.
        /// </summary>
        IList<TEntity> GetAll();

        /// <summary>
        /// Find the entity based on Primary Key.
        /// This search in local storage if not fetch from Database.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="keyValues">The key values.</param>
        /// <returns></returns>
        TEntity Find(params object[] keyValues);

        /// <summary>
        /// Find a single entity based on predicate.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        TEntity SingleOrDefault(Func<TEntity, bool> predicate);
    }
}