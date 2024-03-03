using System;
using System.Collections.Generic;

namespace General.Apt.App.ViewModels.Base
{
    public class WindowManager
    {
        private static Dictionary<string, Action<object>> _dict = new Dictionary<string, Action<object>>();

        public static void Register(string key, Action<object> action)
        {
            if (!string.IsNullOrEmpty(key) && !_dict.ContainsKey(key))
            {
                _dict.Add(key, action);
            }
        }

        public static void DoAction(string key, object obj)
        {
            if (!string.IsNullOrEmpty(key) && _dict.ContainsKey(key))
            {
                _dict[key]?.Invoke(obj);
            }
        }
    }
}
