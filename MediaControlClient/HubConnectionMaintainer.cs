using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MediaControl
{
    public class HubConnectionMaintainer
    {
        private readonly HubConnection _connection;
        
        public HubConnectionMaintainer()
        {
            _connection = new HubConnectionBuilder()
               .WithUrl("http://192.168.1.121:130/media")
               .Build();

            Task.Run(async () =>
            {
                await TryConnectAsync();
                while (true)
                {
                    await CheckConnection();
                    await Task.Delay(1000);
                }
            });
        }
        public HubConnection Connection => _connection;
        private async Task<bool> TryConnectAsync()
        {
            var cts = new CancellationTokenSource();
            Task[] tasks = new Task[]
            {
                _connection.StartAsync(cts.Token),
                Task.Delay(10000)
            };

            try
            {
                var firstTaskNum = Task.WaitAny(tasks);
                if (firstTaskNum == 1)
                {
                    cts.Cancel();
                    throw new TaskCanceledException();
                }
                else
                {
                    if (tasks[0].Exception != null)
                        throw tasks[0].Exception.InnerException;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while attempting to connect hub: {0}", e.Message);
                return false;
            }
        }

        private async Task CheckConnection(bool tryReconnectOnFailure = true)
        {
            if (_connection.State != HubConnectionState.Disconnected)
                return;

            bool connectionObtained;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Trying to estabilish connection");
                Console.ResetColor();

                connectionObtained = await TryConnectAsync();
                if(connectionObtained)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Connection estabilished");
                    Console.ResetColor();
                }
                await Task.Delay(1000 * 30);
            } while (!connectionObtained);

        }
    }
}
