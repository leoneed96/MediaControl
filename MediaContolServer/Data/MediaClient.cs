using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaContolServer.Data
{
    public class MediaClient
    {
        public string ConnectionId { get; set; }
        public string DeviceName { get; set; }
        public double Volume { get; set; }
        public DateTime LastHeartbeat { get; set; }
    }
}
