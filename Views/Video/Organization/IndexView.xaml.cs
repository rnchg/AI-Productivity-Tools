using General.Apt.App.ViewModels.Video.Organization;
using System.Windows.Controls;

namespace General.Apt.App.Views.Video.Organization
{
    /// <summary>
    /// IndexView.xaml 的交互逻辑
    /// </summary>
    public partial class IndexView : UserControl
    {
        public IndexViewModel IndexViewModel => DataContext as IndexViewModel;

        public IndexView()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            Message.Document.Blocks.Clear();

            IndexViewModel.MessageAction += (message) =>
            {
                Message.Document.Blocks.Add(message);
                Message.ScrollToEnd();
                while (Message.Document.Blocks.Count > 100)
                {
                    Message.Document.Blocks.Remove(Message.Document.Blocks.FirstBlock);
                }
            };
            //Loaded += (s, e) =>
            //{
            //    IndexViewModel.Input = Setting.Config.Video.Organization.Input;
            //    IndexViewModel.Output = Setting.Config.Video.Organization.Output;
            //    IndexViewModel.Client = Setting.Config.Video.Organization.Client;
            //};
            //Unloaded += (s, e) =>
            //{
            //    Setting.Config.Video.Organization.Input = IndexViewModel.Input;
            //    Setting.Config.Video.Organization.Output = IndexViewModel.Output;
            //    Setting.Config.Video.Organization.Client = IndexViewModel.Client;
            //};
        }
    }
}