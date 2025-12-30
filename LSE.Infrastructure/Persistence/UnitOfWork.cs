using LSE.Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LSEWriteDbContext _dbContext;

        public UnitOfWork(LSEWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task SaveChangesAsync(CancellationToken token)
        {
            return _dbContext.SaveChangesAsync(token);
        }
    }
}
