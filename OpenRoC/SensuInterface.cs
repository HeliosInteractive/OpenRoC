namespace oroc
{
    using liboroc;

    using System;
    using System.IO;
    using System.Text;
    using System.Net.Sockets;

    public class SensuInterface : IDisposable
    {
        private readonly ProcessManager Manager;
        private UdpClient sensuClientSocket;
        private ExecutorService udpService;
        private uint checksTtl;

        public SensuInterface(ProcessManager manager, string host, uint port, uint ttl)
        {
            Manager = manager;
            checksTtl = ttl;

            try
            {
                sensuClientSocket = new UdpClient(host, (int)port);

                udpService = new ExecutorService();
                udpService.ExceptionReceived += ex => Log.e("UDP service: {0}", ex.Message);

                manager.RunnerAdded += (runner) =>
                {
                    runner.StateChanged += () =>
                    {
                        if (runner.State != ProcessRunner.Status.Invalid)
                            SendChecks();
                    };
                };
            }
            catch(Exception ex)
            {
                Log.e("Failed to construct Sensu client socket: {0}", ex.Message);
                sensuClientSocket = null;
            }
        }

        public void SetTTL(uint ttl)
        {
            if (checksTtl == ttl)
                return;

            checksTtl = ttl;
        }

        public void SendChecks()
        {
            Manager.Runners.ForEach(runner =>
            {
                QueueUdpPacket(new
                {
                    name = Path.GetFileName(runner.ProcessOptions.Path),
                    output = runner.GetStateString(),
                    status = (int)runner.State,
                    timeout = checksTtl,
                    ttl = checksTtl
                });
            });
        }

        private void QueueUdpPacket(object data)
        {
            udpService?.Accept(() =>
            {
                byte[] packet = Encoding.Default.GetBytes(data.ToJson());
                sensuClientSocket?.Send(packet, packet.Length);
            });
        }

        #region IDisposable Support
        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                IsDisposed = true;

                if (disposing)
                {
                    sensuClientSocket?.Close();
                    udpService?.Dispose();
                }

                sensuClientSocket = null;
                udpService = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
