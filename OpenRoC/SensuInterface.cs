namespace oroc
{
    using liboroc;

    using System;
    using System.Text;
    using System.Net.Sockets;

    public class SensuInterface : IDisposable
    {
        private readonly ProcessManager Manager;
        private UdpClient sensuClientSocket;

        public SensuInterface(ProcessManager manager)
        {
            Manager = manager;

            try
            {
                sensuClientSocket = new UdpClient(
                    Settings.Instance.SensuInterfaceHost,
                    (int)Settings.Instance.SensuInterfacePort);
            }
            catch(Exception ex)
            {
                Log.e("Failed to construct Sensu client socket: {0}", ex.Message);
                sensuClientSocket = null;
            }
        }

        public void SendChecks()
        {
            if (sensuClientSocket == null)
                return;

            byte[] data = null;

            Manager.ProcessRunnerList.ForEach(runner =>
            {
                data = runner.ToSensuCheck();
                sensuClientSocket.Send(data, data.Length);
            });

            data = Encoding.Default.GetBytes(new
            {
                name = string.Format("{0}", Environment.MachineName),
                output = string.Format("Uptime: {0}", TimeSpan.FromTicks(Environment.TickCount)),
                status = 1
            }
            .ToJson());

            sensuClientSocket.Send(data, data.Length);
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
                }

                sensuClientSocket = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
