namespace oroc.Metrics
{
    using OpenHardwareMonitor.Hardware;

    public class RamCollector : ICollector
    {
        public RamCollector(Computer computer)
            : base(GetFirstRam(computer))
        { /* no-op */ }

        private static IHardware GetFirstRam(Computer computer)
        {
            IHardware defaultHardware = null;

            foreach (IHardware hardwareItem in computer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.RAM)
                {
                    defaultHardware = hardwareItem;
                }
            }

            return defaultHardware;
        }
    }
}
