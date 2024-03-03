using General.Apt.App.Models;
using General.Apt.App.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace General.Apt.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _appTitle;
        public string AppTitle
        {
            get { return _appTitle; }
            set
            {
                _appTitle = value;
                OnPropertyChanged(nameof(AppTitle));
            }
        }

        private ObservableCollection<NavigateItem> _navigateSource;
        public ObservableCollection<NavigateItem> NavigateSource
        {
            get { return _navigateSource; }
            set
            {
                _navigateSource = value;
                OnPropertyChanged(nameof(NavigateSource));
            }
        }

        private NavigateItem _navigateItem;
        public NavigateItem NavigateItem
        {
            get { return _navigateItem; }
            set
            {
                _navigateItem = value;
                OnPropertyChanged(nameof(NavigateItem));
                NavigateItemChanged(_navigateItem);
            }
        }

        public string Navigate
        {
            get
            {
                return NavigateItem.Code.ToString();
            }
            set
            {
                NavigateItem = NavigateSource.FirstOrDefault(e => e.Code.ToString() == value);
            }
        }

        public void NavigateItemChanged(NavigateItem navigateItem)
        {
            NavigateFrame(navigateItem?.Uri);
        }

        private Uri _frameSource;
        public Uri FrameSource
        {
            get { return _frameSource; }
            set
            {
                _frameSource = value;
                OnPropertyChanged(nameof(FrameSource));
            }
        }


        public ICommand NavigateCommand => new CommandBase(e =>
        {
            //var listView = e as ListView;
            //var selectedItem = listView.SelectedItem as NavigateItem;
            //var uri = selectedItem.Uri;
            //NavigateFrame(uri);
        });

        public MainViewModel()
        {
            AppTitle = $"AI 生产力工具 V{Assembly.GetExecutingAssembly().GetName().Version}";
            NavigateSource = new ObservableCollection<NavigateItem>()
            {
                new NavigateItem() { Code="VideoOrganization", Name = "视频整理", Uri = "/Views/Video/Organization/IndexView.xaml" },
                new NavigateItem() { Code="ImageRestoration", Name = "图片放大", Uri = "/Views/Image/Restoration/IndexView.xaml" },
                new NavigateItem() { Code="VideoRestoration", Name = "视频放大", Uri = "/Views/Video/Restoration/IndexView.xaml" },
                new NavigateItem() { Code="VideoInterpolation", Name = "视频补帧", Uri = "/Views/Video/Interpolation/IndexView.xaml" },
                new NavigateItem() { Code="Help", Name = "帮助说明", Uri = "/Views/App/HelpView.xaml" }
            };
            Navigate = "VideoOrganization";
        }

        public void NavigateFrame(string uri)
        {
            if (!string.IsNullOrEmpty(uri))
            {
                FrameSource = new Uri(uri, UriKind.Relative);
            }
        }
    }
}