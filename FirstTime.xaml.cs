using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Fibeco.Resources;
using SQLite;
using System.IO;
using Windows.Storage;
using Windows.Phone.UI.Input;

using Fibeco;

using Windows.Networking.Proximity;
using Windows.Foundation;
using Windows.Networking.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;

using System.Threading.Tasks;
using Windows.Storage.Streams;

using Microsoft.Phone.Tasks;

using BTConnection;

namespace Fibeco
{
    public partial class FirstTime : PhoneApplicationPage
    {
        // The database path.  
        //  public static string DB_PATH = "Fibeco.db";
        public static string DB_PATH = "Fibeco.db";
        // The sqlite connection.  
        private SQLiteConnection dbConn;
        // Constructor
        public FirstTime()
        {
            InitializeComponent();
        }


        public sealed class User
        {
            [PrimaryKey, AutoIncrement]
            public int user_id { get; set; }

            public string username { get; set; }

            public string password { get; set; }

            public string fname { get; set; }

            public string lname { get; set; }

            public string gender { get; set; }

            public string address { get; set; }

            public string profilepic { get; set; }

            public string isfirsttime { get; set; }


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        { 
            dbConn = new SQLiteConnection(DB_PATH);
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (dbConn != null)
                dbConn.Close();     
        }


        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtusername.Text == "" || txtpassword.Password == "" || txtlname.Text == "" || txtfname.Text == "")
            {
                MessageBox.Show("Textbox is Empty");
                return;
            }
            else if (rdmale.IsChecked == false && rdfemale.IsChecked == false)
            {
                MessageBox.Show("Please Select Gender");
                return;
            }
            else
            {
                SQLiteAsyncConnection connection = new SQLiteAsyncConnection(DB_PATH);
                var User = await connection.Table<User>().FirstOrDefaultAsync();

                if (User != null)
                {
                    try
                    {
                        string gendertemp;
                        User.user_id = 1;
                        User.username = txtusername.Text;
                        User.password = txtpassword.Password;
                        User.lname = txtlname.Text;
                        User.fname = txtfname.Text;
                        User.address = "Maramag Bukidnon";
                        if (rdmale.IsChecked == true)
                        {
                            User.gender = "Male";
                            gendertemp = "Male";
                        }
                        else
                        {
                            User.gender = "Female";
                            gendertemp = "Female";
                        }
                        User.isfirsttime = "F";
                        User.profilepic = "DD";

                        await connection.UpdateAsync(User);
                        MessageBox.Show("Success");
                        NavigationService.Navigate(new Uri("/Menu.xaml?fname=" + txtfname.Text + "&lname=" + txtlname.Text + "&gender=" + gendertemp + "&address=Maramag%20Bukidnon&profilepic=DD&isfirsttime=F", UriKind.Relative));
                    }
                    catch (Exception ex)
                    {
                        var final = ex;
                        MessageBox.Show(final.Message);
                    }
                }
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            var mm = MessageBox.Show("Do you wish to exit the app", "Exit", MessageBoxButton.OKCancel);
            if (mm == MessageBoxResult.OK)
            {
                Application.Current.Terminate();
            }
        }

        private void rdfemale_Checked(object sender, RoutedEventArgs e)
        {
            rdmale.IsChecked = false;
        }

        private void rdmale_Checked(object sender, RoutedEventArgs e)
        {
            rdfemale.IsChecked = false;
        }
    }
}