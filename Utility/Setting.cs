using General.Apt.App.Extensions;
using General.Apt.App.Models.Setting;
using System;
using System.IO;
using System.Reflection;

namespace General.Apt.App.Utility
{
    public static class Setting
    {
        public static string AppPath { get; set; }
        public static string AppName { get; set; }
        public static string AppDirectory { get; set; }

        public static string ConfigPath { get; set; }
        public static string FFmpegPath { get; set; }
        public static string RealESRGANPath { get; set; }
        public static string RifePath { get; set; }

        public static Config Config { get; set; }

        static Setting()
        {
            AppPath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            AppName = Path.GetFileNameWithoutExtension(AppPath);
            AppDirectory = Path.GetDirectoryName(AppPath);

            ConfigPath = Path.Combine(Environment.CurrentDirectory, Const.App.ConfigPath);
            FFmpegPath = Path.Combine(AppDirectory, Const.App.FFmpegPath);
            RealESRGANPath = Path.Combine(AppDirectory, Const.App.RealESRGANPath);
            RifePath = Path.Combine(AppDirectory, Const.App.RifePath);
        }

        public static Config GetConfig()
        {
            var config = new Config();

            config.App = new Models.Setting.App();
            config.App.Name = Assembly.GetExecutingAssembly().GetName().Name;

            config.Image = new Image();
            config.Image.Restoration = new ImageRestoration();
            config.Image.Restoration.Input = "";
            config.Image.Restoration.Output = "";
            config.Image.Restoration.Gpu = "auto";
            config.Image.Restoration.Decode = 1;
            config.Image.Restoration.Amplify = 2;
            config.Image.Restoration.Encode = 2;
            config.Image.Restoration.Model = "Standard";
            config.Image.Restoration.Scale = "X2";

            config.Video = new Video();
            config.Video.Organization = new VideoOrganization();
            config.Video.Organization.Input = "";
            config.Video.Organization.Output = "";
            config.Video.Organization.Client = "UWP";

            config.Video.Restoration = new VideoRestoration();
            config.Video.Restoration.Input = "";
            config.Video.Restoration.Output = "";
            config.Video.Restoration.Gpu = "auto";
            config.Video.Restoration.Decode = 1;
            config.Video.Restoration.Amplify = 2;
            config.Video.Restoration.Encode = 2;
            config.Video.Restoration.Model = "Standard";
            config.Video.Restoration.Scale = "X2";

            config.Video.Interpolation = new VideoInterpolation();
            config.Video.Interpolation.Input = "";
            config.Video.Interpolation.Output = "";
            config.Video.Interpolation.Gpu = "auto";
            config.Video.Interpolation.Decode = 1;
            config.Video.Interpolation.Amplify = 2;
            config.Video.Interpolation.Encode = 2;
            config.Video.Interpolation.Scale = "X2";

            return config;
        }

        public static bool SetSetting()
        {
            try
            {
                Properties.Settings.Default.Config = Config.ToJson();
                Properties.Settings.Default.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool GetSetting()
        {
            try
            {
                var config = Properties.Settings.Default.Config.JsonTo<Config>();
                Config = GetSetting(config);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static Config GetSetting(Config config)
        {
            var configDefault = GetConfig();
            if (config == null) config = configDefault;
            if (config.App == null) config.App = configDefault.App;
            if (config.Image == null) config.Image = configDefault.Image;
            if (config.Image.Restoration == null) config.Image.Restoration = configDefault.Image.Restoration;
            if (config.Video == null) config.Video = configDefault.Video;
            if (config.Video.Organization == null) config.Video.Organization = configDefault.Video.Organization;
            if (config.Video.Restoration == null) config.Video.Restoration = configDefault.Video.Restoration;
            if (config.Video.Interpolation == null) config.Video.Interpolation = configDefault.Video.Interpolation;
            return config;
        }
    }
}