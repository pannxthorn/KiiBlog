using KiiBlog.Application.Repositories;
using KiiBlog.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Application.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        #region [Repository]

        IRepositories<PLAYER> Player { get; }
        IRepositories<FLEX> Flex { get; }
        IRepositories<FLEX_ITEM> FlexItem { get; }

        #endregion [Repository]

        Task SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task ExecuteInTransactionAsync(Func<Task> operation);
        Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation);
    }
}
