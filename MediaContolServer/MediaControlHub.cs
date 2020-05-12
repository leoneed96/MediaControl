using MediaContolServer.Data;
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
        private readonly MediaClientStorage _storage;
        public MediaControlHub(MediaClientStorage storage)
        {
            _storage = storage;
        }
        public async Task StateReceived(object data)
        {
            var state = JsonConvert.DeserializeObject<MediaState>(data.ToString());
            _storage.UpdateOrAddClientState(state);
        }
    }
}
