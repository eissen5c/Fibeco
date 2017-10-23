using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Fibeco
{
    public partial class Upload : PhoneApplicationPage
    {
        public Upload()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            var mm = MessageBox.Show("Do you wish to exit the app", "Exit", MessageBoxButton.OKCancel);
            if (mm == MessageBoxResult.OK)
            {
                Application.Current.Terminate();
            }
        }
    }
}