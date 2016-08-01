namespace oroc.Metrics
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using OpenHardwareMonitor.Hardware;

    public class Manager : IDisposable
    {
        private Task setupTask;
        private Computer computer;
        private ICollector cpuCollector;
        private ICollector gpuCollector;
        private ICollector ramCollector;

        public double[] CpuSamples { get; private set; }

        public double[] GpuSamples { get; private set; }

        public double[] RamSamples { get; private set; }

        public Manager()
        {
            var initial_sensor_value = 0.0d;
            var initial_sensor_count = 50;

            CpuSamples = new double[initial_sensor_count];
            CpuSamples = Enumerable.Repeat(initial_sensor_value, initial_sensor_count).ToArray();

            GpuSamples = new double[initial_sensor_count];
            GpuSamples = Enumerable.Repeat(initial_sensor_value, initial_sensor_count).ToArray();

            RamSamples = new double[initial_sensor_count];
            RamSamples = Enumerable.Repeat(initial_sensor_value, initial_sensor_count).ToArray();

            setupTask = Task.Run(() =>
            {
                computer = new Computer
                {
                    CPUEnabled = true,
                    GPUEnabled = true,
                    RAMEnabled = true,
                };

                computer.Open();

                cpuCollector = new CpuCollector(computer);
                gpuCollector = new GpuCollector(computer);
                ramCollector = new RamCollector(computer);
            });
        }

        public void Update()
        {
            if (setupTask.Status != TaskStatus.RanToCompletion)
                return;

            cpuCollector.Update();
            gpuCollector.Update();
            ramCollector.Update();

            CpuSamples.ShiftLeft(cpuCollector.CurrentSample);
            GpuSamples.ShiftLeft(gpuCollector.CurrentSample);
            RamSamples.ShiftLeft(ramCollector.CurrentSample);
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
                    if (!setupTask.IsCompleted)
                        setupTask.Wait();

                    computer?.Close();
                }

                computer = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
