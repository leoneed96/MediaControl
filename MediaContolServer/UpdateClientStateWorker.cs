using MediaContolServer.Data;
using Messaging;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaContolServer
{
    public class UpdateClientStateWorker
    {
        private readonly IHubContext<MediaControlHub> _hub;
        private readonly MediaClientStorage _storage;

        public UpdateClientStateWorker(IHubContext<MediaControlHub> hub, MediaClientStorage storage)
        {
            _storage = storage;
            _hub = hub;
            Task.Run(async () =>
            {
                while (true)
                {
                    _storage.Clients.Clear();
                    await _hub.Clients.All.SendAsync(MessagingConstants.ClientMessages.CHECK_STATE);
                    await Task.Delay(5000);
                }
            });
        }
    }
}
