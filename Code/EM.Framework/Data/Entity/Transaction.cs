#region using

using System;
using System.Transactions;
using EM.Framework.Data.Repository;

#endregion

namespace EM.Framework.Data.Entity
{
    /// <summary>
    ///     Entity framework implementation of the transaction.
    /// </summary>
    public class Transaction : ITransaction
    {
        private bool _isDisposed;

        public Transaction(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
            TransactionScope = new TransactionScope();
        }

        protected IUnitOfWork UnitOfWork { get; private set; }

        protected TransactionScope TransactionScope { get; private set; }

        /// <summary>
        ///     Commit unit of work and commits the transaction scope.
        /// </summary>
        public void Commit()
        {
            UnitOfWork.SaveChanges();
            TransactionScope.Complete();
        }

        /// <summary>
        ///     Rolls back transaction.
        ///     Actually the transaction rollback is handled automatically with Dispose method if
        ///     transaction scope was not commited.
        /// </summary>
        public void Rollback()
        {
        }

        ~Transaction()
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
                if (TransactionScope != null)
                {
                    TransactionScope.Dispose();
                    TransactionScope = null;
                    UnitOfWork = null;
                }
            }
            _isDisposed = true;
        }
    }
}