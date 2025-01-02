using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModel;
using System.Windows;

namespace Client.View
{
    /// <summary>
    /// Logic for MainView window
    /// Binds MainView with MainViewModel
    /// </summary>
    public partial class MainView : Window
    {
        /// <summary>
        /// Initializes a new instance MainView
        /// </summary>
		public MainView()
		{
			InitializeComponent();
		}
        /// <summary>
        /// Initializes a new instance MainView with arguments
        /// Constructor accpets user's username and session token
        /// Sets the DataContext to a new instance
        /// </summary>
        /// <param name="username">User's username</param>
        /// <param name="token">User's session token</param>
		public MainView(string username, string token)
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(username, token);
        }
    }
}
