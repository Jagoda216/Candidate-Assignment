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
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
        }
        private void OnChangePassword(object sender, EventArgs e)
        {
            var password = sender as PasswordBox;
            if (password != null)
            {
                var loginViewModel = (LoginViewModel)this.DataContext;
                loginViewModel.Password = password.Password;
            }
        }

    }
}
