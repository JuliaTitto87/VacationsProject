﻿using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vacations_DAL.Repository;

namespace Vacations_DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        private IDbContextTransaction _currentTransaction;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            lock (_context)
            {
                if (_currentTransaction != null)
                {
            //        throw new UnitOfWorkAlreadyInTransactionStateException();
                }

                _currentTransaction = _context.Database.BeginTransaction();
            }
            return _currentTransaction;
        }

        IRepository<TEntity> IUnitOfWork.GetRepository<TEntity>()
        {
            return new Repository<TEntity>(_context);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
