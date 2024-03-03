using General.Apt.App.ViewModels;
using General.Apt.App.ViewModels.App;

namespace General.Apt.App.Utility
{
    public class Locator
    {
        public MainViewModel MainViewModel => new MainViewModel();
        public HelpViewModel HelpViewModel => new HelpViewModel();
        public ViewModels.Video.Organization.IndexViewModel VideoOrganizationViewModel => new ViewModels.Video.Organization.IndexViewModel();
        public ViewModels.Image.Restoration.IndexViewModel ImageRestorationViewModel => new ViewModels.Image.Restoration.IndexViewModel();
        public ViewModels.Video.Restoration.IndexViewModel VideoRestorationViewModel => new ViewModels.Video.Restoration.IndexViewModel();
        public ViewModels.Video.Interpolation.IndexViewModel VideoInterpolationViewModel => new ViewModels.Video.Interpolation.IndexViewModel();
    }
}