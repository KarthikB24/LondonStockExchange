using LSE.Application.DTOs.BrokerAuth;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Abstraction.Broker
{
    public interface IBrokerAuthRepository
    {
        Task<BrokerLoginResponseDto?> ValidateLoginAsync(string username, string password, CancellationToken token);
    }
}
