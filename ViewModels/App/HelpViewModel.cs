using General.Apt.App.Utility;
using General.Apt.App.ViewModels.Base;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace General.Apt.App.ViewModels.App
{
    public class HelpViewModel : ViewModelBase
    {
        private string Text { get; set; }
        public FlowDocument Message { get; set; } = new FlowDocument();
        public Action<FlowDocument> MessageAction { get; set; }

        public HelpViewModel()
        {
            ShowInformation();
        }

        public void ShowInformation()
        {
            Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Text = string.Empty;
                    Text += $"AI 生产力工具 V{Assembly.GetExecutingAssembly().GetName().Version}";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontWeight = FontWeights.Bold,
                        FontSize = 32
                    }));

                    Text = string.Empty;
                    Text += "温馨提示：";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontWeight = FontWeights.Bold,
                        FontSize = 24
                    }));

                    Text = string.Empty;
                    Text += "本工具安全绿色，功能全面，操作便捷。\r\n";
                    Text += "视频整理：一键批量（解密、合并、整理）B站下载视频。\r\n";
                    Text += "图片放大：一键批量无损放大图片（1080P→8K）。\r\n";
                    Text += "视频放大：一键批量无损放大视频（1080P→8K）。\r\n";
                    Text += "视频补帧：一键批量智能补帧视频（30FPS→240FPS）。";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontSize = 14
                    }));

                    Text = string.Empty;
                    Text += "技术支持：";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontWeight = FontWeights.Bold,
                        FontSize = 24
                    }));
                    Text = string.Empty;
                    Text += "联系方式 [ QQ: 6085398 ] [ Email: Rnchg@Hotmail.com ] [ B站; 风轻云也净 ]。";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontSize = 14
                    }));

                    Text = string.Empty;
                    Text += "操作说明：";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontWeight = FontWeights.Bold,
                        FontSize = 24
                    }));

                    Text = string.Empty;
                    Text += "视频整理";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontWeight = FontWeights.Bold,
                        FontSize = 16
                    }));

                    Text = string.Empty;
                    Text += "操作步骤：选择输入目录→选择输出目录→选择程序类型→点击开始按钮→等待处理完成。\r\n";
                    Text += "输入目录：处理前文件所在文件夹路径。\r\n";
                    Text += "输出目录：处理后文件所在文件夹路径。\r\n";
                    Text += "程序类型：支持哔哩哔哩（BiliBili）官方客户端（UWP/Windows/Android）。";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontSize = 14
                    }));

                    Text = string.Empty;
                    Text += "图片放大";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontWeight = FontWeights.Bold,
                        FontSize = 16
                    }));

                    Text = string.Empty;
                    Text += "操作步骤：选择输入目录→选择输出目录→选择处理模式→选择放大倍率→点击开始按钮→等待处理完成。\r\n";
                    Text += "输入目录：处理前文件所在文件夹路径。\r\n";
                    Text += "输出目录：处理后文件所在文件夹路径。\r\n";
                    Text += "性能配置：支持（GPU设置/解码线程数/处理线程数/编码线程数）等不同性能配置。\r\n";
                    Text += "处理模式：支持（标准/动画）等不同处理模式。\r\n";
                    Text += "放大倍率：支持（2X/3X/4X）等不同放大倍率。";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontSize = 14
                    }));

                    Text = string.Empty;
                    Text += "视频放大";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontWeight = FontWeights.Bold,
                        FontSize = 16
                    }));

                    Text = string.Empty;
                    Text += "操作步骤：选择输入目录→选择输出目录→选择处理模式→选择放大倍率→点击开始按钮→等待处理完成。\r\n";
                    Text += "输入目录：处理前文件所在文件夹路径。\r\n";
                    Text += "输出目录：处理后文件所在文件夹路径。\r\n";
                    Text += "性能配置：支持（GPU设置/解码线程数/处理线程数/编码线程数）等不同性能配置。\r\n";
                    Text += "处理模式：支持（标准/动画）等不同处理模式。\r\n";
                    Text += "放大倍率：支持（2X/3X/4X）等不同放大倍率。";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontSize = 14
                    }));

                    Text = string.Empty;
                    Text += "视频补帧";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontWeight = FontWeights.Bold,
                        FontSize = 16
                    }));

                    Text = string.Empty;
                    Text += "操作步骤：选择输入目录→选择输出目录→选择补帧倍率→点击开始按钮→等待处理完成。\r\n";
                    Text += "输入目录：处理前文件所在文件夹路径。\r\n";
                    Text += "输出目录：处理后文件所在文件夹路径。\r\n";
                    Text += "性能配置：支持（GPU设置/解码线程数/处理线程数/编码线程数）等不同性能配置。\r\n";
                    Text += "放大倍率：支持（2X/3X/4X/5X/6X/7X/8X）等不同补帧倍率。";
                    Message.Blocks.Add(new Paragraph(new Run(Text)
                    {
                        Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(Const.Color.Text)),
                        FontFamily = new FontFamily("微软雅黑"),
                        FontSize = 14
                    }));

                    MessageAction?.Invoke(Message);
                });
            });
        }
    }
}
