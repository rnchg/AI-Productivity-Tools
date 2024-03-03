namespace General.Apt.App.Models.Video.Organization.Android
{
    public class Entry
    {
        public long media_type { get; set; }
        public bool has_dash_audio { get; set; }
        public bool is_completed { get; set; }
        public long total_bytes { get; set; }
        public long downloaded_bytes { get; set; }
        public string title { get; set; }
        public string type_tag { get; set; }
        public string cover { get; set; }
        public long video_quality { get; set; }
        public long prefered_video_quality { get; set; }
        public long guessed_total_bytes { get; set; }
        public long total_time_milli { get; set; }
        public long danmaku_count { get; set; }
        public long time_update_stamp { get; set; }
        public long time_create_stamp { get; set; }
        public bool can_play_in_advance { get; set; }
        public bool interrupt_transform_temp_file { get; set; }
        public string quality_pithy_description { get; set; }
        public string quality_superscript { get; set; }
        public long cache_version_code { get; set; }
        public long preferred_audio_quality { get; set; }
        public long audio_quality { get; set; }
        public long avid { get; set; }
        public long spid { get; set; }
        public long seasion_id { get; set; }
        public string bvid { get; set; }
        public long owner_id { get; set; }
        public string owner_name { get; set; }
        public PageData page_data { get; set; }
    }

    public class PageData
    {
        public long cid { get; set; }
        public long page { get; set; }
        public string from { get; set; }
        public string part { get; set; }
        public string link { get; set; }
        public string vid { get; set; }
        public bool has_alias { get; set; }
        public long tid { get; set; }
        public long width { get; set; }
        public long height { get; set; }
        public long rotate { get; set; }
        public string download_title { get; set; }
        public string download_subtitle { get; set; }
    }
}
