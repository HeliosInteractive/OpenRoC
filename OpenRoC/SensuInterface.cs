namespace oroc
{
    using liboroc;

    using System;
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

            Manager.ProcessRunnerList.ForEach(runner =>
            {
                byte[] data = runner.ToSensuCheck();
                sensuClientSocket.Send(data, data.Length);
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
