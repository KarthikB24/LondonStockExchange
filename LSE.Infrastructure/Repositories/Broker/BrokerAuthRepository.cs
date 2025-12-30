using Dapper;
using LSE.Application.Abstraction.Broker;
using LSE.Application.DTOs.BrokerAuth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace LSE.Infrastructure.Repositories.Broker
{
    public class BrokerAuthRepository : IBrokerAuthRepository
    {
        private readonly IDbConnection _db;

        public BrokerAuthRepository(IDbConnection db)
        {
            _db = db;
        }

        public async Task<BrokerLoginResponseDto?> ValidateLoginAsync(
            string username, string password, CancellationToken token)
        {
            var sql = @"
                SELECT TOP 1 BrokerId, Username, Password
                FROM BrokerUsers
                WHERE Username = @username AND IsActive = 1;
            ";

            var user = await _db.QueryFirstOrDefaultAsync<dynamic>(sql, new { username });

            if (user == null)
                return null;

            bool isValid = password == user.Password; // ONLY FOR TESTING
            if (!isValid)
                return null;

            return new BrokerLoginResponseDto
            {
                BrokerCode= user.BrokerId,
                Username = user.Username
            };
        }
    }
}
