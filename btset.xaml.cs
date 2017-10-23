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



//using Bluetooth;

//Bluetooth;

namespace Fibeco
{
    public partial class btset : PhoneApplicationPage
    {

       public static BTConnection.ConnectionManager btConn;
       public static PeerInformation peerInformation;
       public string sFileName = "";

        public btset()
        {
            InitializeComponent();
            btConn = new ConnectionManager();
            btConn.ConnectDone += btConn_ConnectDone;
            btConn.MessageReceived += btConn_MessageReceived;
        }

        private async Task connectionTest(string MacAddress)
        {
            //Modify the connection with your device mac Address
            var macAddress = "001208092929";
            await connectionTest(macAddress);
        }

        void btConn_ConnectDone(Windows.Networking.HostName deviceHostName)
        {
            //await btConn.send("Hello \n");
            //addLog("Connected to " + deviceHostName);
            //await btConn.SendCommand("Hello\n");
            //System.Threading.Thread.Sleep(2000);
            btnDisconnect.IsEnabled = true;
            btnConnect.IsEnabled = false;
            btnTest.IsEnabled = true;
        
        }


        private void ClickSearch(object sender, RoutedEventArgs e)
        {
            try
            {
               
                NavigationService.Navigate(new Uri("/btdevices.xaml", UriKind.Relative));
                
            }
            catch (Exception ex)
            {
             //  if ((uint)ex.HResult == 0x8007048F)
            //    {   
                   
                    MessageBox.Show("Bluetooth is turned off /n Please Turn it on.");
                    NavigationService.GoBack();
              //  }
            }
        }

        void showBTSettings()
        {
            var task = new ConnectionSettingsTask();
            task.ConnectionSettingsType = ConnectionSettingsType.Bluetooth;
            task.Show();

        }

        private void ClickConnect(object sender, RoutedEventArgs e)
        {
            try
            {
              
                if (btConn.isConnected)
                {
                    MessageBox.Show("Select Printer First");
                    NavigationService.Navigate(new Uri("/btdevices.xaml", UriKind.Relative));
                
                }
                else
                {
                    btConn.Connect(peerInformation.HostName);
                    NavigationService.GoBack();
                }
                

            }
            catch (Exception ex)
            {
                var final = ex;
                MessageBox.Show("Select Printer First");
                NavigationService.Navigate(new Uri("/btdevices.xaml", UriKind.Relative));
            }
        }
        private void btnTest_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (btConn.isConnected)
                {
                    byte[] cmd = new byte[3];
                    cmd[0] = 0x1B;
                    btConn.send(cmd); 
                   // printFile();
                   
                    printlogo();
                   printFile();
                
                       //addLog("Send done");
                }
                else
                {
                    addLog("Not Connected");
                }
            }
            catch (Exception ex) 
            { 
                addLog("Exception: " + ex.Message); 
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
             btConn.send(cmd);
            targetEncoding = Encoding.Unicode;
            OutBuffer = targetEncoding.GetBytes(s);
             btConn.send(OutBuffer);
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
          btConn.send(cmd);
            targetEncoding = Encoding.Unicode;
            OutBuffer = targetEncoding.GetBytes(s);
            btConn.send(OutBuffer);
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
             else if(i == "center")
             {
                 cmd[2] = 0x01;
             }
             else if(i == "right")
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
                 btConn.send(cmd);
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
                 btConn.send(cmd);
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
        
         }

         void printFile()
        {
            printlogo();
            string strMsg1 =  "\t E L E C T R I C  B I L L \n" ;
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
            string strMsg5 = "Name \t \t: PANGANIBAN , SEN \n";
            txtalign("left");
            printdoublesize(strMsg5);
            string strMsg6 = "Acct No. \t \t : 00-0000-0000 0 \n";
            txtalign("left");
            printdoublesize(strMsg6);
            string strMsg7 = "Address \t : MUSUAN BUKIDNON \n";
            txtalign("left");
            printnormalsize(strMsg7);
            string strMsg8 = "Meter No \t : 0000000000 \nMult : 0 \n";
            txtalign("left");
            printnormalsize(strMsg8);
            string strMsg9 = "Pres Energy: 0000.0 \nPres Demand: 0.000 \n";
            txtalign("left");
            printnormalsize(strMsg9);
            string strMsg10 = "Prev Energy: 0000.0 \nPrev Demand: 0.000 \n";
            txtalign("left");
            printnormalsize(strMsg10);
            string strMsg11 = "kWH Used: 00.0 \nDemand: 0.000 \n";
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
          /*  string strMsg23 = "Arrears : \t 00.00 \n";
            printdoublesize(strMsg23);
            string strMsg24 = "Please disregard arrears if payment \n";
            printnormalsize(strMsg24);
            string strMsg25 = "MARAMAG SEN @ 00/00/0000/ 00:00 XX \n";
            printnormalsize(strMsg25);
            string strMsg26 = "------------------------------ \n";
            printnormalsize(strMsg26);
            string strMsg27 = "Message: \n";
            printnormalsize(strMsg27);
            string strMsg28 = "Pagkaputo sa serbisyo malikayan kung atong \n";
            printnormalsize(strMsg28);
            string strMsg29 = "obligasyon matag bulan bayaran. \n \n";
            printnormalsize(strMsg29);
            string strMsg30 = "\t THIS IS NOT AN OFFICIAL RECEIPT \n \n";
            printnormalsize(strMsg30);
            string strMsg31 = "------------------------------ \n";
            printnormalsize(strMsg31);
     */
            byte[] cmd = new byte[5];
            cmd[0] = 0x0A;
            cmd[1] = 0x0A;
             btConn.send(cmd);      
        }
    
    
        public async Task<byte[]> ReadFileContentsAsync(StorageFile _fileName)
        {
            try
            {
         
                var file = await _fileName.OpenReadAsync();
                Stream stream = file.AsStreamForRead();
                byte[] buf = new byte[stream.Length];
     
                int iRead = stream.Read(buf, 0, buf.Length);
              
                //addLog("read file = " + iRead.ToString());
                return buf;
            }
            catch (Exception)
            {
                return new byte[0];
            }
        }

        void addLog(string s)
        {
           // MessageBox.Show(s);
            //string t = txtLog.Text;
            //txtLog.Text = s + "\r\n" + t;
           // System.Diagnostics.Debug.WriteLine(s);
        }
        void btConn_MessageReceived(string message)
        {
            addLog(message);
        }

        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            if (btConn.isConnected)
            {
                btConn.Disconnect();
                //btConn = null;
                btnDisconnect.IsEnabled = false;
                btnConnect.IsEnabled = true;
                btnTest.IsEnabled = false;
            }
            else
            {
                MessageBox.Show("Connect First");
            }
        }

        private void txtbluetooth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtbluetooth.Text != "")
            {
                btnConnect.IsEnabled = true;
            }
            else
            {
                btnConnect.IsEnabled = false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            byte[] cmd = new byte[2];
            cmd[0] = 0x0A;
           
            try
            {
                 btConn.send(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

      



    }
}