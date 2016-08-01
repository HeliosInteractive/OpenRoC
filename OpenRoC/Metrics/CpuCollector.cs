namespace oroc.Metrics
{
    using OpenHardwareMonitor.Hardware;

    public class CpuCollector : ICollector
    {
        public CpuCollector(Computer computer)
            : base(GetFirstCpu(computer))
        { /* no-op */ }

        private static IHardware GetFirstCpu(Computer computer)
        {
            IHardware defaultHardware = null;

            foreach (IHardware hardwareItem in computer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    defaultHardware = hardwareItem;
                }
            }

            return defaultHardware;
        }
    }
}
