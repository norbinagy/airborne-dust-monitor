using System;

namespace airborne_dust_monitor
{
    internal class TestSensorData
    {
        public DateTime createdAt { get; set; }
        public int entryId { get; set; }
        public double temperature { get; set; }
        public double humidity { get; set; }
    }
}
