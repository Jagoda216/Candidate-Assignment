using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client.ViewModel;
using System.Windows;

namespace Client.View
{
    public partial class MainView : Window
    {
		public MainView()
		{
			InitializeComponent();
		}
        
		public MainView(string username, string token)
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(username, token);
        }
    }
}
