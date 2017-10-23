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

//using Bluetooth;

namespace Fibeco
{
    public partial class MainPage : PhoneApplicationPage
    {
        // The database path.  
      //  public static string DB_PATH = "Fibeco.db";
        public static string DB_PATH = "Fibeco.db";  
        // The sqlite connection.  
        private SQLiteConnection dbConn;  
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        public static string fname,lname,uaddress,gender,profilepic,isfirsttime;

        

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            var mm = MessageBox.Show("Do you wish to exit the app", "Exit", MessageBoxButton.OKCancel);
            if (mm == MessageBoxResult.OK)
            {
                Application.Current.Terminate();
            }

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
            // Create the database connection.  
            dbConn = new SQLiteConnection(DB_PATH);
            // Create the table Task, if it doesn't exist.  
           // dbConn.CreateTable<User>();
           /* User task = new User()
            {
                username = "sen",
                password = "sen"
                
            };*/
            // Insert the new task in the Task table.  
            //dbConn.Insert(task);  
          
            var tp = dbConn.Query<User>("select * from user").FirstOrDefault();
            if (tp == null)
            {
                MessageBox.Show("Account Not Found");
            }
            else
            {
                isfirsttime = tp.isfirsttime;
                if (isfirsttime == "T")
                {
                    NavigationService.Navigate(new Uri("/FirstTime.xaml", UriKind.Relative));
                    return;
                }
            }
           
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (dbConn != null)
                dbConn.Close();     // Close the database connection.  
           
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var tp = dbConn.Query<User>("select * from user where username='" + txtun.Text + "' and password ='" + txtpw.Password + "'").FirstOrDefault();
          
            if (tp == null)
            {
                MessageBox.Show("Account Not Found");
            }
            else
            {
                
                txtun.Text = "";
                txtpw.Password = "";
                fname = tp.fname;
                lname = tp.lname;
                gender = tp.gender;
                uaddress = tp.address;
                profilepic = tp.profilepic;
                isfirsttime = tp.isfirsttime;

                if (isfirsttime == "T")
                {
                    NavigationService.Navigate(new Uri("/FirstTime.xaml", UriKind.Relative));
                }
                else
                {
                    NavigationService.Navigate(new Uri("/Menu.xaml?fname=" + fname + "&lname=" + lname + "&gender=" + gender + "&address=" + uaddress + "&profilepic=" + profilepic + "&isfirsttime=" + isfirsttime , UriKind.Relative));
                }
                
            }
                 
               
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
       //     NavigationService.Navigate(new Uri("/Bluetooth.xaml", UriKind.Relative));
            //
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            fname = "Sen";
            lname = "Panganiban";
            gender = "Male";
            uaddress = "Musuan, Maramag Bukidnon";
            profilepic = "/profilepic/default.png";
            isfirsttime = "F";

            NavigationService.Navigate(new Uri("/Menu.xaml?fname=" + fname + "&lname=" + lname + "&gender=" + gender + "&address=" + uaddress + "&profilepic=" + profilepic + "&isfirsttime=" + isfirsttime, UriKind.Relative));
        }

        private void logo_click(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MessageBox.Show("Fibeco");
        }




    }
}