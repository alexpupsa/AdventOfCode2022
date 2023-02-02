namespace AdventOfCode15
{
    public class SignalBeacon
    {
        public (int, int) SignalCoords { get; set; }
        public (int, int) BeaconCoords { get; set; }
        public int ManhattanDistance { get; set; }
        public SignalBeacon((int, int) signalCoords, (int, int) beaconCoords)
        {
            SignalCoords = signalCoords;
            BeaconCoords = beaconCoords;
            ManhattanDistance = Utils.ManhattanDistance(signalCoords.Item1, signalCoords.Item2, beaconCoords.Item1, beaconCoords.Item2);
        }
    }
}
