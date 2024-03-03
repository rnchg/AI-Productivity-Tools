using System;

namespace General.Apt.App.Models.Video.Restoration
{
    public class Data
    {
        public double FPS { get; set; }
        public int TotalFrame { get; set; }
        public TimeSpan TotalTime { get; set; }
        public TimeSpan CurrentTime { get; set; }

        public double Percent { get; set; }
        public int CurrentFrame { get; set; }
    }
}