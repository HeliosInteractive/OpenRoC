namespace oroc
{
    using liboroc;

    using System;
    using System.Text;
    using System.Linq;
    using System.Reflection;
    using System.Net.Sockets;
    using System.Diagnostics;

    public class SensuInterface : IDisposable
    {
        private readonly ProcessManager Manager;
        private readonly Metrics.Manager Metric;
        private UdpClient sensuClientSocket;
        private ExecutorService udpService;
        private PerformanceCounter uptime;

        public SensuInterface(ProcessManager manager, Metrics.Manager metrics, string host, uint port)
        {
            Manager = manager;
            Metric = metrics;

            try
            {
                sensuClientSocket = new UdpClient(host, (int)port);

                udpService = new ExecutorService();
                udpService.ExceptionReceived += ex => Log.e("UDP service: {0}", ex.Message);

                uptime = new PerformanceCounter("System", "System Up Time");

                manager.RunnerAdded += (runner) =>
                {
                    runner.StateChanged += () =>
                    {
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

        public void SendChecks()
        {
            Manager.Runners.ForEach(runner =>
            {
                QueueUdpCheck(runner.ToSensuCheckResult());
            });

            QueueUdpCheck(new
            {
                name = Environment.MachineName,
                uptime = TimeSpan.FromSeconds(uptime.NextValue()).ToString(@"hh\:mm\:ss"),
                output = Assembly.GetEntryAssembly().GetName().Name,
                status = 0,
                metrics = new
                {
                    cpu = Metric.CpuSamples.Last(),
                    ram = Metric.RamSamples.Last(),
                    gpu = Metric.GpuSamples.Last()
                }
            });
        }

        private void QueueUdpCheck(object data)
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
                    uptime?.Dispose();
                }

                sensuClientSocket = null;
                udpService = null;
                uptime = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
