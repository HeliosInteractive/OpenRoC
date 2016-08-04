namespace oroc.Metrics
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using OpenHardwareMonitor.Hardware;

    public abstract class ICollector
    {
        protected readonly IHardware Hardware;
        protected readonly List<ISensor> Sensors;
        public double CurrentSample { get; private set; } = 0.0d;

        protected ICollector(IHardware hardware)
        {
            Hardware = hardware;
            Sensors = new List<ISensor>();

            if (Hardware != null)
            {
                Hardware.SensorAdded += (sensor) =>
                {
                    Log.i("Sensor {0} is added.", sensor.Name);

                    if (Sensors != null)
                    {
                        if (sensor.SensorType == SensorType.Load)
                            Sensors.Add(sensor);
                    }
                };

                Hardware.SensorRemoved += (sensor) =>
                {
                    Log.i("Sensor {0} is removed.", sensor.Name);

                    if (Sensors != null)
                    {
                        if (sensor.SensorType == SensorType.Load)
                            Sensors.Remove(sensor);
                    }
                };

                foreach (ISensor sensor in Hardware.Sensors)
                {
                    if (sensor.SensorType == SensorType.Load)
                    {
                        Sensors.Add(sensor);
                    }
                }
            }
        }

        public void Update()
        {
            if (Hardware == null || Sensors.Count == 0)
                return;

            Hardware.Update();
            
            foreach (IHardware subhardware in Hardware.SubHardware)
                subhardware.Update();

            try
            {
                CurrentSample = Sensors
                    .Where(sensor => sensor.Value.HasValue && sensor.Value.Value > 0)
                    .Average(sensor => sensor.Value.Value)
                    / 100d;
            }
            catch (InvalidOperationException) { /* no samples this tick */ }
        }
    }
}
