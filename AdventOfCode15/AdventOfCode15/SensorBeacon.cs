namespace AdventOfCode15
{
    public class SensorBeacon
    {
        public (int, int) SensorCoords { get; set; }
        public (int, int) BeaconCoords { get; set; }
        public int ManhattanDistance { get; set; }
        public SensorBeacon((int, int) sensorCoords, (int, int) beaconCoords)
        {
            SensorCoords = sensorCoords;
            BeaconCoords = beaconCoords;
            ManhattanDistance = Utils.ManhattanDistance(sensorCoords.Item1, sensorCoords.Item2, beaconCoords.Item1, beaconCoords.Item2);
        }
    }
}
