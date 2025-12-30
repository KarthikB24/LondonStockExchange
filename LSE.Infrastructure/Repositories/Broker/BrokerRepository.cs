using LSE.Application.Abstraction.Broker;
using LSE.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Infrastructure.Repositories.Broker
{
    public class BrokerRepository : IBrokerRepository
    {
        private readonly LSEWriteDbContext _db;

        public BrokerRepository(LSEWriteDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Exists(string brokerCode, CancellationToken token)
        {
            return await _db.Brokers
                .AnyAsync(x => x.BrokerCode == brokerCode && x.IsActive, token);
        }
    }
}
