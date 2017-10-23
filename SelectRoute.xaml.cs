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
using System.IO;
using Windows.Storage;
namespace Fibeco
{
    public partial class SelectRoute : PhoneApplicationPage
    {
        List<string> test = new List<String>();
        public static string DB_PATH = "Fibeco.db";
        // The sqlite connection.  
        private SQLiteConnection dbConn;  
        public SelectRoute()
        {
            InitializeComponent();
          
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            dbConn = new SQLiteConnection(DB_PATH);
            // Retrieve the task list from the database.  
            List<route> retrievedTasks = dbConn.Table<route>().ToList<route>();
            // Clear the list box that will show all the tasks.  
            //lstRoute.Items.Clear();
            foreach (var t in retrievedTasks)
            {
                lstRoute.Items.Add(t.route_id.ToString());
            }

            // lstRoute.ItemsSource = retrievedTasks;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (dbConn != null)
                dbConn.Close();     // Close the database connection.  

        }
        public sealed class tests
        {
            [PrimaryKey, AutoIncrement]
            public int id { get; set; }
            public int no { get; set; }
        }

        public sealed class route
        {
            [PrimaryKey, AutoIncrement]
            public int id { get; set; }
            public string route_id { get; set; }
            public string route_name { get; set; }
        }
        public sealed class member_list
        {
            [PrimaryKey, AutoIncrement]
            public int id { get; set; }
            public string meter_no { get; set; }
            public string area { get; set; }
            public int consumer_no { get; set; }
            public string fname { get; set; }
            public string mi {get; set;}
            public string lname { get; set; }
            public string route { get; set; }
            public string sp { get; set; }
            public string barangay { get; set; }
            public string town { get; set; }
            public string sfname { get; set; }
            public string smi { get; set; }
            public string slname { get; set; }
            public string con_type { get; set; }
            public string date_applied { get; set; }
            public string date_range { get; set; }
            public string rec_type { get; set; }
            public string oldname { get; set; }
            public string contact_number { get; set; }
            public string mem_type { get; set; }
            public string acct_no { get; set; }
            public string type { get; set; }

        }
        public sealed class electric_bill
        {
            [PrimaryKey, AutoIncrement]
            public int id { get; set; }
          
            public string bill_no { get; set; }
            public string bill_group { get; set; }
            public string bill_month { get; set; }
            public string energy { get; set; }
            public string kwh_used { get; set; }
            public string demand { get; set; }
            public string total_vat { get; set; }
            public string total_gov_charges { get; set; }
            public string basic_charge { get; set; }
            public string transformer_rental_others { get; set; }
            public string recover_charge { get; set; }
            public string due_date { get; set; }
            public int mult { get; set; }
            public int consumer_no { get; set; }

        }

        private void cmbSelect(object sender, System.Windows.Input.GestureEventArgs e)
        {
              // NavigationService.Navigate(new Uri("/Home.xaml?Route=Test", UriKind.Relative));
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

        private void cmbSelected(object sender, System.Windows.Input.GestureEventArgs e)
        {
            //NavigationService.Navigate(new Uri("/Home.xaml?msg=" +  , UriKind.Relative));
           // MessageBox.Show()
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstSelect(object sender, System.Windows.Input.GestureEventArgs e)
        {
           // MessageBox.Show(lstRoute.SelectedItem.ToString());
        }

        private void routeSelected(object sender, System.Windows.Input.GestureEventArgs e)
        {
           
        }

        private void btnCancel(object sender, RoutedEventArgs e)
        {
             //NavigationService.Navigate(new Uri("/Menu.xaml", UriKind.Relative));
            NavigationService.Navigate(new Uri("/Home.xaml?msg=", UriKind.Relative));
           //NavigationService.GoBack();
        }

        private void btnSelect(object sender, RoutedEventArgs e)
        {
            if (lstRoute.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select ID");
            }
            else
            {
                var mySelectedItem = lstRoute.SelectedItem as String;
                //MessageBox.Show(mySelectedItem.ToString());
                NavigationService.Navigate(new Uri("/Home.xaml?msg=" + mySelectedItem.ToString(), UriKind.Relative));
            }
        }
    }
}