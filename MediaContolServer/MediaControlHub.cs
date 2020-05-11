using Messaging;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MediaContolServer
{
    public class MediaControlHub: Hub
    {
        public async Task StateReceived(object data)
        {
            var a = data as MediaState;
            var aa = JsonConvert.DeserializeObject<MediaState>(data.ToString());
        }
    }
}
