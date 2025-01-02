using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Services
{
    public class Navigation
    {
        public void Navigate<T>(string username, string token) where T : Window, new()
        {
            var newWindow = (T)Activator.CreateInstance(typeof(T), username, token);
            var currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault();
            
            newWindow.Show();
            currentWindow?.Close();

        }
    }
}
