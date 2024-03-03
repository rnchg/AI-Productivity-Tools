using General.Apt.App.Extensions;
using General.Apt.App.Models;
using General.Apt.App.Models.Video.Organization.Android;
using General.Apt.App.Models.Video.Organization.Pc;
using General.Apt.App.Models.Video.Organization.Uwp;
using General.Apt.App.Utility;
using General.Apt.App.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace General.Apt.App.ViewModels.Video.Organization
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

        private ObservableCollection<ComBoBoxItem<string>> _clientSource;
        public ObservableCollection<ComBoBoxItem<string>> ClientSource
        {
            get { return _clientSource; }
            set
            {
                _clientSource = value;
                OnPropertyChanged(nameof(ClientSource));
            }
        }

        private ComBoBoxItem<string> _clientItem;
        public ComBoBoxItem<string> ClientItem
        {
            get { return _clientItem; }
            set
            {
                _clientItem = value;
                OnPropertyChanged(nameof(ClientItem));
            }
        }

        public string Client
        {
            get
            {
                return ClientItem.Value;
            }
            set
            {
                ClientItem = ClientSource.FirstOrDefault(e => e.Value == value);
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
            ClientSource = new ObservableCollection<ComBoBoxItem<string>>()
            {
                new ComBoBoxItem<string>() { Text="哔哩哔哩 UWP", Value="UWP" },
                new ComBoBoxItem<string>() { Text="哔哩哔哩 Windows", Value="Windows" },
                new ComBoBoxItem<string>() { Text="哔哩哔哩 Android", Value="Android" }
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
                    message += "视频整理：一键批量（解密、合并、整理）B站下载视频。\r\n";
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    message += "技术支持：\r\n";
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    message += "联系方式 [ QQ: 6085398 ] [ Email: Rnchg@Hotmail.com ] [ B站; 风轻云也净 ]。\r\n";
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    message += "操作说明：\r\n";
                    message += "-------------------------------------------------------------------------------------------------------------------------------\r\n";
                    message += "操作步骤：选择输入目录→选择输出目录→选择程序类型→点击开始按钮→等待处理完成。\r\n";
                    message += "输入目录：处理前文件所在文件夹路径。\r\n";
                    message += "输出目录：处理后文件所在文件夹路径。\r\n";
                    message += "程序类型：支持哔哩哔哩（BiliBili）官方客户端（UWP/Windows/Android）。\r\n";
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
                var clientItem = ClientItem?.Value;
                if ((clientItem == "Windows" || clientItem == "Android") && !File.Exists(Setting.FFmpegPath))
                {
                    throw new Exception($"请检查依赖目录 [ {Setting.FFmpegPath} ] ");
                }

                StartEnabled = false;
                StopEnabled = true;

                switch (clientItem)
                {
                    case "UWP":
                        await GetUwp();
                        break;
                    case "Windows":
                        await GetPc();
                        break;
                    case "Android":
                        await GetAndroid();
                        break;
                    default:
                        throw new Exception("客户端错误");
                }

                Dialog.ShowInformationDialog("操作完成");
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

        public async Task GetUwp()
        {
            var files = Directory.GetFiles(Input, "*", SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
            {
                throw new Exception($"待处理文件不存在 [{Input}]");
            }
            var exts = new[] { ".dvi" };
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
                    await Uwp(file, i, files.Length);
                }
                else
                {
                    await Logger.AddError($"待处理文件丢失 [{file}]", MessageAction);
                }
                if (!StopEnabled) throw new Exception("已停止操作");
            }
        }

        public async Task Uwp(string inputFile, int currentFile, int totalFiles)
        {
            var inputDirectory = Path.GetDirectoryName(inputFile);

            var coverFile = $"{inputDirectory}{Separator}cover.jpg";

            var dvi = File.ReadAllText(inputFile).JsonTo<Dvi>();
            var userName = Replace.ReplaceInvalidFileNameChars(dvi.Uploader ?? dvi.Mid);
            var fileName = Replace.ReplaceInvalidFileNameChars(dvi.Title);

            var outputDirectory = $"{Output}{Separator}{userName}{Separator}{fileName}";

            await Logger.AddSuccess($"[{inputFile}] 文件处理完成", MessageAction);
            await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 0, 1));

            var directories = Directory.GetDirectories(inputDirectory, "*", SearchOption.TopDirectoryOnly);

            foreach (var directory in directories)
            {
                var videoFile = Directory.GetFiles(directory, "*.mp4", SearchOption.TopDirectoryOnly).FirstOrDefault();
                if (!File.Exists(videoFile))
                {
                    await Logger.AddError($"[{directory}{Separator}*.mp4] 文件不存在", MessageAction);
                    return;
                }

                var directoryName = directory.Substring(directory.LastIndexOf(Separator) + 1);

                Directory.CreateDirectory(outputDirectory);

                var videoNewFile = $"{outputDirectory}{Separator}{directoryName}.{fileName}.mp4";

                if (File.Exists(coverFile))
                {
                    var coverNewFile = $"{outputDirectory}{Separator}{directoryName}.cover.jpg";
                    File.Copy(coverFile, coverNewFile, true);
                }
                try
                {
                    using (var videoStream = File.OpenRead(videoFile))
                    {
                        using (var videoNewStream = File.OpenWrite(videoNewFile))
                        {
                            var buffer = new byte[1024 * 1024];
                            videoStream.Seek(3, SeekOrigin.Begin);
                            while (true)
                            {
                                var readLength = videoStream.Read(buffer, 0, buffer.Length);
                                videoNewStream.Write(buffer, 0, readLength);
                                if (readLength < buffer.Length) break;
                            }
                        }
                    }
                    await Logger.AddSuccess($"[{videoFile}] 文件解密完成 [{videoNewFile}]", MessageAction);
                    await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 1, 1));
                    await Logger.AddSuccess($"[{videoFile}] 文件处理完成 [{videoNewFile}]", MessageAction);
                    await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 2, 1));
                }
                catch (Exception ex)
                {
                    await Logger.AddError($"[{videoFile}] 文件处理失败 [{videoNewFile}] [{ex.Message}]", MessageAction);
                    File.Delete(videoNewFile);
                    return;
                }
            }
        }

        public async Task GetPc()
        {
            var files = Directory.GetFiles(Input, "*", SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
            {
                throw new Exception($"待处理文件不存在 [{Input}]");
            }
            var exts = new[] { ".videoinfo" };
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
                    await Pc(file, i, files.Length);
                }
                else
                {
                    await Logger.AddError($"待处理文件丢失 [{file}]", MessageAction);
                }
                if (!StopEnabled) throw new Exception("已停止操作");
            }
        }

        public async Task Pc(string inputFile, int currentFile, int totalFiles)
        {
            var inputDirectory = Path.GetDirectoryName(inputFile);

            var imageFile = $"{inputDirectory}{Separator}image.jpg";
            var groupFile = $"{inputDirectory}{Separator}group.jpg";

            var videoinfo = File.ReadAllText(inputFile).JsonTo<VideoInfo>();
            var userName = Replace.ReplaceInvalidFileNameChars(videoinfo.uname ?? videoinfo.uid);
            var fileName = Replace.ReplaceInvalidFileNameChars(videoinfo.title);

            var outputDirectory = $"{Output}{Separator}{userName}{Separator}{fileName}";

            await Logger.AddSuccess($"[{inputFile}] 文件处理完成", MessageAction);
            await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 6, 0, 1));

            var playurlFile = $"{inputDirectory}{Separator}.playurl";
            if (!File.Exists(playurlFile)) return;

            var playurl = File.ReadAllText(playurlFile).JsonTo<PlayUrl>();
            var videoName = Path.GetFileName(playurl.data.dash.video.First().baseUrl.Split('?')[0]);
            var audioName = Path.GetFileName(playurl.data.dash.audio.First().baseUrl.Split('?')[0]);

            var videoFile = $"{inputDirectory}{Separator}{videoName}";
            if (!File.Exists(videoFile))
            {
                await Logger.AddError($"[{videoFile}] 文件不存在", MessageAction);
                return;
            }
            var audioFile = $"{inputDirectory}{Separator}{audioName}";
            if (!File.Exists(audioFile))
            {
                await Logger.AddError($"[{audioFile}] 文件不存在", MessageAction);
                return;
            }
            await Logger.AddSuccess($"[{playurlFile}] 文件处理完成", MessageAction);
            await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 6, 1, 1));

            Directory.CreateDirectory(outputDirectory);

            var videoTempFile = $"{outputDirectory}{Separator}{videoName.Insert(videoName.IndexOf('.'), "_temp")}";
            try
            {
                using (var videoStream = File.OpenRead(videoFile))
                {
                    using (var videoNewStream = File.OpenWrite(videoTempFile))
                    {
                        var buffer = new byte[1024 * 1024];
                        videoStream.Seek(9, SeekOrigin.Begin);
                        while (true)
                        {
                            var readLength = videoStream.Read(buffer, 0, buffer.Length);
                            videoNewStream.Write(buffer, 0, readLength);
                            if (readLength < buffer.Length) break;
                        }
                    }
                }
                await Logger.AddSuccess($"[{videoFile}] 文件解密完成 [{videoTempFile}]", MessageAction);
                await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 6, 2, 1));
            }
            catch (Exception ex)
            {
                await Logger.AddError($"[{videoFile}] 文件解密失败 [{videoTempFile}] [{ex.Message}]", MessageAction);
                File.Delete(videoTempFile);
                return;
            }

            var audioTempFile = $"{outputDirectory}{Separator}{audioName.Insert(audioName.IndexOf('.'), "_temp")}";
            try
            {
                using (var audioStream = File.OpenRead(audioFile))
                {
                    using (var audioNewStream = File.OpenWrite(audioTempFile))
                    {
                        var buffer = new byte[1024 * 1024];
                        audioStream.Seek(9, SeekOrigin.Begin);
                        while (true)
                        {
                            var readLength = audioStream.Read(buffer, 0, buffer.Length);
                            audioNewStream.Write(buffer, 0, readLength);
                            if (readLength < buffer.Length) break;
                        }
                    }
                }
                await Logger.AddSuccess($"[{audioFile}] 文件解密完成 [{audioTempFile}]", MessageAction);
                await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 6, 3, 1));
            }
            catch (Exception ex)
            {
                await Logger.AddError($"[{audioFile}] 文件解密失败 [{audioTempFile}] [{ex.Message}]", MessageAction);
                File.Delete(audioTempFile);
                return;
            }

            var outputFile = $"{outputDirectory}{Separator}{fileName}.mp4";

            if (File.Exists(imageFile))
            {
                var coverNewFile = $"{outputDirectory}{Separator}image.jpg";
                File.Copy(imageFile, coverNewFile, true);
            }
            if (File.Exists(groupFile))
            {
                var coverNewFile = $"{outputDirectory}{Separator}group.jpg";
                File.Copy(groupFile, coverNewFile, true);
            }
            try
            {
                var command = string.Format(Const.Command.Video.Organization.PcFFmpegIn, videoTempFile, audioTempFile, outputFile);
                await Proc.StartProcess(Setting.FFmpegPath, command, null, (sender, args) =>
                {
                    if (!StopEnabled)
                    {
                        ProcessCancel((Process)sender);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(args.Data)) return;
                });
                await Logger.AddSuccess($"[{outputFile}] 文件合并完成", MessageAction);
                await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 6, 4, 1));
                await Logger.AddSuccess($"[{outputFile}] 文件处理完成", MessageAction);
                await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 6, 5, 1));
            }
            catch (Exception ex)
            {
                await Logger.AddError($"[{outputFile}] 文件合并失败 [{ex.Message}]", MessageAction);
                File.Delete(outputFile);
                return;
            }
            finally
            {
                File.Delete(videoTempFile);
                File.Delete(audioTempFile);
            }
        }

        public async Task GetAndroid()
        {
            var files = Directory.GetFiles(Input, "*", SearchOption.AllDirectories);
            if (files == null || files.Length == 0)
            {
                throw new Exception($"待处理文件不存在 [{Input}]");
            }
            var fileNames = new[] { "entry.json" };
            files = files.Where(e => fileNames.Contains(Path.GetFileName(e).ToLower())).ToArray();
            if (files == null || files.Length == 0)
            {
                throw new Exception($"符合条件的文件不存在 [ {string.Join("|", fileNames.Select(e => $"*{e}"))} ]");
            }
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                if (File.Exists(file))
                {
                    await Android(file, i, files.Length);
                }
                else
                {
                    await Logger.AddError($"待处理文件丢失 [{file}]", MessageAction);
                }
                if (!StopEnabled) throw new Exception("已停止操作");
            }
        }

        public async Task Android(string inputFile, int currentFile, int totalFiles)
        {
            var inputDirectory = Path.GetDirectoryName(inputFile);

            var entry = File.ReadAllText(inputFile).JsonTo<Entry>();
            var userName = Replace.ReplaceInvalidFileNameChars(string.IsNullOrWhiteSpace(entry.owner_name) ? entry.owner_id.ToString() : entry.owner_name);
            var fileName = Replace.ReplaceInvalidFileNameChars(string.IsNullOrWhiteSpace(entry.page_data?.download_subtitle) ? entry.title : entry.page_data?.download_subtitle);
            await Logger.AddSuccess($"[{inputFile}] 文件处理完成", MessageAction);
            await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 0, 1));

            var directories = Directory.GetDirectories(inputDirectory, "*", SearchOption.TopDirectoryOnly);

            foreach (var directory in directories)
            {
                var directoryName = directory.Substring(directory.LastIndexOf(Separator) + 1);

                var outputDirectory = $"{Output}{Separator}{userName}{Separator}{fileName}";

                var indexFile = $"{directory}{Separator}index.json";
                if (!File.Exists(indexFile)) return;

                var isBlv = false;
                var blvFile = $"{directory}{Separator}0.blv";
                if (File.Exists(blvFile))
                {
                    isBlv = true;
                }

                var videoFile = string.Empty;
                var audioFile = string.Empty;
                if (!isBlv)
                {
                    videoFile = $"{directory}{Separator}video.m4s";
                    if (!File.Exists(videoFile))
                    {
                        await Logger.AddError($"[{videoFile}] 文件不存在", MessageAction);
                        return;
                    }
                    audioFile = $"{directory}{Separator}audio.m4s";
                    if (!File.Exists(audioFile))
                    {
                        await Logger.AddError($"[{audioFile}] 文件不存在", MessageAction);
                        return;
                    }
                }

                Directory.CreateDirectory(outputDirectory);

                var outputFile = $"{outputDirectory}{Separator}{fileName}.mp4";
                try
                {
                    var command = string.Empty;
                    if (isBlv)
                    {
                        command = string.Format(Const.Command.Video.Organization.AndroidFFmpegIn1, blvFile, outputFile);
                    }
                    else
                    {
                        command = string.Format(Const.Command.Video.Organization.AndroidFFmpegIn2, videoFile, audioFile, outputFile);
                    }
                    await Proc.StartProcess(Setting.FFmpegPath, command, null, (sender, args) =>
                    {
                        if (!StopEnabled)
                        {
                            ProcessCancel((Process)sender);
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(args.Data)) return;
                    });
                    await Logger.AddSuccess($"[{outputFile}] 文件合并完成", MessageAction);
                    await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 1, 1));
                    await Logger.AddSuccess($"[{outputFile}] 文件处理完成", MessageAction);
                    await AddProcess(Progress.GetProgress(ProgressBarMaximum, totalFiles, currentFile, 3, 2, 1));
                }
                catch (Exception ex)
                {
                    await Logger.AddError($"[{outputFile}] 文件合并失败 [{ex.Message}]", MessageAction);
                    File.Delete(outputFile);
                    return;
                }
            }
        }

        public void ProcessCancel(Process process, bool waitForExit = false)
        {
            if (!process.HasExited)
            {
                process.Kill();
                if (waitForExit)
                {
                    process.WaitForExit();
                }
            }
            StartEnabled = true;
            StopEnabled = false;
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