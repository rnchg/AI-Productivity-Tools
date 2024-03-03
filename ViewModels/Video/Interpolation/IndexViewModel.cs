using General.Apt.App.Models;
using General.Apt.App.Models.Video.Interpolation;
using General.Apt.App.Utility;
using General.Apt.App.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace General.Apt.App.ViewModels.Video.Interpolation
{
    public class IndexViewModel : ViewModelBase
    {
        public Action<Paragraph> MessageAction { get; set; }
        private char Separator => Path.DirectorySeparatorChar;

        private string _input;
        public string Input
        {
            get { return _input; }
            set
            {
                _input = value;
                OnPropertyChanged(nameof(Input));
            }
        }

        public ICommand InputCommand => new CommandBase(e =>
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(Input)) folderBrowserDialog.SelectedPath = Input;
            if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Input = folderBrowserDialog.SelectedPath;
        });

        private string _output;
        public string Output
        {
            get { return _output; }
            set
            {
                _output = value;
                OnPropertyChanged(nameof(Output));
            }
        }

        public ICommand OutputCommand => new CommandBase(e =>
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(Output)) folderBrowserDialog.SelectedPath = Output;
            if (folderBrowserDialog.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            Output = folderBrowserDialog.SelectedPath;
        });

        private ObservableCollection<ComBoBoxItem<string>> _gpuSource;
        public ObservableCollection<ComBoBoxItem<string>> GpuSource
        {
            get { return _gpuSource; }
            set
            {
                _gpuSource = value;
                OnPropertyChanged(nameof(GpuSource));
            }
        }

        private ComBoBoxItem<string> _gpuItem;
        public ComBoBoxItem<string> GpuItem
        {
            get { return _gpuItem; }
            set
            {
                _gpuItem = value;
                OnPropertyChanged(nameof(GpuItem));
            }
        }

        public string Gpu
        {
            get => GpuItem.Value;
            set => GpuItem = GpuSource.FirstOrDefault(e => e.Value == value);
        }

        public int _decode;
        public int Decode
        {
            get { return _decode; }
            set
            {
                _decode = value;
                OnPropertyChanged(nameof(Decode));
            }
        }

        public int _handle;
        public int Handle
        {
            get { return _handle; }
            set
            {
                _handle = value;
                OnPropertyChanged(nameof(Handle));
            }
        }

        public int _encode;
        public int Encode
        {
            get { return _encode; }
            set
            {
                _encode = value;
                OnPropertyChanged(nameof(Encode));
            }
        }

        private ObservableCollection<ComBoBoxItem<string>> _scaleSource;
        public ObservableCollection<ComBoBoxItem<string>> ScaleSource
        {
            get { return _scaleSource; }
            set
            {
                _scaleSource = value;
                OnPropertyChanged(nameof(ScaleSource));
            }
        }

        private ComBoBoxItem<string> _scaleItem;
        public ComBoBoxItem<string> ScaleItem
        {
            get { return _scaleItem; }
            set
            {
                _scaleItem = value;
                OnPropertyChanged(nameof(ScaleItem));
            }
        }

        public string Scale
        {
            get
            {
                return ScaleItem.Value;
            }
            set
            {
                ScaleItem = ScaleSource.FirstOrDefault(e => e.Value == value);
            }
        }

        private int _progressBarMaximum;
        public int ProgressBarMaximum
        {
            get
            {
                return _progressBarMaximum;
            }
            set
            {
                _progressBarMaximum = value;
                OnPropertyChanged(nameof(ProgressBarMaximum));
            }
        }

        private int _progressBarValue;
        public int ProgressBarValue
        {
            get
            {
                return _progressBarValue;
            }
            set
            {
                _progressBarValue = value;
                OnPropertyChanged(nameof(ProgressBarValue));
                OnPropertyChanged(nameof(ProgressBarText));
            }
        }

        public string ProgressBarText
        {
            get
            {
                return (ProgressBarValue / (double)ProgressBarMaximum).ToString("0.00%");
            }
        }

        public ICommand StartCommand => new CommandBase(async e => await Start());

        private bool _startEnabled;
        public bool StartEnabled
        {
            get
            {
                return _startEnabled;
            }
            set
            {
                _startEnabled = value;
                OnPropertyChanged(nameof(StartEnabled));
            }
        }

        public ICommand StopCommand => new CommandBase(e =>
        {
            StopEnabled = false;
        });

        private bool _stopEnabled;
        public bool StopEnabled
        {
            get
            {
                return _stopEnabled;
            }
            set
            {
                _stopEnabled = value;
                OnPropertyChanged(nameof(StopEnabled));
            }
        }

        public IndexViewModel()
        {
            GpuSource = Computer.GetGPU();
            GpuSource.Insert(0, new ComBoBoxItem<string> { Text = "自动", Value = "auto" });
            ScaleSource = new ObservableCollection<ComBoBoxItem<string>>()
            {
                new ComBoBoxItem<string>() { Text="X2", Value="X2" },
                new ComBoBoxItem<string>() { Text="X3", Value="X3" },
                new ComBoBoxItem<string>() { Text="X4", Value="X4" },
                new ComBoBoxItem<string>() { Text="X5", Value="X5" },
                new ComBoBoxItem<string>() { Text="X6", Value="X6" },
                new ComBoBoxItem<string>() { Text="X7", Value="X7" },
                new ComBoBoxItem<string>() { Text="X8", Value="X8" }
            };
            ProgressBarMaximum = 1000000;
            ProgressBarValue = 0;
            StartEnabled = true;
            StopEnabled = false;
            ShowInformation();
        }

        public void ShowInformation()
        {
            Task.Run(() =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    var message = string.Empty;
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    message += "温馨提示：\r\n";
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    message += "视频补帧：一键批量智能补帧视频（30FPS→240FPS）[ mp4/mkv ]。\r\n";
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    message += "技术支持：\r\n";
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    message += "联系方式 [ QQ: 6085398 ] [ Email: Rnchg@Hotmail.com ] [ B站; 风轻云也净 ]。\r\n";
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    message += "操作说明：\r\n";
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    message += "操作步骤：选择输入目录→选择输出目录→选择处理模式→选择放大倍率→点击开始按钮→等待处理完成。\r\n";
                    message += "输入目录：处理前文件所在文件夹路径。\r\n";
                    message += "输出目录：处理后文件所在文件夹路径。\r\n";
                    message += "性能配置：支持（GPU设置/解码线程数/处理线程数/编码线程数）等不同性能配置。\r\n";
                    message += "处理模式：支持（标准/动画）等不同处理模式。\r\n";
                    message += "放大倍率：支持（2X/3X/4X）等不同放大倍率。\r\n";
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    var run = new Run(message);
                    var color = (Color)ColorConverter.ConvertFromString(Const.Color.Information);
                    run.Foreground = new SolidColorBrush(color);
                    run.FontWeight = FontWeights.Bold;
                    var paragraph = new Paragraph(run);
                    MessageAction?.Invoke(paragraph);
                });
            });
        }

        public string GetGpu()
        {
            return Gpu.ToString();
        }

        public string GetThread()
        {
            return $"{Decode}:{Handle}:{Encode}";
        }

        public int GetScale()
        {
            if (Scale == "X2")
            {
                return 2;
            }
            if (Scale == "X3")
            {
                return 3;
            }
            if (Scale == "X4")
            {
                return 4;
            }
            if (Scale == "X5")
            {
                return 5;
            }
            if (Scale == "X6")
            {
                return 6;
            }
            if (Scale == "X7")
            {
                return 7;
            }
            if (Scale == "X8")
            {
                return 8;
            }
            throw new Exception("获取缩放失败！");
        }

        public async Task Start()
        {
            try
            {
                if (string.IsNullOrEmpty(Input))
                {
                    throw new Exception("请选择输入目录");
                }
                if (string.IsNullOrEmpty(Output))
                {
                    throw new Exception("请选择输出目录");
                }
                if (!File.Exists(Setting.RealESRGANPath))
                {
                    throw new Exception($"请检查依赖目录 [ {Setting.RealESRGANPath} ] ");
                }
                if (!File.Exists(Setting.FFmpegPath))
                {
                    throw new Exception($"请检查依赖目录 [ {Setting.FFmpegPath} ] ");
                }

                StartEnabled = false;
                StopEnabled = true;

                await GetVideo();

                ProgressBarValue = ProgressBarMaximum;

                Dialog.ShowInformationDialog($"操作完成");
            }
            catch (Exception ex)
            {
                await Logger.AddError(ex.Message, MessageAction);
                Dialog.ShowErrorDialog(ex.Message);
            }
            finally
            {
                ProgressBarValue = 0;
                StartEnabled = true;
                StopEnabled = false;
            }
        }

        public async Task GetVideo()
        {
            var files = Directory.GetFiles(Input, "*", SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
            {
                throw new Exception($"待处理文件不存在 [{Input}]");
            }
            var exts = new[] { ".mp4", ".mkv" };
            files = files.Where(e => exts.Contains(Path.GetExtension(e).ToLower())).ToArray();
            if (files == null || files.Length == 0)
            {
                throw new Exception($"符合条件的文件不存在 [ {string.Join("|", exts.Select(e => $"*{e}"))} ]");
            }
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                if (File.Exists(file))
                {
                    await Video(file, i, files.Length);
                }
                else
                {
                    await Logger.AddError($"待处理文件丢失 [{file}]", MessageAction);
                }
                if (!StopEnabled) throw new Exception("已停止操作");
            }
        }

        public async Task Video(string inputFile, int currentFile, int totalFiles)
        {
            var outputFile = inputFile.Replace(Input, Output);
            var outputDirectory = outputFile.Replace(".", "_");
            Directory.CreateDirectory(outputDirectory);

            var outputDirectoryInput = $"{outputDirectory}{Separator}input";
            Directory.CreateDirectory(outputDirectoryInput);

            var data = new Data();
            try
            {
                await Logger.AddSuccess($"[{inputFile}] 文件处理开始[ 解析帧到文件夹 ] [{outputDirectoryInput}]", MessageAction);
                var command = string.Format(Const.Command.Video.Interpolation.FFmpegIn1, inputFile, outputDirectoryInput);
                await Proc.StartProcess(Setting.FFmpegPath, command, null, async (sender, args) =>
                {
                    if (!StopEnabled)
                    {
                        Proc.CancelProcess((Process)sender, () => { StartEnabled = true; StopEnabled = false; });
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    if (Regex.IsMatch(args.Data, Const.Command.Video.Interpolation.FFmpegIn1Out1))
                    {
                        await Logger.AddError($"出现错误：[ {args.Data} ]", MessageAction);
                        Proc.CancelProcess((Process)sender, () => { StartEnabled = true; StopEnabled = false; });
                        return;
                    }
                    if (data.TotalTime == default)
                    {
                        var regexTotalTime = Regex.Matches(args.Data, Const.Command.Video.Interpolation.FFmpegIn1Out2);
                        if (regexTotalTime.Count == 0) return;
                        data.TotalTime = TimeSpan.Parse(regexTotalTime[0].Groups[1].Value);
                    }
                    if (data.FormFPS == default)
                    {
                        var regexFormFPS = Regex.Matches(args.Data, Const.Command.Video.Interpolation.FFmpegIn1Out3);
                        if (regexFormFPS.Count == 0) return;
                        data.FormFPS = double.Parse(regexFormFPS[0].Groups[1].Value);
                    }
                    var regexCurrentTime = Regex.Matches(args.Data, Const.Command.Video.Interpolation.FFmpegIn1Out4);
                    if (regexCurrentTime.Count == 0) return;
                    data.CurrentTime = TimeSpan.Parse(regexCurrentTime[0].Groups[1].Value);
                    await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 0, (double)data.CurrentTime.Ticks / data.TotalTime.Ticks));
                });
                await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 0, 1));
                await Logger.AddSuccess($"[{inputFile}] 文件处理完成[ 解析帧到文件夹 ] [{outputDirectoryInput}]", MessageAction);
                if (!StopEnabled) throw new Exception("已停止操作");
            }
            catch (Exception ex)
            {
                await Logger.AddError($"[{inputFile}] 文件处理失败[ 解析帧到文件夹 ] [{outputDirectoryInput}] [{ex.Message}]", MessageAction);
                Directory.Delete(outputDirectory, true);
                return;
            }

            var gpu = GetGpu();
            var thread = GetThread();

            data.Scale = GetScale();
            data.ToFPS = Math.Round(data.FormFPS) * data.Scale;
            data.FormTotalFrame = Directory.GetFiles(outputDirectoryInput).Length;
            data.ToTotalFrame = data.FormTotalFrame * data.Scale;

            var outputDirectoryOutput = $"{outputDirectory}{Separator}output";
            Directory.CreateDirectory(outputDirectoryOutput);

            try
            {
                await Logger.AddSuccess($"[{outputDirectoryInput}] 文件处理开始[ 处理帧到文件夹 ] [{outputDirectoryOutput}]", MessageAction);
                var command = string.Format(Const.Command.Video.Interpolation.RifeIn, outputDirectoryInput, outputDirectoryOutput, gpu, thread, data.ToTotalFrame, "rife-v4.6");
                await Proc.StartProcess(Setting.RifePath, command, null, async (sender, args) =>
                {
                    if (!StopEnabled)
                    {
                        Proc.CancelProcess((Process)sender, () => { StartEnabled = true; StopEnabled = false; });
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    if (Regex.IsMatch(args.Data, Const.Command.Video.Interpolation.RifeOut1))
                    {
                        await Logger.AddError($"出现错误：[ {args.Data} ]", MessageAction);
                        Proc.CancelProcess((Process)sender, () => { StartEnabled = true; StopEnabled = false; });
                        return;
                    }
                    var regexPercent = Regex.Matches(args.Data, Const.Command.Video.Interpolation.RifeOut2);
                    if (regexPercent.Count == 0) return;
                    data.State = regexPercent[0].Groups[1].Value;
                    if (data.State == "done") data.CurrentFrame++;
                    await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 1, (double)data.CurrentFrame / data.ToTotalFrame));
                });
                await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 1, 1));
                await Logger.AddSuccess($"[{outputDirectoryInput}] 文件处理完成[ 处理帧到文件夹 ] [{outputDirectoryOutput}]", MessageAction);
                if (!StopEnabled) throw new Exception("已停止操作");
            }
            catch (Exception ex)
            {
                await Logger.AddError($"[{outputDirectoryInput}] 文件处理失败[ 处理帧到文件夹 ] [{outputDirectoryOutput}] [{ex.Message}]", MessageAction);
                Directory.Delete(outputDirectory, true);
                return;
            }

            try
            {
                await Logger.AddSuccess($"[{outputDirectoryOutput}] 文件处理开始[ 合并帧到文件 ] [{outputFile}]", MessageAction);
                var command = string.Format(Const.Command.Video.Interpolation.FFmpegIn2, data.ToFPS, outputDirectoryOutput, inputFile, data.ToFPS, outputFile);
                await Proc.StartProcess(Setting.FFmpegPath, command, null, async (sender, args) =>
                {
                    if (!StopEnabled)
                    {
                        Proc.CancelProcess((Process)sender, () => { StartEnabled = true; StopEnabled = false; });
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                    if (Regex.IsMatch(args.Data, Const.Command.Video.Interpolation.FFmpegIn2Out1))
                    {
                        await Logger.AddError($"出现错误：[ {args.Data} ]", MessageAction);
                        Proc.CancelProcess((Process)sender, () => { StartEnabled = true; StopEnabled = false; });
                        return;
                    }
                    if (data.TotalTime == default)
                    {
                        var regexTotalTime = Regex.Matches(args.Data, Const.Command.Video.Interpolation.FFmpegIn2Out2);
                        if (regexTotalTime.Count == 0) return;
                        data.TotalTime = TimeSpan.Parse(regexTotalTime[0].Groups[1].Value);
                    }
                    var regexCurrentTime = Regex.Matches(args.Data, Const.Command.Video.Interpolation.FFmpegIn2Out3);
                    if (regexCurrentTime.Count == 0) return;
                    data.CurrentTime = TimeSpan.Parse(regexCurrentTime[0].Groups[1].Value);
                    await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 2, (double)data.CurrentTime.Ticks / data.TotalTime.Ticks));
                });
                await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 2, 1));
                await Logger.AddSuccess($"[{outputDirectoryOutput}] 文件处理完成[ 合并帧到文件 ] [{outputFile}]", MessageAction);
                if (!StopEnabled) throw new Exception("已停止操作");
            }
            catch (Exception ex)
            {
                await Logger.AddError($"[{outputDirectoryOutput}] 文件处理失败[ 合并帧到文件 ] [{outputFile}] [{ex.Message}]", MessageAction);
                File.Delete(outputFile);
                return;
            }
            finally
            {
                Directory.Delete(outputDirectory, true);
            }
        }

        public Task AddProcess(int process)
        {
            return Task.Run(() =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    ProgressBarValue = process;
                });
            });
        }
    }
}