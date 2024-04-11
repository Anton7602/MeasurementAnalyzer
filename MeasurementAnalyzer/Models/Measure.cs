using System;
using System.Collections.Generic;

namespace MeasurementAnalyzer.Models
{
    public class Measure
    {
        public string Id { get; set; }
        public string IdTrace { get; set; }
        public bool isFeed { get; set; }
        public byte SlabType { get; set; }
        public DateTime DateReceipt { get; set; }
        public DateTime DateTrace { get; set; }
        public DateTime DateMeasuring { get; set; }
        public string ImageTagRef { get; set; }
        public bool isRecord { get; set; }
        public List<Metering> Meterings { get; set; }
        public List<State> States { get; set; }
        public List<MeasurementDistance> Distances { get; set; }
        public int TraceXMin { get; set; }
        public int TraceXMax { get; set; }
        public decimal DistanceFactor { get; set; }
        public decimal OpticLengthFactor { get; set; }
        public decimal DeltaSpeed { get; set; }
        public decimal SpeedThreshold { get; set; }
        public Metering LastMetering { get; set; }
        public int ExpositionAdapting { get; set; }
        public Cyclogram swCyclogram { get; set; }
        public decimal TempVelocimeter { get; set; }
        public bool isBypass { get; set; }
        public bool isComplete { get; set; }
        public bool isRollback { get; set; }
        public string CompleteDesc { get; set; }
        public int TempSlab { get; set; }
        public int VideoWriterId { get; set; }
        public int ProfileCount { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal WidthLeft { get; set; }
        public decimal WidthRight { get; set; }
        public decimal Height { get; set; }
        public decimal HeightLeft { get; set; }
        public decimal HeightRight { get; set; }
        public decimal HeightCalculate { get; set; }
        public decimal WidthPlan { get; set; }
        public decimal WidthCalculate { get; set; }
        public decimal LengthPlan { get; set; }
        public decimal LengthSeries { get; set; }
        public decimal LengthCalculate { get; set; }
        public decimal Volume { get; set; }
        public decimal Weight { get; set; }
        public int MinCountProfilePoints { get; set; }
        public int MaxCountProfilePoints { get; set; }
        public int AvgCountProfilePoints { get; set; }
        public List<ProfilometerStatistic> ProfilometerStatistics { get; set; }
        public bool isPlanFind { get; set; }
        public bool isSeriesSlabsFind { get; set; }
        public decimal SNR { get; set; }
    }

    public class Metering
    {
        public DateTime dateTime { get; set; }
        public int ElapsedTime { get; set; }
        public decimal Speed { get; set; }
        public decimal Distance { get; set; }
        public decimal DistanceBegin { get; set; }
        public List<MeteringProfile> MeteringProfiles { get; set; }
        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal HeightLeft { get; set; }
        public decimal HeightRight { get; set; }
        public int Tag1 { get; set; }
        public int Tag2 { get; set; }
        public bool isExclude { get; set; }
    }

    public class MeteringProfile
    {
        public int ProfileometerID { get; set; }
        public int Id { get; set; }
        public List<Point> Points { get; set; }
        public List<Line> Lines { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public int RangePointsCount { get; set; }
        public int TotalLengthVerticalLines { get; set; }

    }

    public class ProfilometerStatistic
    {
        public string Ip { get; set; }
        public double PointsAverage { get; set; }
        public int Strength { get; set; }
        public int Exposition { get; set; }
        public int Gain { get; set; }
        public string AnalogGain { get; set; }
        public string IpShort { get; set; }
        public string IpSide { get; set; }
    }


    public class Point
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public int Spot { get; set; }
    }

    public class Line
    {
        public string P1 { get; set; }
        public string P2 { get; set; }
        public LineDirection Direction { get; set; }
        public decimal Angle { get; set; }
        public decimal AngleHorizontal { get; set; }
    }

    public class LineDirection
    {
        public bool isEmpty { get; set; }
        public decimal X { get; set; }
        public decimal Y { get; set; }
    }

    public class State
    {
        public int ElapsedTime { get; set; }
        public string Series { get; set; }
        public int Sample { get; set; }
        public int Tag { get; set; }
        public int Value { get; set; }
    }

    public class MeasurementDistance
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int ElapsedTime { get; set; }
        public decimal Speed { get; set; }
        public decimal SNR1 { get; set; }
        public decimal Distance { get; set; }
    }

    public class Cyclogram
    {
        public bool isRunning { get; set; }
        public string Elapsed { get; set; }
        public int ElapsedMilliseconds { get; set; }
        public long ElapsedTicks { get; set; }
    }
}
