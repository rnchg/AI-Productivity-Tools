using General.Apt.App.ViewModels.Video.Restoration;
using System.Windows.Controls;

namespace General.Apt.App.Views.Video.Restoration
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
            //    IndexViewModel.Input = Setting.Config.Video.Restoration.Input;
            //    IndexViewModel.Output = Setting.Config.Video.Restoration.Output;
            //    IndexViewModel.Model = Setting.Config.Video.Restoration.Model;
            //    IndexViewModel.Scale = Setting.Config.Video.Restoration.Scale;
            //};
            //Unloaded += (s, e) =>
            //{
            //    Setting.Config.Video.Restoration.Input = IndexViewModel.Input;
            //    Setting.Config.Video.Restoration.Output = IndexViewModel.Output;
            //    Setting.Config.Video.Restoration.Model = IndexViewModel.Model;
            //    Setting.Config.Video.Restoration.Scale = IndexViewModel.Scale;
            //};
        }
    }
}