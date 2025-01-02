using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client.Services
{
    /// <summary>
    /// Provides for navigating between WPF windows.
    /// </summary>
    public class Navigation
    {
        /// <summary>
        /// Closes the current window and opens a new window
        /// </summary>
        /// <typeparam name="T">Represents a new window</typeparam>
        /// <param name="username">User's username</param>
        /// <param name="token">User's session token</param>
        public void Navigate<T>(string username, string token) where T : Window, new()
        {
            //Creates a new instance of new window
            var newWindow = (T)Activator.CreateInstance(typeof(T), username, token);
            //Create a new instance of the current window
            var currentWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault();
            //Opens a new window
            newWindow.Show();
            //Closes the previous window
            currentWindow?.Close();

        }
    }
}
