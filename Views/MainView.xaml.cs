using General.Apt.App.Utility;
using General.Apt.App.ViewModels;
using General.Apt.App.ViewModels.App;
using General.Apt.App.Views.App;
using System.Windows;

namespace General.Apt.App.Views
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView : Window
    {
        public MainViewModel MainViewModel => DataContext as MainViewModel;
        public ViewModels.Video.Organization.IndexViewModel VideoOrganizationViewModel => (VideoOrganization.Content as Views.Video.Organization.IndexView).DataContext as ViewModels.Video.Organization.IndexViewModel;
        public ViewModels.Image.Restoration.IndexViewModel ImageRestorationViewModel => (ImageRestoration.Content as Views.Image.Restoration.IndexView).DataContext as ViewModels.Image.Restoration.IndexViewModel;
        public ViewModels.Video.Restoration.IndexViewModel VideoRestorationViewModel => (VideoRestoration.Content as Views.Video.Restoration.IndexView).DataContext as ViewModels.Video.Restoration.IndexViewModel;
        public ViewModels.Video.Interpolation.IndexViewModel VideoInterpolationViewModel => (VideoInterpolation.Content as Views.Video.Interpolation.IndexView).DataContext as ViewModels.Video.Interpolation.IndexViewModel;
        public HelpViewModel HelpViewModel => (Help.Content as HelpView).DataContext as HelpViewModel;

        public MainView()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            Loaded += (s, e) =>
            {
                VideoOrganizationViewModel.Input = Setting.Config.Video.Organization.Input;
                VideoOrganizationViewModel.Output = Setting.Config.Video.Organization.Output;
                VideoOrganizationViewModel.Client = Setting.Config.Video.Organization.Client;

                ImageRestorationViewModel.Input = Setting.Config.Image.Restoration.Input;
                ImageRestorationViewModel.Output = Setting.Config.Image.Restoration.Output;
                ImageRestorationViewModel.Gpu = Setting.Config.Image.Restoration.Gpu;
                ImageRestorationViewModel.Decode = Setting.Config.Image.Restoration.Decode;
                ImageRestorationViewModel.Handle = Setting.Config.Image.Restoration.Amplify;
                ImageRestorationViewModel.Encode = Setting.Config.Image.Restoration.Encode;
                ImageRestorationViewModel.Model = Setting.Config.Image.Restoration.Model;
                ImageRestorationViewModel.Scale = Setting.Config.Image.Restoration.Scale;

                VideoRestorationViewModel.Input = Setting.Config.Video.Restoration.Input;
                VideoRestorationViewModel.Output = Setting.Config.Video.Restoration.Output;
                VideoRestorationViewModel.Gpu = Setting.Config.Video.Restoration.Gpu;
                VideoRestorationViewModel.Decode = Setting.Config.Video.Restoration.Decode;
                VideoRestorationViewModel.Handle = Setting.Config.Video.Restoration.Amplify;
                VideoRestorationViewModel.Encode = Setting.Config.Video.Restoration.Encode;
                VideoRestorationViewModel.Model = Setting.Config.Video.Restoration.Model;
                VideoRestorationViewModel.Scale = Setting.Config.Video.Restoration.Scale;

                VideoInterpolationViewModel.Input = Setting.Config.Video.Interpolation.Input;
                VideoInterpolationViewModel.Output = Setting.Config.Video.Interpolation.Output;
                VideoInterpolationViewModel.Gpu = Setting.Config.Video.Interpolation.Gpu;
                VideoInterpolationViewModel.Decode = Setting.Config.Video.Interpolation.Decode;
                VideoInterpolationViewModel.Handle = Setting.Config.Video.Interpolation.Amplify;
                VideoInterpolationViewModel.Encode = Setting.Config.Video.Interpolation.Encode;
                VideoInterpolationViewModel.Scale = Setting.Config.Video.Interpolation.Scale;
            };
            Closed += (s, e) =>
            {
                Setting.Config.Video.Organization.Input = VideoOrganizationViewModel.Input;
                Setting.Config.Video.Organization.Output = VideoOrganizationViewModel.Output;
                Setting.Config.Video.Organization.Client = VideoOrganizationViewModel.Client;

                Setting.Config.Image.Restoration.Input = ImageRestorationViewModel.Input;
                Setting.Config.Image.Restoration.Output = ImageRestorationViewModel.Output;
                Setting.Config.Image.Restoration.Gpu = ImageRestorationViewModel.Gpu;
                Setting.Config.Image.Restoration.Decode = ImageRestorationViewModel.Decode;
                Setting.Config.Image.Restoration.Amplify = ImageRestorationViewModel.Handle;
                Setting.Config.Image.Restoration.Encode = ImageRestorationViewModel.Encode;
                Setting.Config.Image.Restoration.Model = ImageRestorationViewModel.Model;
                Setting.Config.Image.Restoration.Scale = ImageRestorationViewModel.Scale;

                Setting.Config.Video.Restoration.Input = VideoRestorationViewModel.Input;
                Setting.Config.Video.Restoration.Output = VideoRestorationViewModel.Output;
                Setting.Config.Video.Restoration.Gpu = VideoRestorationViewModel.Gpu;
                Setting.Config.Video.Restoration.Decode = VideoRestorationViewModel.Decode;
                Setting.Config.Video.Restoration.Amplify = VideoRestorationViewModel.Handle;
                Setting.Config.Video.Restoration.Encode = VideoRestorationViewModel.Encode;
                Setting.Config.Video.Restoration.Model = VideoRestorationViewModel.Model;
                Setting.Config.Video.Restoration.Scale = VideoRestorationViewModel.Scale;

                Setting.Config.Video.Interpolation.Input = VideoInterpolationViewModel.Input;
                Setting.Config.Video.Interpolation.Output = VideoInterpolationViewModel.Output;
                Setting.Config.Video.Interpolation.Gpu = VideoInterpolationViewModel.Gpu;
                Setting.Config.Video.Interpolation.Decode = VideoInterpolationViewModel.Decode;
                Setting.Config.Video.Interpolation.Amplify = VideoInterpolationViewModel.Handle;
                Setting.Config.Video.Interpolation.Encode = VideoInterpolationViewModel.Encode;
                Setting.Config.Video.Interpolation.Scale = VideoInterpolationViewModel.Scale;

                Proc.Clear();
            };
        }
    }
}
