using LSE.Application.DTOs.BrokerAuth;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.Queries.Auth
{
    public record BrokerLoginQuery(string UserName, string Password)
       : IRequest<BrokerLoginResponseDto?>; 
}
