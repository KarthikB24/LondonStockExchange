using System;
using System.Collections.Generic;
using System.Text;

namespace LSE.Domain.Entities.Broker
{
    public class Broker
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string BrokerCode { get; private set; }
        public string BrokerName { get; private set; }
        public bool IsActive { get; private set; } = true;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

        private Broker() { }

        public Broker(string code, string name)
        {
            BrokerCode = code;
            BrokerName = name;
        }
    }
}
