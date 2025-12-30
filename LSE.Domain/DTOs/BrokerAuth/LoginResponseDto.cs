using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Domain.DTOs.BrokerAuth
{
    public class LoginResponseDto
    {
        public string BrokerCode { get; set; }
        public string Username { get; set; }
        public string JwtToken { get; set; }
    }
}
