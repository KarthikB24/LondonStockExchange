using LSE.Application.DTOs;
using LSE.Application.DTOs.BrokerAuth;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Abstraction.Broker
{
    public interface IBrokerRepository
    {
        Task<bool> Exists(string brokerCode, CancellationToken token);

    } 
}
