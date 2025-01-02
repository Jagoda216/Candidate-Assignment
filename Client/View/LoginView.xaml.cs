using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace Client.View
{
    /// <summary>
    /// Logic for LoginView window
    /// Binds LoginView with LoginViewModel
    /// </summary>
    public partial class LoginView : Window
    {
        /// <summary>
        /// Initializes a new instance LoginView
        /// Sets the DataContext to a new instance
        /// </summary>
        public LoginView()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }

        /// <summary>
        /// Handles changes to the PasswordBox and updates them
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">Event argument</param>
        private void OnChangePassword(object sender, EventArgs e)
        {
            var password = sender as PasswordBox;
            if (password != null)
            {
                //Retrive the current instance of LoginViewModel
                var loginViewModel = (LoginViewModel)this.DataContext;
                //Updates the password in LoginViewModel
                loginViewModel.Password = password.Password;
            }
        }

    }
}
