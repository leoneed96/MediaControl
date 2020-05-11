using AudioSwitcher.AudioApi.CoreAudio;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaControl
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var audioWorker = new AudioWorker();
            var worker = new ConnectionWorker(audioWorker);
            Console.ReadKey();
            //await connection.InvokeAsync("SendState", new { connection.ConnectionId });
        }
    }
}
