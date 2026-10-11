using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Infrastructure.Caching
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string InstanceName { get; set; } = "ShopApi";
        public int DefaultExpiryMinutes { get; set; } = 30;
    }
}