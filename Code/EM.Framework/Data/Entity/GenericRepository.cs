#region using

using System;
using System.Collections.Generic;
using EM.Framework.Data.Repository;

#endregion

namespace EM.Framework.Data.Entity
{
    /// <summary>
    ///     Generic repository wraps given <see cref="IUnitOfWork" /> implementation
    ///     and provides unified access to the entities stored in underlying data storage.
    /// </summary>
    /// <remarks>
    ///     Additionally to <see cref="IUnitOfWork" />, the repository supports
    ///     fluently initialized specifications. See also Read method.
    ///     All commands have to be executed over started unit of work session.
    ///     Flushing of entities to underlying data storage is in competence of
    ///     given unit of work. In other words, synchronization between in-memory repository
    ///     and data storage (e.g. database) is done via unit of work. This way the client
    ///     has complete control over calling data storage and can optimize the way the entities
    ///     are managed.
    /// </remarks>
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="unitOfWork">Unit of work for concrete implementation of data mapper.</param>
        /// <param name="specificationLocator">
        ///     Specification locator resolves implementations of
        ///     <see cref="ISpecification{T}" /> interface. <see cref="ISpecificationLocator" /> is normally
        ///     wrapper over IoC container.
        /// </param>
        public GenericRepository(IUnitOfWork unitOfWork, ISpecificationLocator specificationLocator = null)
        {
            EnsureNotNull(unitOfWork);
            SpecificationLocator = specificationLocator;
            UnitOfWork = unitOfWork;
        }

        /// <summary>
        ///     Checks if given instance is not null. Use the method to validate input parameters.
        /// </summary>
        protected void EnsureNotNull(object o)
        {
            if (o == null)
            {
                throw new ArgumentNullException("o", "Argument can not be null.");
            }
        }

        /// <summary>
        ///     Gets specification locator for the repository to resolve specifications.
        /// </summary>
        protected ISpecificationLocator SpecificationLocator { get; private set; }

        /// <summary>
        ///     Gets unit of work the repository operates on.
        /// </summary>
        protected IUnitOfWork UnitOfWork { get; private set; }

        /// <summary>
        ///     Inserts entity to the repository.
        /// </summary>
        public virtual void Create(TEntity entity)
        {
            UnitOfWork.Create(entity);
        }

        /// <summary>
        ///     Gets specification that allows to filter only requested entities
        ///     from the repository.
        /// </summary>
        /// <typeparam name="TSpecification">
        ///     Concrete specification that will be resolved
        ///     and initialized with underlying unit of work instance. This ensures fluent
        ///     and strongly typed way of connecting repository (uow) and specifications.
        /// </typeparam>
        public virtual TSpecification Read<TSpecification>()
            where TSpecification : class, ISpecification<TEntity>
        {
            return SpecificationLocator.Resolve<TSpecification, TEntity>();
        }

        public virtual TSpecification Read<TSpecification>(TSpecification specification)
            where TSpecification : QueryableSpecification<TEntity>
        {
            specification.Initialize(UnitOfWork);
            return specification;
        }

        /// <summary>
        ///     Updates entity in the repository.
        /// </summary>
        public virtual void Update(TEntity entity)
        {
            UnitOfWork.Update(entity);
        }

        public virtual void Attach(TEntity entity)
        {
            UnitOfWork.Attach(entity);
        }

        /// <summary>
        ///     Deletes entity from the repository.
        /// </summary>
        public virtual void Delete(TEntity entity)
        {
            UnitOfWork.Delete(entity);
        }

        public virtual IList<TEntity> GetAll()
        {
            return UnitOfWork.GetAll<TEntity>();
        }

        public virtual TEntity Find(params object[] keyValues)
        {
            return UnitOfWork.Find<TEntity>(keyValues);
        }

        public virtual TEntity SingleOrDefault(Func<TEntity, bool> predicate)
        {
            return UnitOfWork.SingleOrDefault(predicate);
        }
    }
}