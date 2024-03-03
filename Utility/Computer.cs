using General.Apt.App.Models;
using System.Collections.ObjectModel;
using System.Management;

namespace General.Apt.App.Utility
{
    public class Computer
    {
        public static ObservableCollection<ComBoBoxItem<string>> GetGPU()
        {
            var result = new ObservableCollection<ComBoBoxItem<string>>();
            var managementObjectSearcher = new ManagementObjectSearcher("Select * from Win32_VideoController");
            var count = 0;
            foreach (var mo in managementObjectSearcher.Get())
            {
                result.Add(new ComBoBoxItem<string>() { Text = mo["Name"].ToString(), Value = count.ToString() });
                count++;
            }
            managementObjectSearcher.Dispose();
            return result;
        }
    }
}
