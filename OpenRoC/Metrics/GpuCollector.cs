namespace oroc.Metrics
{
    using OpenHardwareMonitor.Hardware;

    public class GpuCollector : ICollector
    {
        public GpuCollector(Computer computer)
            : base(GetFirstGpu(computer))
        { /* no-op */ }

        private static IHardware GetFirstGpu(Computer computer)
        {
            IHardware defaultHardware = null;

            foreach (IHardware hardwareItem in computer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.GpuAti ||
                    hardwareItem.HardwareType == HardwareType.GpuNvidia)
                {
                    defaultHardware = hardwareItem;
                }
            }

            return defaultHardware;
        }
    }
}
