namespace General.Apt.App.Models.Video.Organization.Pc
{
    public class PlayUrl
    {
        public long code { get; set; }
        public string message { get; set; }
        public long ttl { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string from { get; set; }
        public string result { get; set; }
        public string message { get; set; }
        public long quality { get; set; }
        public string format { get; set; }
        public long timelength { get; set; }
        public string accept_format { get; set; }
        public string[] accept_description { get; set; }
        public long[] accept_quality { get; set; }
        public long video_codecid { get; set; }
        public string seek_param { get; set; }
        public string seek_type { get; set; }
        public Dash dash { get; set; }
        public SupportFormat[] support_formats { get; set; }
        public object high_format { get; set; }
        public long last_play_time { get; set; }
        public long last_play_cid { get; set; }
    }

    public class Dash
    {
        public long duration { get; set; }
        public double minBufferTime { get; set; }
        public double min_buffer_time { get; set; }
        public Video[] video { get; set; }
        public Audio[] audio { get; set; }
    }

    public class Video
    {
        public long id { get; set; }
        public string baseUrl { get; set; }
        public string base_url { get; set; }
        public long bandwidth { get; set; }
        public string mimeType { get; set; }
        public string mime_type { get; set; }
        public string codecs { get; set; }
        public long width { get; set; }
        public long height { get; set; }
        public string frameRate { get; set; }
        public string frame_rate { get; set; }
        public string sar { get; set; }
        public long startWithSap { get; set; }
        public long start_with_sap { get; set; }
        public SegmentBase SegmentBase { get; set; }
        public Segment_Base segment_base { get; set; }
        public Dolby dolby { get; set; }
        public object flac { get; set; }
    }

    public class SegmentBase
    {
        public string Initialization { get; set; }
        public string indexRange { get; set; }
    }

    public class Segment_Base
    {
        public string initialization { get; set; }
        public string index_range { get; set; }
    }

    public class Audio
    {
        public long id { get; set; }
        public string baseUrl { get; set; }
        public string base_url { get; set; }
        public long bandwidth { get; set; }
        public string mimeType { get; set; }
        public string mime_type { get; set; }
        public string codecs { get; set; }
        public long width { get; set; }
        public long height { get; set; }
        public string frameRate { get; set; }
        public string frame_rate { get; set; }
        public string sar { get; set; }
        public long startWithSap { get; set; }
        public long start_with_sap { get; set; }
        public SegmentBase SegmentBase { get; set; }
        public Segment_Base segment_base { get; set; }
        public long codecid { get; set; }
    }

    public class Dolby
    {
        public long type { get; set; }
        public object audio { get; set; }
    }

    public class SupportFormat
    {
        public long quality { get; set; }
        public string format { get; set; }
        public string new_description { get; set; }
        public string display_desc { get; set; }
        public string superscript { get; set; }
        public string[] codecs { get; set; }
    }
}
