using System;

namespace General.Apt.App.Models.Video.Interpolation
{
    public class Data
    {
        public double FormFPS { get; set; }
        public double ToFPS { get; set; }
        public int FormTotalFrame { get; set; }
        public int ToTotalFrame { get; set; }
        public TimeSpan TotalTime { get; set; }
        public TimeSpan CurrentTime { get; set; }
        public int Scale { get; set; }

        public string State { get; set; }
        public int CurrentFrame { get; set; }
    }
}
