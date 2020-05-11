using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace MediaContolServer.Controllers
{
    [ApiController]
    [Route("media")]
    public class MediaController : ControllerBase
    {
        private readonly IHubContext<MediaControlHub> _hubContext;
        public MediaController(IHubContext<MediaControlHub> hubContext)
        {
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetConnectedDevices()
        {

        }
    }
}
