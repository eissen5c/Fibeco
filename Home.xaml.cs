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
using SQLite;


using BTConnection;

using Windows.ApplicationModel;

namespace Fibeco
{
    public partial class Home : PhoneApplicationPage
    {
        List<string> test = new List<String>();
        public static string DB_PATH = "Fibeco.db";
        // The sqlite connection.  
        private SQLiteConnection dbConn;

        string msg;
        string bill_no = "0000000000";
        string name = "XXXXX , XXXXX";
        string acct_no = "00-0000-0000 X";
        string address = "XXXXXXXX , XXXXXXXX";
        string meter_no = "0000000000";
        int mult = 0;
        double pres_energy = 0.00;
        double pres_demand = 0.00;
        double prev_energy = 0.00;
        double kwh_used = 0.00;
        double energy_demand = 0.00;
        double total_demand = 0.00;

        double total_vat = 0.000;
        double total_govt_charges = 0.00;
        double basic_charge = 0.00;
        double t_r_o = 0.00;
        double recovery_charge = 0.00;
        double current_bill = 0.00;

        double due_pay = 0.00;
        int percent = 3;


        string bill_no2 = "0000000000";
        string name2 = "XXXXX , XXXXX";
        string acct_no2 = "00-0000-0000 X";
        string address2 = "XXXXXXXX , XXXXXXXX";
        string meter_no2 = "0000000000";
        int mult2 = 0;
        double pres_energy2 = 0.00;
        double pres_demand2 = 0.00;
        double prev_energy2 = 0.00;
        double kwh_used2 = 0.00;
        double energy_demand2 = 0.00;
        double total_demand2 = 0.00;

        double total_vat2 = 0.000;
        double total_govt_charges2 = 0.00;
        double basic_charge2 = 0.00;
        double t_r_o2 = 0.00;
        double recovery_charge2 = 0.00;
        double current_bill2 = 0.00;

        double due_pay2 = 0.00;
        int percent2 = 3;

        public static string fname, lname, uaddress, gender, profilepic, isfirsttime;

        public Home()
        {
            InitializeComponent();
          
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (dbConn != null)
                dbConn.Close();     // Close the database connection.  

        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            dbConn = new SQLiteConnection(DB_PATH);
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

            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
            {
                txtroute.Text = msg;
            }
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

        private void select_route(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (btnSearch.IsEnabled == false && btnClear.IsEnabled)
            {

            }
            else
            {
                NavigationService.Navigate(new Uri("/SelectRoute.xaml", UriKind.Relative));
            }
              
        }

        public sealed class member_list
        {
            [PrimaryKey, AutoIncrement]
            public int id { get; set; }
            public string meter_no { get; set; }
            public string area { get; set; }
            public int consumer_no { get; set; }
            public string fname { get; set; }
            public string mi { get; set; }
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
            public string route_id {get; set; }

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
            public string recovery_charge { get; set; }
            public string due_date { get; set; }
            public int mult { get; set; }
            public int consumer_no { get; set; }
            public string acc_no { get; set; }
            public string date_print { get; set; }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Menu.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
           // up.lblacctype.Text = MainPage.
            NavigationService.Navigate(new Uri("/UserProfile.xaml?fname=" + fname + "&lname=" + lname + "&gender=" + gender + "&address=" + uaddress + "&profilepic=" + profilepic + "&isfirsttime=" + isfirsttime, UriKind.Relative));
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

 

        private void btnRePrint(object sender, RoutedEventArgs e)
        {
            rePrint();

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
        void boldtext(string s)
        {
            byte[] cmd = new byte[9];
            cmd[0] = 0x1B;
            cmd[1] = 0x45;
            if (s == "on")
            {
               cmd[2] = 0x01;
            }
            else if (s == "off")
            {
                cmd[2] = 0x00;
            }
            try
            {
                btset.btConn.send(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        public double getCurrentBill(double total_vat = 0.00,double total_govt_charges = 0.00,double basic_charge = 0.00,double t_r_o = 0.00,double recovery_charge = 0.00)
        {
            double currentbill = 0;
            currentbill = currentbill + total_vat;
            currentbill = currentbill + total_govt_charges;
            currentbill = currentbill + basic_charge;
            currentbill = currentbill + t_r_o;
            currentbill = currentbill + recovery_charge;
            return currentbill;
        }
     
        void printFile()
        {
           
            dbConn = new SQLiteConnection(DB_PATH);
            var tp = dbConn.Query<member_list>("select * from member_list where acct_no='" + txtaccno.Text + "'").FirstOrDefault();

            if (tp == null)
            {
                MessageBox.Show("Account Not Found");
                return;
            }
            else
            {


                var lastbillno = dbConn.Query<electric_bill>("SELECT * FROM electric_bill ORDER BY electric_bill.bill_no DESC LIMIT 0, 1").FirstOrDefault();

                //MessageBox.Show(lastbillno.bill_no);

                int newbill = Convert.ToInt32(lastbillno.bill_no) + 1 ;

                String newbill2 = Convert.ToString(newbill);

                String newbill3 = "";
                
                if ( 7 - newbill2.Length == 1) 
                {
                    newbill3 = "0" + newbill2;
                }
                else if (7 - newbill2.Length == 2)
                {
                    newbill3 = "00" + newbill2;
                }
                else if (7 - newbill2.Length == 3)
                {
                    newbill3 = "000" + newbill2;
                }
                else if (7 - newbill2.Length == 4)
                {
                    newbill3 = "0000" + newbill2;
                }
                else if (7 - newbill2.Length == 5)
                {
                    newbill3 = "00000" + newbill2;
                }
                else if (7 - newbill2.Length == 6)
                {
                    newbill3 = "000000" + newbill2;
                }
                else if (7 - newbill2.Length == 7)
                {
                    newbill3 = "0000000" + newbill2;
                }
                else
                {
                    newbill3 = newbill2;
                }


                var billno = dbConn.Query<electric_bill>("SELECT * FROM electric_bill where acc_no='" + txtaccno.Text + "' ORDER BY electric_bill.bill_no DESC LIMIT 0, 1  ").FirstOrDefault();
               

                btnProcesss.IsEnabled = false;
                btnSearch.IsEnabled = false;
                btnClear.IsEnabled = false;
                txtaccno.IsEnabled = false;
                txtmeterserial.IsEnabled = false;
                txtreading.IsEnabled = false;
                txtroute.IsEnabled = false;
                txtdemand.IsEnabled = false;
                btnNewBill.IsEnabled = true;




                bill_no = billno.bill_group + tp.route + newbill3;
                name = tp.lname + ", " + tp.fname;
                acct_no = tp.area + "-" + tp.route + "-" + tp.acct_no + " " + tp.type;
                address = tp.barangay + " " + tp.town;
                meter_no = tp.meter_no;
                mult = 1;

                var tp2 = dbConn.Query<electric_bill>("select * from electric_bill where acc_no='" + tp.acct_no + "' ORDER BY electric_bill.bill_no DESC LIMIT 0, 1 ").FirstOrDefault();

               
                //tp2.energy + 
                kwh_used = Convert.ToDouble (txtreading.Text);
                prev_energy = Convert.ToDouble(tp2.energy);

                pres_energy = prev_energy + kwh_used;
                pres_demand = 0.00;
                
               
                energy_demand = 0.00;
                total_demand = 0.00;

                total_vat = 0.000;
                total_govt_charges = 0.00;
                basic_charge = 0.00;
                t_r_o = 0.00;
                recovery_charge = 0.00;
                current_bill = 0.00;

                current_bill = getCurrentBill(total_vat, total_govt_charges, basic_charge, t_r_o, recovery_charge);

                due_pay = current_bill * 0.03;

               // var insetbillno = dbConn.Query<electric_bill>("insert into electric_bill (bill_no,bill_group,bill_month,energy) VALUES ('" + newbill3 + "','A','"  + DateTime.Now.ToShortDateString() +  "','" + pres_energy + "') ").LastOrDefault();
                var electric_billl = new electric_bill()
                {
                    bill_no = newbill3,
                    bill_group = "A",
                     bill_month = DateTime.Now.Month.ToString(),
                    energy = Convert.ToString(pres_energy),
                    kwh_used = Convert.ToString(kwh_used),
                   demand = "0",
                    total_vat = "0",
                    total_gov_charges= "0",
                     basic_charge = "0",
                    transformer_rental_others = "0",
                    recovery_charge = "0",
                     due_date = "0",
                     mult = 1,
                     consumer_no = 1,
                    acc_no = tp.acct_no,
                     date_print = DateTime.Now.ToString()

                };
                dbConn.Insert(electric_billl);



              //  MessageBox.Show(bill_no);

                bill_no2 = bill_no;
                name2 = name;
                acct_no2 = acct_no;
                address2 = address;
                meter_no2 = meter_no;
                mult2 = mult;
                pres_energy2 = pres_energy;
                pres_demand2 = pres_demand;
                prev_energy2 = prev_energy;
                kwh_used2 = kwh_used;
                energy_demand2 = energy_demand;
                total_demand2 = total_demand;

                total_vat2 = total_vat;
                total_govt_charges2 = total_govt_charges;
                basic_charge2 = basic_charge;
                t_r_o2 = t_r_o;
                recovery_charge2 = recovery_charge;
                current_bill2 = current_bill;

                due_pay2 = current_bill2 * 0.03;
   
               string strMsg1 = "E L E C T R I C  B I L L \n";
               string strMsg2 = "Statement of Account \n";
               string strMsg3 = "Bill Month : " + DateTime.Now.ToString("MM") + "/" + DateTime.Now.Year.ToString() + "\nBill No. : " + bill_no + " \n";
               string strMsg4 = "------------------------------ \n";
               string strMsg5 = "Name: " + name + " \n";
               string strMsg6 = "Acct No.: " + acct_no + " \n";
               string strMsg7 = "Address: " + address + " \n ";
               string strMsg8 = "Meter No: " + meter_no + " \nMult: " + mult + " \n";
               string strMsg9 = "Pres Energy: " + pres_energy + " \nPres Demand: " + pres_demand + " \n";
               string strMsg10 = "Prev Energy: " + prev_energy + " \n";
               string strMsg11 = "kWH Used: " + kwh_used + "\nEnergy Demand: " + energy_demand + " \n";
               string strMsg111 = "Total Demand: " + total_demand + " \n";
               string strMsg12 = "------------------------------ \n";
               string strMsg13 = "Total VAT: " + total_vat + " \n";
               string strMsg14 = "Total Govt Charges: " + total_govt_charges + " \n";
               string strMsg15 = "Basic Charge: " + basic_charge + " \n";
               string strMsg16 = "Transformer Rental / Others:\n" + t_r_o + " \n";
               string strMsg17 = "Recovery Charge: " + recovery_charge + " \n";
               string strMsg18 = "CURRENT BILL: " + current_bill + " \n";
               string strMsg19 = "Due Date : " + DateTime.Today.AddDays(7).ToShortDateString() + " \n";
               string strMsg20 = "After due date add " + percent + "% mo ";
               string strMsg21 = "and due for disconnection \n " + due_pay + "\n";
               string strMsg22 = "------------------------------ \n";

               MessageBox.Show( strMsg1 + strMsg2 + strMsg3 + strMsg4 + strMsg5 + strMsg6 + strMsg7 + strMsg8 + strMsg9 + strMsg10 + strMsg11 + strMsg111 + strMsg12 + strMsg13 + strMsg14 + strMsg15 + strMsg16 + strMsg17 + strMsg18 + strMsg19 + strMsg20 + strMsg21 + strMsg22);
          

               /*
                printlogo();
               string strMsg1 = "E L E C T R I C  B I L L \n";
                boldtext("on");
                txtalign("center");
                printdoublesize(strMsg1);
                boldtext("off");
               string strMsg2 = "Statement of Account \n";
               txtalign("center");
               printdoublesize(strMsg2);
               string strMsg3 = "Bill Month : " + DateTime.Now.ToString("MM") + "/" + DateTime.Now.Year.ToString() + "\nBill No. : " + bill_no + " \n";
                txtalign("left");
               printdoublesize(strMsg3);
               string strMsg4 = "------------------------------ \n";
               txtalign("center");
               printnormalsize(strMsg4);
               string strMsg5 = "Name: " + name + " \n";
               txtalign("left");
               printdoublesize(strMsg5);
               string strMsg6 = "Acct No.: " + acct_no + " \n";
               txtalign("left");
               printdoublesize(strMsg6);
               string strMsg7 = "Address: " + address + " \n ";
               txtalign("left");
               printnormalsize(strMsg7);
               string strMsg8 = "Meter No: " + meter_no + " \nMult: " + mult + " \n";
               txtalign("left");
               printnormalsize(strMsg8);
               string strMsg9 = "Pres Energy: " + pres_energy + " \nPres Demand: " + pres_demand + " \n";
               txtalign("left");
               printnormalsize(strMsg9);
               string strMsg10 = "Prev Energy: " + prev_energy + " \n";
               txtalign("left");
               printnormalsize(strMsg10);
               string strMsg11 = "kWH Used: " + kwh_used + "\nEnergy Demand: " + energy_demand + " \n";
               txtalign("left");
               printdoublesize(strMsg11);
               string strMsg111 = "Total Demand: " + total_demand + " \n";
               txtalign("left");
               printdoublesize(strMsg111);
               string strMsg12 = "------------------------------ \n";
               txtalign("left");
               printnormalsize(strMsg12);
               string strMsg13 = "Total VAT: " + total_vat + " \n";
               txtalign("left");
               printnormalsize(strMsg13);
               string strMsg14 = "Total Govt Charges: " + total_govt_charges + " \n";
               txtalign("left");
               printnormalsize(strMsg14);
               string strMsg15 = "Basic Charge: " + basic_charge + " \n";
               txtalign("left");
               printnormalsize(strMsg15);
               string strMsg16 = "Transformer Rental / Others:\n" + t_r_o + " \n";
               txtalign("left");
               printnormalsize(strMsg16);
               string strMsg17 = "Recovery Charge: " + recovery_charge + " \n";
               txtalign("left");
               printnormalsize(strMsg17);
               string strMsg18 = "CURRENT BILL: " + current_bill + " \n";
               txtalign("left"); 
               printdoublesize(strMsg18);
               string strMsg19 = "Due Date : " + DateTime.Today.AddDays(7).ToShortDateString() + " \n";
               txtalign("left");
               printdoublesize(strMsg19);
               string strMsg20 = "After due date add " + percent + "% mo ";
               txtalign("left");
               printnormalsize(strMsg20);
               string strMsg21 = "and due for disconnection \n " + due_pay + "\n";
               txtalign("left");
               printnormalsize(strMsg21);
               string strMsg22 = "------------------------------ \n";
                txtalign("center");
                printnormalsize(strMsg22);

               byte[] cmd = new byte[5];
               cmd[0] = 0x0A;
               cmd[1] = 0x0A;
                btset.btConn.send(cmd);
               //MessageBox.Show( strMsg1 + strMsg2 + strMsg3 + strMsg4 + strMsg5 + strMsg6 + strMsg7 + strMsg8 + strMsg9 + strMsg10 + strMsg11 + strMsg12 + strMsg13 + strMsg14 + strMsg15 + strMsg16 + strMsg17 + strMsg18 + strMsg19 + strMsg20 + strMsg21 + strMsg22);
          
                */
            }
        }

        void rePrint()
        {
            string strMsg1 = "E L E C T R I C  B I L L \n";
            string strMsg2 = "Statement of Account \n";
            string strMsg3 = "Bill Month : " + DateTime.Now.ToString("MM") + "/" + DateTime.Now.Year.ToString() + "\nBill No. : " + bill_no2 + " \n";
            string strMsg4 = "------------------------------ \n";
            string strMsg5 = "Name: " + name2 + " \n";
            string strMsg6 = "Acct No.: " + acct_no2 + " \n";
            string strMsg7 = "Address: " + address2 + " \n ";
            string strMsg8 = "Meter No: " + meter_no2 + "\nMult: " + mult2 + " \n";
            string strMsg9 = "Pres Energy: " + pres_energy2 + " \nPres Demand: " + pres_demand2 + " \n";
            string strMsg10 = "Prev Energy: " + prev_energy2 + " \n";
            string strMsg11 = "kWH Used: " + kwh_used2 + "\nEnergy Demand: " + energy_demand2 + " \n";
            string strMsg111 = "Total Demand: " + total_demand2 + " \n";
            string strMsg12 = "------------------------------ \n";
            string strMsg13 = "Total VAT: " + total_vat2 + " \n";
            string strMsg14 = "Total Govt Charges: " + total_govt_charges2 + " \n";
            string strMsg15 = "Basic Charge: " + basic_charge2 + " \n";
            string strMsg16 = "Transformer Rental / Others:\n" + t_r_o2 + " \n";
            string strMsg17 = "Recovery Charge: " + recovery_charge2 + " \n";
            string strMsg18 = "CURRENT BILL: " + current_bill2 + " \n";
            string strMsg19 = "Due Date : " + DateTime.Today.AddDays(7).ToShortDateString() + " \n";
            string strMsg20 = "After due date add " + percent2 + "% mo ";
            string strMsg21 = "and due for disconnection \n " + due_pay2 + "\n";
            string strMsg22 = "------------------------------ \n";

            MessageBox.Show(strMsg1 + strMsg2 + strMsg3 + strMsg4 + strMsg5 + strMsg6 + strMsg7 + strMsg8 + strMsg9 + strMsg10 + strMsg11 + strMsg111 + strMsg12 + strMsg13 + strMsg14 + strMsg15 + strMsg16 + strMsg17 + strMsg18 + strMsg19 + strMsg20 + strMsg21 + strMsg22);
            return;

            /*
            printlogo();
            string strMsg1 = "E L E C T R I C  B I L L \n";
            boldtext("on");
            txtalign("center");
            printdoublesize(strMsg1);
            boldtext("off");
            string strMsg2 = "Statement of Account \n";
            txtalign("center");
            printdoublesize(strMsg2);
            string strMsg3 = "Bill Month : " + DateTime.Now.ToString("MM") + "/" + DateTime.Now.Year.ToString() + "\nBill No. : " + bill_no2 + " \n";
            txtalign("left");
            printdoublesize(strMsg3);
            string strMsg4 = "------------------------------ \n";
            txtalign("center");
            printnormalsize(strMsg4);
            string strMsg5 = "Name: " + name2 + " \n";
            txtalign("left");
            printdoublesize(strMsg5);
            string strMsg6 = "Acct No.: " + acct_no2 + " \n";
            txtalign("left");
            printdoublesize(strMsg6);
            string strMsg7 = "Address: " + address2 + " \n ";
            txtalign("left");
            printnormalsize(strMsg7);
            string strMsg8 = "Meter No: " + meter_no2 + " \nMult: " + mult2 + " \n";
            txtalign("left");
            printnormalsize(strMsg8);
            string strMsg9 = "Pres Energy: " + pres_energy2 + " \nPres Demand: " + pres_demand2 + " \n";
            txtalign("left");
            printnormalsize(strMsg9);
            string strMsg10 = "Prev Energy: " + prev_energy2 + " \n";
            txtalign("left");
            printnormalsize(strMsg10);
            string strMsg11 = "kWH Used: " + kwh_used2 + "\nEnergy Demand: " + energy_demand2 + " \n";
            txtalign("left");
            printdoublesize(strMsg11);
            string strMsg111 = "Total Demand: " + total_demand2 + " \n";
            txtalign("left");
            printdoublesize(strMsg111);
            string strMsg12 = "------------------------------ \n";
            txtalign("left");
            printnormalsize(strMsg12);
            string strMsg13 = "Total VAT: " + total_vat2 + " \n";
            txtalign("left");
            printnormalsize(strMsg13);
            string strMsg14 = "Total Govt Charges: " + total_govt_charges2 + " \n";
            txtalign("left");
            printnormalsize(strMsg14);
            string strMsg15 = "Basic Charge: " + basic_charge2 + " \n";
            txtalign("left");
            printnormalsize(strMsg15);
            string strMsg16 = "Transformer Rental / Others:\n" + t_r_o2 + " \n";
            txtalign("left");
            printnormalsize(strMsg16);
            string strMsg17 = "Recovery Charge: " + recovery_charge2 + " \n";
            txtalign("left");
            printnormalsize(strMsg17);
            string strMsg18 = "CURRENT BILL: " + current_bill2 + " \n";
            txtalign("left");
            printdoublesize(strMsg18);
            string strMsg19 = "Due Date \t : " + DateTime.Today.AddDays(7).ToShortDateString() + " \n";
            txtalign("left");
            printdoublesize(strMsg19);
            string strMsg20 = "After due date add " + percent2 + "% mo ";
            txtalign("left");
            printnormalsize(strMsg20);
            string strMsg21 = "and due for disconnection \n " + due_pay2 + "\n";
            txtalign("left");
            printnormalsize(strMsg21);
            string strMsg22 = "------------------------------ \n";
            txtalign("center");
            printnormalsize(strMsg22);

            byte[] cmd = new byte[5];
            cmd[0] = 0x0A;
            cmd[1] = 0x0A;

            btset.btConn.send(cmd);
             */
        }
        private async Task CopyDatabase()
        {
        //    bool isDatabaseExisting = false;

         //   try
         //   {
          //      StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(DB_PATH);
          //      isDatabaseExisting = true;
         //   }
         //   catch
          //  {
         //       isDatabaseExisting = false;
         //   }

        //    if (!isDatabaseExisting)
        //    {
          //      StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync(DB_PATH);
         //       await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
         //   }
            
        }
        
        private async void btnProcess(object sender, RoutedEventArgs e)
        {
            if (txtreading.Text == "")
            {
                MessageBox.Show("Reading is Required");
            }
            else if ( txtroute.Text == "" || txtmeterserial.Text == "" || txtaccno.Text == "" || txtacctype.Text == "" || txtname.Text == "" || txtaddress.Text == "" || txtdemand.Text == "")
            {
                MessageBox.Show("No Data");
            }
            else
            {
                printFile();
                btnRePrintt.IsEnabled = true;

                await CopyDatabase();

                return;
                try
                {
                    if (btset.btConn.isConnected)
                    {
                        printFile();
                        btnRePrintt.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Not Connected");
                        btnRePrintt.IsEnabled = false;
                    }
                }
                catch (Exception ex)
                {
                    var final = ex;
                    MessageBox.Show(ex.Message.ToString());
                    btnRePrintt.IsEnabled = false;
                }
            }
          
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            txtaccno.Text = "";
            txtroute.Text = "";
            txtacctype.Text = "";
            txtname.Text = "";
            txtaddress.Text = "";
            txtmeterserial.Text = "";
            txtpresent.Text = "";
            txtreading.Text = "";
            txtdemand.Text = "";

            txtroute.IsEnabled = true;
            txtaccno.IsEnabled = true;
            txtmeterserial.IsEnabled = true;
            btnSearch.IsEnabled = true;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // or meter_no='" + txtmeterserial.Text + "'
            dbConn = new SQLiteConnection(DB_PATH);
            var tp = dbConn.Query<member_list>("select * from member_list where acct_no='" + txtaccno.Text + "' or meter_no='" + txtmeterserial.Text + "' and route='" + txtroute.Text + "'").FirstOrDefault();

            if (tp == null)
            {
                MessageBox.Show("Account Not Found");
            }
            else
            {

                var tp2 = dbConn.Query<electric_bill>("select * from electric_bill where acc_no='" + tp.acct_no + "'").FirstOrDefault();

                txtaccno.Text = tp.acct_no;
                txtroute.Text = tp.route;
                txtacctype.Text = tp.type;
                txtname.Text = tp.lname + ", " + tp.fname;
                txtaddress.Text = tp.town.ToUpper() + " BUKIDNON";
                txtmeterserial.Text = tp.meter_no;
                txtpresent.Text = tp2.energy;
                // txtreading.Text = "0";
                txtdemand.Text = "0";

                txtroute.IsEnabled = false;
                txtaccno.IsEnabled = false;
                txtmeterserial.IsEnabled = false;
                btnSearch.IsEnabled = false;
            }
            if (dbConn != null)
            {
                dbConn.Close();
            }
        }

        private void btnNewBill_Click(object sender, RoutedEventArgs e)
        {
            var mm = MessageBox.Show("Are You Sure?", "New Session", MessageBoxButton.OKCancel);
            if (mm == MessageBoxResult.OK)
            {
                txtaccno.Text = "";
                txtroute.Text = "";
                txtacctype.Text = "";
                txtname.Text = "";
                txtaddress.Text = "";
                txtmeterserial.Text = "";
                txtpresent.Text = "";
                txtreading.Text = "";
                txtdemand.Text = "";
                btnRePrintt.IsEnabled = false;
                btnProcesss.IsEnabled = true;
                btnSearch.IsEnabled = true;
                btnClear.IsEnabled = true;
                txtaccno.IsEnabled = true;
                txtmeterserial.IsEnabled = true;
                txtreading.IsEnabled = true;
                txtroute.IsEnabled = true;
                txtdemand.IsEnabled = true;
                btnNewBill.IsEnabled = false;
            }
        }

        private void searchbymeter(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ShowData.xaml", UriKind.Relative));
        }
        

      

   
        

       
    }
}