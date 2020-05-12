using Messaging;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaControl
{
    public class ConnectionWorker
    {
        private readonly AudioWorker _audioWorker;
        private HubConnectionMaintainer _connectionMaintainer;
        private HubConnection Connection => _connectionMaintainer.Connection;
        public ConnectionWorker(AudioWorker audioWorker)
        {
            _audioWorker = audioWorker;
            _connectionMaintainer = new HubConnectionMaintainer();

            _connectionMaintainer.Connection.On(MessagingConstants.ClientMessages.CHECK_STATE, SendState);

            _connectionMaintainer.Connection.On(MessagingConstants.ClientMessages.PLAY_PAUSE, async () => {
                await OnPlayPause();
            });
            _connectionMaintainer.Connection.On(MessagingConstants.ClientMessages.DEC_VOLUME, async () =>
            {
                await OnTuneVolume(false);
            });
            _connectionMaintainer.Connection.On(MessagingConstants.ClientMessages.INC_VOLUME, async () =>
            {
                await OnTuneVolume(true);
            });
            _connectionMaintainer.Connection.On<double>(MessagingConstants.ClientMessages.SET_VOLUME, (v) =>
            {
                _audioWorker.SetVolume((int)v);
            });

            _connectionMaintainer.Connection.On(MessagingConstants.ClientMessages.NEXT, () =>
            {
                _audioWorker.Next();
            });

            _connectionMaintainer.Connection.On(MessagingConstants.ClientMessages.PREV, () =>
            {
                _audioWorker.Prev();
            });
        }

        private async Task OnTuneVolume(bool inc)
        {
            if (inc)
                _audioWorker.IncreaseVolume();
            else
                _audioWorker.DecreaseVolume();

            await SendState();
        }
        private async Task OnPlayPause()
        {
            _audioWorker.PlayPause();
            await SendState();
        }

        private async Task SendState()
        {
            await Connection.SendAsync(MessagingConstants.ServerMessages.STATE_RECEIVED, new MediaState()
            {
                ConnectionId = Connection.ConnectionId,
                DeviceId = _audioWorker.AudioDeviceName,
                Volume = _audioWorker.Volume
            });
        }
    }
}
