using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Application.DTOs.BrokerAuth
{
    public class BrokerLoginResponseDto
    {
        public string BrokerCode { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
    }
}
