using General.Apt.App.ViewModels.App;
using System.Windows.Controls;

namespace General.Apt.App.Views.App
{
    /// <summary>
    /// HelpView.xaml 的交互逻辑
    /// </summary>
    public partial class HelpView : UserControl
    {
        public HelpViewModel HelpViewModel => DataContext as HelpViewModel;

        public HelpView()
        {
            InitializeComponent();
            InitializeData();
        }

        public void InitializeData()
        {
            HelpViewModel.MessageAction += (message) =>
            {
                Message.Document = message;
            };
        }
    }
}
