namespace liboroc
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public class ExecutorService : IDisposable
    {
        private readonly List<Task> pending;
        public Action<Exception> ExceptionReceived;

        public ExecutorService()
        {
            pending = new List<Task>();
        }

        public void Accept(Action action)
        {
            if (IsDisposed)
                return;

            lock (this)
            {
                pending?.Add(Task.Run(() =>
                {
                    try { action(); }
                    catch (Exception ex)
                    { ExceptionReceived?.Invoke(ex); }
                }));
            }
        }

        public void Wait()
        {
            if (IsDisposed)
                return;

            lock (this)
            {
                try
                {
                    Task.WaitAll(pending.ToArray());
                }
                catch (ObjectDisposedException ex)
                {
                    ExceptionReceived?.Invoke(ex);
                }
                catch (AggregateException ex)
                {
                    ExceptionReceived?.Invoke(ex);
                }

                pending.ForEach(task => task.Dispose());
                pending.Clear();
            }
        }

        #region IDisposable Support
        public bool IsDisposed { get; private set; } = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    Wait();
                }

                IsDisposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
