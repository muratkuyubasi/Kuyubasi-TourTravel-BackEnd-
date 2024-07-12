﻿using TourV2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TourV2.Data.Dto;

namespace TourV2.Common.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext>
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly ILogger<UnitOfWork<TContext>> _logger;
        private readonly UserInfoToken _userInfoToken;
        public UnitOfWork(
            TContext context,
            ILogger<UnitOfWork<TContext>> logger,
            UserInfoToken userInfoToken)
        {
            _context = context;
            _logger = logger;
            _userInfoToken = userInfoToken;
        }
        public int Save()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    SetModifiedInformation();
                    var retValu = _context.SaveChanges();
                    transaction.Commit();
                    return retValu;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    _logger.LogError(e, e.Message);
                    return 0;
                }
            }
        }
        public async Task<int> SaveAsync()
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    SetModifiedInformation();
                    var val = await _context.SaveChangesAsync();
                    transaction.Commit();
                    return val;
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    _logger.LogError(e, e.Message);
                    return 0;
                }
            }
        }
        public TContext Context => _context;
        public void Dispose()
        {
            _context.Dispose();
        }

        private void SetModifiedInformation()
        {
            foreach (var entry in Context.ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.CreatedBy = Guid.Parse(_userInfoToken.Id);
                    entry.Entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
                    entry.Entity.ModifiedDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity.IsDeleted)
                    {
                        entry.Entity.DeletedBy = Guid.Parse(_userInfoToken.Id);
                        entry.Entity.DeletedDate = DateTime.Now;
                    }
                    else
                    {
                        entry.Entity.ModifiedBy = Guid.Parse(_userInfoToken.Id);
                        entry.Entity.ModifiedDate = DateTime.Now;
                    }
                }
            }
        }
    }
}
