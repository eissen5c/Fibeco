using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using SQLite;

namespace Fibeco
{
    public partial class AccountSettings : PhoneApplicationPage
    {
        public static string DB_PATH = "Fibeco.db";
        // The sqlite connection.  
        private SQLiteConnection dbConn; 

        public AccountSettings()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (dbConn != null)
                dbConn.Close();     // Close the database connection.  

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}