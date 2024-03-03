namespace General.Apt.App.Models.Setting
{
    public class Config
    {
        public App App { get; set; }
        public Image Image { get; set; }
        public Video Video { get; set; }
    }

    public class App
    {
        public string Name { get; set; }
    }
    public class Image
    {
        public ImageRestoration Restoration { get; set; }
    }
    public class ImageRestoration
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public string Gpu { get; set; }
        public int Decode { get; set; }
        public int Amplify { get; set; }
        public int Encode { get; set; }
        public string Model { get; set; }
        public string Scale { get; set; }
    }
    public class Video
    {
        public VideoOrganization Organization { get; set; }
        public VideoRestoration Restoration { get; set; }
        public VideoInterpolation Interpolation { get; set; }
    }
    public class VideoOrganization
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public string Client { get; set; }
    }
    public class VideoRestoration
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public string Gpu { get; set; }
        public int Decode { get; set; }
        public int Amplify { get; set; }
        public int Encode { get; set; }
        public string Model { get; set; }
        public string Scale { get; set; }
    }
    public class VideoInterpolation
    {
        public string Input { get; set; }
        public string Output { get; set; }
        public string Gpu { get; set; }
        public int Decode { get; set; }
        public int Amplify { get; set; }
        public int Encode { get; set; }
        public string Scale { get; set; }
    }
}
