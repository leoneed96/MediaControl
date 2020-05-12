using Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaContolServer.Data
{
    public class MediaClientStorage
    {
        public List<MediaClient> Clients { get; } = new List<MediaClient>();
        public void AddClient(MediaClient client) => Clients.Add(client);
        public void RemoveClient(string connectionId)
        {
            var entry = Clients.SingleOrDefault(x => x.ConnectionId == connectionId);
            if (entry != null)
                Clients.Remove(entry);
        }
        public void UpdateOrAddClientState(MediaState state)
        {
            var client = Clients.SingleOrDefault(x => x.ConnectionId == state.ConnectionId);
            if(client != null)
            {
                client.DeviceName = state.DeviceId;
                client.Volume = state.Volume;
                client.LastHeartbeat = DateTime.Now;
            }
            else
            {
                Clients.Add(new MediaClient()
                {
                    DeviceName = state.DeviceId,
                    Volume = state.Volume,
                    LastHeartbeat = DateTime.Now,
                    ConnectionId = state.ConnectionId
                });
            }
        }
    }
}
