using KiiBlog.Application.Repositories;
using KiiBlog.Application.UnitOfWork;
using KiiBlog.Domain.Entities;
using KiiBlog.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiiBlog.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction _transaction;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        #region [Repository Instances]

        private IRepositories<PLAYER> _player;
        private IRepositories<FLEX> _flex;
        private IRepositories<FLEX_ITEM> _flexItem;

        #endregion [Repository instances]

        #region [Repository Properties]
        public IRepositories<PLAYER> Player
        {
            get { return _player ??= new Repositories<PLAYER>(_context); }
        }

        public IRepositories<FLEX> Flex
        {
            get { return _flex ??= new Repositories<FLEX>(_context); }
        }

        public IRepositories<FLEX_ITEM> FlexItem
        {
            get { return _flexItem ??= new Repositories<FLEX_ITEM>(_context); }
        }
        #endregion [Repository Properties]

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_transaction != null)
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }

            _transaction = await _context.Database.BeginTransactionAsync();
            return _transaction;
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction in progress.");
            }

            try
            {
                await SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("No transaction in progress.");
            }

            try
            {
                await _transaction.RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Log exception here
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
        }

        public async Task ExecuteInTransactionAsync(Func<Task> operation)
        {
            if (_transaction != null)
            {
                // ถ้ามี transaction อยู่แล้ว ใช้ transaction เดิม (Nested Transaction)
                await operation();
                return;
            }

            using var transaction = await BeginTransactionAsync();

            try
            {
                await operation();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                _transaction = null;
            }
        }

        public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation)
        {
            if (_transaction != null)
            {
                return await operation();
            }

            using var transaction = await BeginTransactionAsync();

            try
            {
                var result = await operation();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                _transaction = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context?.Dispose();
        }
    }
}
