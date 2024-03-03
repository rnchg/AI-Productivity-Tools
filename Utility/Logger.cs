using System;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace General.Apt.App.Utility
{
    public static class Logger
    {
        public static Task AddInformation(string message, Action<Paragraph> action)
        {
            return Task.Run(() =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    message = $"[ {DateTime.Now} ]=>[ {message} ]";
                    var run = new Run(message);
                    var color = (Color)ColorConverter.ConvertFromString(Const.Color.Information);
                    run.Foreground = new SolidColorBrush(color);
                    var paragraph = new Paragraph(run);
                    action.Invoke(paragraph);
                });
            });
        }

        public static Task AddSuccess(string message, Action<Paragraph> action)
        {
            return Task.Run(() =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    message = $"[ {DateTime.Now} ]=>[ {message} ]";
                    var run = new Run(message);
                    var color = (Color)ColorConverter.ConvertFromString(Const.Color.Success);
                    run.Foreground = new SolidColorBrush(color);
                    var paragraph = new Paragraph(run);
                    action.Invoke(paragraph);
                });
            });
        }

        public static Task AddWarning(string message, Action<Paragraph> action)
        {
            return Task.Run(() =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    message = $"[ {DateTime.Now} ]=>[ {message} ]";
                    var run = new Run(message);
                    var color = (Color)ColorConverter.ConvertFromString(Const.Color.Warning);
                    run.Foreground = new SolidColorBrush(color);
                    var paragraph = new Paragraph(run);
                    action.Invoke(paragraph);
                });
            });
        }

        public static Task AddError(string message, Action<Paragraph> action)
        {
            return Task.Run(() =>
            {
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    message = $"[ {DateTime.Now} ]=>[ {message} ]";
                    var run = new Run(message);
                    var color = (Color)ColorConverter.ConvertFromString(Const.Color.Error);
                    run.Foreground = new SolidColorBrush(color);
                    var paragraph = new Paragraph(run);
                    action.Invoke(paragraph);
                });
            });
        }
    }
}