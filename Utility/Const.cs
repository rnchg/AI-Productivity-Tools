namespace General.Apt.App.Utility
{
    public class Const
    {
        public class App
        {
            public const string ConfigPath = @"Config.json";
            public const string FFmpegPath = @"Dependencies\ffmpeg\ffmpeg.exe";
            public const string RealESRGANPath = @"Dependencies\realesrgan\realesrgan-ncnn-vulkan.exe";
            public const string RifePath = @"Dependencies\rife\rife-ncnn-vulkan.exe";
        }
        public class Color
        {
            public const string Text = "#595959";
            public const string Information = "#1677ff";
            public const string Success = "#52c41a";
            public const string Warning = "#faad14";
            public const string Error = "#ff4d4f";
        }
        public class Command
        {
            public class Image
            {
                public class Restoration
                {
                    public const string RealESRGANIn = "-i \"{0}\" -o \"{1}\" -g {2} -j {3} -n {4} -s {5} -f png -v";
                    public const string RealESRGANInOut1 = @"encode image .+? failed";
                    public const string RealESRGANInOut2 = @"(\d+.\d+)%";
                }
            }

            public class Video
            {
                public class Organization
                {
                    public const string PcFFmpegIn = "-i \"{0}\" -i \"{1}\" -c copy -y \"{2}\"";
                    public const string AndroidFFmpegIn1 = "-i \"{0}\" -c copy -y \"{1}\"";
                    public const string AndroidFFmpegIn2 = "-i \"{0}\" -i \"{1}\" -c copy -y \"{2}\"";
                }

                public class Restoration
                {
                    public const string FFmpegIn1 = "-i \"{0}\" -qscale:v 1 -qmin 1 -qmax 1 -vsync 0 \"{1}/%08d.png\"";
                    public const string FFmpegIn1Out1 = @"No space left on device|I/O error";
                    public const string FFmpegIn1Out2 = @"\s*Duration:\s(\d{2}:\d{2}:\d{2}\.\d{2}).+";
                    public const string FFmpegIn1Out3 = @"(\d+.?\d+)\stbr";
                    public const string FFmpegIn1Out4 = @"frame=\s*\d+\s.+?time=(\d{2}:\d{2}:\d{2}\.\d{2}).+";

                    public const string RealESRGANIn = "-i \"{0}\" -o \"{1}\" -g {2} -j {3} -n {4} -s {5} -f png -v";
                    public const string RealESRGANInOut1 = @"encode image .+? failed";
                    public const string RealESRGANInOut2 = @"(\d+.\d+)%";

                    public const string FFmpegIn2 = "-r {0} -i \"{1}/%08d.png\" -i \"{2}\" -map 0:v:0 -map 1 -map -1:v -max_interleave_delta 0 -c:a copy -c:v libx264 -r {3} -vf scale=out_color_matrix=bt709,format=yuv420p \"{4}\"";
                    public const string FFmpegIn2Out1 = @"No space left on device|I/O error";
                    public const string FFmpegIn2Out2 = @"\s*Duration:\s(\d{2}:\d{2}:\d{2}\.\d{2}).+";
                    public const string FFmpegIn2Out3 = @"frame=\s*\d+\s.+?time=(\d{2}:\d{2}:\d{2}\.\d{2}).+";
                }

                public class Interpolation
                {
                    public const string FFmpegIn1 = "-i \"{0}\" -qscale:v 1 -qmin 1 -qmax 1 -vsync 0 \"{1}/%08d.png\"";
                    public const string FFmpegIn1Out1 = @"No space left on device|I/O error";
                    public const string FFmpegIn1Out2 = @"\s*Duration:\s(\d{2}:\d{2}:\d{2}\.\d{2}).+";
                    public const string FFmpegIn1Out3 = @"(\d+.?\d+)\stbr";
                    public const string FFmpegIn1Out4 = @"frame=\s*\d+\s.+?time=(\d{2}:\d{2}:\d{2}\.\d{2}).+";

                    public const string RifeIn = "-i \"{0}\" -o \"{1}\" -g {2} -j {3} -n {4} -m {5} -f png -v";
                    public const string RifeOut1 = @"encode image .+? failed";
                    public const string RifeOut2 = @"(done)";

                    public const string FFmpegIn2 = "-r {0} -i \"{1}/%08d.png\" -i \"{2}\" -map 0:v:0 -map 1 -map -1:v -max_interleave_delta 0 -c:a copy -c:v libx264 -r {3} -vf scale=out_color_matrix=bt709,format=yuv420p \"{4}\"";
                    public const string FFmpegIn2Out1 = @"No space left on device|I/O error";
                    public const string FFmpegIn2Out2 = @"\s*Duration:\s(\d{2}:\d{2}:\d{2}\.\d{2}).+";
                    public const string FFmpegIn2Out3 = @"frame=\s*\d+\s.+?time=(\d{2}:\d{2}:\d{2}\.\d{2}).+";
                }
            }
        }
    }
}
