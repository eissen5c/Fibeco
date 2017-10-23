using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using btPrint4wp.Resources;
using System.Text;
using Windows.Networking.Proximity;
using Windows.Foundation;
using Windows.Networking.Sockets;
using System.Runtime.InteropServices.WindowsRuntime;

using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using System.IO;

using Microsoft.Phone.Tasks;

using BTConnection;

namespace Fibeco
{
    public partial class UserProfile : PhoneApplicationPage
    {
        public UserProfile()
        {
            InitializeComponent();
        }
        public static string fname, lname, uaddress, gender, profilepic, isfirsttime;
       
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string fname2, lname2, address2, gender2, profilepic2, isfirsttime2;

            if (NavigationContext.QueryString.TryGetValue("fname", out fname2))
            {
                fname = fname2;
            }

            if (NavigationContext.QueryString.TryGetValue("lname", out lname2))
            {
                lname = lname2;
            }

            if (NavigationContext.QueryString.TryGetValue("address", out address2))
            {
                uaddress = address2;
            }

            if (NavigationContext.QueryString.TryGetValue("profilepic", out profilepic2))
            {
                profilepic = profilepic2;
            }

            if (NavigationContext.QueryString.TryGetValue("isfirsttime", out isfirsttime2))
            {
                isfirsttime = isfirsttime2;
            }

            if (NavigationContext.QueryString.TryGetValue("gender", out gender2))
            {
                gender = gender2;
            }

            lblname.Text = lname + ", " + fname;
            lbladdress.Text = uaddress;
            lblgender.Text = gender;
            lblacctype.Text = "User";
           
           // profilepicture.Source = profilepic;
           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

    }
}