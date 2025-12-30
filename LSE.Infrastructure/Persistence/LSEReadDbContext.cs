using LSE.Domain.Entities.Stocks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LSE.Infrastructure.Persistence
{
    public class LSEReadDbContext : DbContext
    {
        public LSEReadDbContext(DbContextOptions<LSEReadDbContext> options) : base(options) { }
        public DbSet<StockSummary> StockSummary => Set<StockSummary>();

    }
}
