using LSE.Domain.Entities.Broker;
using LSE.Domain.Entities.Stock;
using LSE.Domain.Entities.Stocks;
using LSE.Domain.Entities.Trades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Infrastructure.Persistence
{
    public class LSEWriteDbContext : DbContext
    {
        public LSEWriteDbContext(DbContextOptions<LSEWriteDbContext> options)
            : base(options)
        {
        }

        public DbSet<Trade> Trades => Set<Trade>();
        public DbSet<Broker> Brokers => Set<Broker>();
        public DbSet<Stock> Stocks => Set<Stock>();
        public DbSet<StockSummary> StockSummary => Set<StockSummary>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockSummary>(entity =>
            {
                entity.HasKey(x => x.Ticker);
                entity.Property(x => x.AvgPrice)
                      .HasComputedColumnSql("[TotalPrice] / [TradeCount]", stored: true);
            });
        }
    }
}
