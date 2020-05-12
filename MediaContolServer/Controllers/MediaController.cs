using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediaContolServer.Data;
using Messaging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using static Messaging.MessagingConstants;

namespace MediaContolServer.Controllers
{
    [ApiController]
    [Route("media")]
    public class MediaController : ControllerBase
    {
        private readonly IHubContext<MediaControlHub> _hubContext;
        private readonly MediaClientStorage _storage;
        public MediaController(IHubContext<MediaControlHub> hubContext, MediaClientStorage storage,
            UpdateClientStateWorker worker)
        {
            _hubContext = hubContext;
            _storage = storage;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetConnectedDevices()
        {
            return Ok(_storage.Clients);
        }

        [HttpPost]
        [Route("next/{connectionId}")]
        public async Task<IActionResult> Next(string connectionId)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync(ClientMessages.NEXT);
            return Ok();
        }

        [HttpPost]
        [Route("prev/{connectionId}")]
        public async Task<IActionResult> Prev(string connectionId)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync(ClientMessages.PREV);
            return Ok();
        }

        [HttpPost]
        [Route("playPause/{connectionId}")]
        public async Task<IActionResult> PlayPause(string connectionId)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync(ClientMessages.PLAY_PAUSE);
            return Ok();
        }

        [HttpPost]
        [Route("setVolume/{connectionId}/{value}")]
        public async Task<IActionResult> PlayPause(string connectionId, double value)
        {
            await _hubContext.Clients.Client(connectionId).SendAsync(ClientMessages.SET_VOLUME, value);
            return Ok();
        }


    }
}
