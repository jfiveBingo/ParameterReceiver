using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ParameterReceiver
{
    public class ParameterTcpServer
    {
        private TcpListener listener;
        private TcpClient client;

        private readonly int port;

        public bool IsClientConnected
        {
            get
            {
                return client != null && client.Connected;
            }
        }

        public ParameterTcpServer(int port)
        {
            this.port = port;
        }

        public void Start()
        {
            listener = new TcpListener(IPAddress.Loopback, port);

            listener.Start();

            AcceptClient();
        }

        private async void AcceptClient()
        {
            try
            {
                while (true)
                {
                    client = await listener.AcceptTcpClientAsync();

                    // Application X connected
                }
            }
            catch
            {
                // Server stopped
            }
        }

        public async Task SendAsync(ParameterMessage message)
        {
            if (client == null || !client.Connected)
                return;

            try
            {
                NetworkStream stream = client.GetStream();

                string json = JsonSerializer.Serialize(message);

                byte[] data = Encoding.UTF8.GetBytes(json + "\n");

                await stream.WriteAsync(data, 0, data.Length);
            }
            catch
            {
                client = null;
            }
        }

        public void Stop()
        {
            try
            {
                client?.Close();
                listener?.Stop();
            }
            catch
            {

            }
        }
    }
}
