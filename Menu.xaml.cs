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
    public partial class Menu : PhoneApplicationPage
    {

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



        }

        public Menu()
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


        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Home.xaml?fname=" + fname + "&lname=" + lname + "&gender=" + gender + "&address=" + uaddress + "&profilepic=" + profilepic + "&isfirsttime=" + isfirsttime, UriKind.Relative));

        }

        private void btnUploadC(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Upload.xaml", UriKind.Relative));
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/btset.xaml", UriKind.Relative));
        }

        private void TestClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btset.btConn.isConnected)
                {
                    printFile();
                }
                else
                {
                    MessageBox.Show("Not Connected");
                    NavigationService.Navigate(new Uri("/btset.xaml", UriKind.Relative));
                }
          
            }
            catch (Exception ex)
            {
                var final = ex;
                MessageBox.Show("Not Connected");
                NavigationService.Navigate(new Uri("/btset.xaml", UriKind.Relative));
            }
            
        }

        void txtalign(string i)
        {
            byte[] cmd = new byte[9];
            cmd[0] = 0x1B;
            cmd[1] = 0x61;

            if (i == "left")
            {
                cmd[2] = 0x00;
            }
            else if (i == "center")
            {
                cmd[2] = 0x01;
            }
            else if (i == "right")
            {
                cmd[2] = 0x02;
            }
            else
            {
                cmd[2] = 0x00;
            }

            //cmd[3] = 0x31;

            try
            {
                btset.btConn.send(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        void printlogo()
        {
            byte[] cmd = new byte[9];
            cmd[0] = 0x1C;
            cmd[1] = 0x70;
            cmd[2] = 0x01;
            cmd[3] = 0x00;

            try
            {
                btset.btConn.send(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void printFile()
        {
           if(btset.btConn.isConnected)
           {
               printlogo();
               string strMsg1 = "\t E L E C T R I C  B I L L \n";
               txtalign("center");
               printdoublesize(strMsg1);
               string strMsg2 = "\t Statement of Account \n";
               txtalign("center");
               printdoublesize(strMsg2);
               string strMsg3 = "Bill Month : 00/0000 \nBill No. : 0000000000 \n";
               txtalign("left");
               printdoublesize(strMsg3);
               string strMsg4 = "------------------------------ \n";
               txtalign("center");
               printnormalsize(strMsg4);
               string strMsg5 = "Name: PANGANIBAN , EISSEN JULE \n";
               txtalign("left");
               printdoublesize(strMsg5);
               string strMsg6 = "Acct No.: 00-0000-0000 0 \n";
               txtalign("left");
               printdoublesize(strMsg6);
               string strMsg7 = "Address: MUSUAN BUKIDNON \n";
               txtalign("left");
               printnormalsize(strMsg7);
               string strMsg8 = "Meter No: 0000000000 \nMult: 0 \n";
               txtalign("left");
               printnormalsize(strMsg8);
               string strMsg9 = "Pres Energy: 0000.0 \nPres Demand: 0.000 \n";
               txtalign("left");
               printnormalsize(strMsg9);
               string strMsg10 = "Prev Energy: 0000.0 \nPrev Demand: 0.000 \n";
               txtalign("left");
               printnormalsize(strMsg10);
               string strMsg11 = "kWH Used: 00.0 \t \t \t \t \t \tDemand: 0.000 \n";
               txtalign("left");
               printdoublesize(strMsg11);
               string strMsg12 = "------------------------------ \n";
               txtalign("left");
               printnormalsize(strMsg12);
               string strMsg13 = "Total VAT: 00.00 \n";
               txtalign("left");
               printnormalsize(strMsg13);
               string strMsg14 = "Total Govt Charges: 00.00 \n";
               txtalign("left");
               printnormalsize(strMsg14);
               string strMsg15 = "Basic Charge: 00.00 \n";
               txtalign("left");
               printnormalsize(strMsg15);
               string strMsg16 = "Transformer Rental / Others:\n00.00 \n";
               txtalign("left");
               printnormalsize(strMsg16);
               string strMsg17 = "Recovery Charge: 00.00 \n";
               txtalign("left");
               printnormalsize(strMsg17);
               string strMsg18 = "CURRENT BILL: \t 0.00 \n";
               txtalign("left");
               printdoublesize(strMsg18);
               string strMsg19 = "Due Date \t : 00/00/0000 \n";
               txtalign("left");
               printdoublesize(strMsg19);
               string strMsg20 = "After due date add 3% mo ";
               txtalign("left");
               printnormalsize(strMsg20);
               string strMsg21 = "and due for disconnection \n 00.00 \n";
               txtalign("left");
               printnormalsize(strMsg21);
               string strMsg22 = "------------------------------ \n";
               txtalign("center");
               printnormalsize(strMsg22);
               string strMsg23 = "Print Test By : Sen Aljun Peter \n";
               txtalign("left");
               printnormalsize(strMsg23);
               byte[] cmd = new byte[5];
               cmd[0] = 0x0A;
               cmd[1] = 0x0A;
               btset.btConn.send(cmd);
           }
           else
           {
               MessageBox.Show("Not Connected");
           }
        }

        void printdoublesize(string s)
        {
            byte[] OutBuffer;
            Encoding targetEncoding;
            byte[] cmd = new byte[3];
            cmd[0] = 0x1b;
            cmd[1] = 0x21;
            cmd[2] |= 0x10;
            cmd[2] |= 0x03;
            btset.btConn.send(cmd);
            targetEncoding = Encoding.Unicode;
            OutBuffer = targetEncoding.GetBytes(s);
           btset.btConn.send(OutBuffer);
        }
        void printnormalsize(string s)
        {
            byte[] OutBuffer;
            Encoding targetEncoding;
            byte[] cmd = new byte[3];
            cmd[0] = 0x1b;
            cmd[1] = 0x21;
            cmd[2] &= 0xDF;
            cmd[2] &= 0xEF;
            btset.btConn.send(cmd);
            targetEncoding = Encoding.Unicode;
            OutBuffer = targetEncoding.GetBytes(s);
            btset.btConn.send(OutBuffer);
        }

    


        private void btnExittt(object sender, RoutedEventArgs e)
        {
           
            var mm = MessageBox.Show("Do you wish to exit the app", "Exit", MessageBoxButton.OKCancel);
            if (mm == MessageBoxResult.OK)
            {
                Application.Current.Terminate();
            }
        }
      
    }
}