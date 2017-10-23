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
using Fibeco.Resources;
//using Windows.Networking.Sockets;

using Renci.SshNet;

//using System.Data.

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking;
namespace Fibeco
{

    public partial class ShowData : PhoneApplicationPage
    {

        List<string> test = new List<String>();
        public static string DB_PATH = "Fibeco.db";
        // The sqlite connection.  
        private SQLiteConnection dbConn;  
        public ShowData()
        {
            InitializeComponent();
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

        public String ReadData()
        {
            //Set the path of the file
            String pathoffile = "MCtableModified.csv";

            //Content of file
            String Content = "";

            using (System.IO.StreamReader reader = new System.IO.StreamReader(pathoffile))
            {
                Content=reader.ReadToEnd();
                return Content;
            }

           // return Content;
       
        }

        public void writetotextfile(String Content)
        {
            //Set the path of the file
            String pathoffile = "MCtableModified.csv";

            //Content of file
            //String Content = "Change the content to something else.";
            String OldContent = "";

            using (System.IO.StreamReader reader = new System.IO.StreamReader(pathoffile))
            {
                OldContent = reader.ReadToEnd();
            }

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(pathoffile))
            {
                writer.WriteLine(OldContent + Content);
                
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            dbConn = new SQLiteConnection(DB_PATH);
            // Retrieve the task list from the database.  
            List<electric_bill> retrievedTasks = dbConn.Table<electric_bill>().ToList<electric_bill>();
            // Clear the list box that will show all the tasks.  
            //lstRoute.Items.Clear();
            foreach (var t in retrievedTasks)
            {
                lstRoute.Items.Add(t.bill_group.ToString() + "0332" + t.bill_no.ToString());
            }

            // lstRoute.ItemsSource = retrievedTasks;
           // MessageBox.Show(ReadData());
            //writetotextfile("TESTZXC");
            //MessageBox.Show(ReadData());
            try
            {
                 testupload();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           


        }


        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            if (dbConn != null)
                dbConn.Close();     // Close the database connection.  

        }

        public  async void testupload()
        {
            try
            {
                FtpClient client = new FtpClient();
                //await client.ConnectAsync(new HostName("musuan.site"),"21", "panganiban@musuan.site","x35bcz");
                Uri uri = new Uri("ftp://sen@192.168.1.103/");//ip problem
             
               await client.ConnectAsync(new HostName(uri.Host), "21", "sen", "sen");

                //Set the path of the file
                String pathoffile = "MCtableModified.csv";

                //Content of file
                //String Content = "Change the content to something else.";
                String OldContent = "";
                String NewContent = "";
                using (System.IO.StreamReader reader = new System.IO.StreamReader(pathoffile))
                {
                    OldContent = reader.ReadToEnd();
                }

                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(pathoffile))
                {
                    writer.WriteLine(OldContent + Content);
                }
                NewContent = OldContent + Content;

                byte[] data = Encoding.UTF8.GetBytes(NewContent);
                await client.UploadAsync(pathoffile,data);
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }

        private async void DoDownloadOrUpload(bool isDownload)
        {
            Uri uri = new Uri("sftp:////192.168.1.103");

            MessageBox.Show("Connecting.");

            FtpClient client = new FtpClient();
            await client.ConnectAsync(new HostName(uri.Host), "22","sen", "sen");

            if (isDownload)
            {
              MessageBox.Show( "Downloading.");

                byte[] data = await client.DownloadAsync(uri.AbsolutePath);

               MessageBox.Show( Encoding.UTF8.GetString(data, 0, data.Length));
            }
            else
            {
                MessageBox.Show( "Uploading.");

                byte[] data = Encoding.UTF8.GetBytes("test");

                await client.UploadAsync(uri.AbsolutePath, data);
            }

          MessageBox.Show( "Done.");
        }
       
        
        public async Task<string> SFTPFileUpload(string FileContent, string fileUsername)
        {
            try
            {
                string FolderPath = "FTPFiles/WindowsPhone";
                string m_Host = "sftp:////192.168.1.103";
                string FtpUserName = "sen";
                string FtpPwd = "sen";
                int SFtpPort = 22;//Default IP address  
                fileUsername = fileUsername + ".txt";
               SftpClient sftp =  new SftpClient(m_Host, SFtpPort, FtpUserName, FtpPwd);
               // SftpClient sftp = new SftpClient(m_Host ,"" ,"","");
                sftp.ConnectionInfo.Timeout = TimeSpan.FromSeconds(6000);

                sftp.Connect();
               sftp.ChangeDirectory(FolderPath);
                byte[] data = Encoding.Unicode.GetBytes(FileContent);
                sftp.BufferSize = 4 * 1024;

                Stream fileStream = new MemoryStream(data);

                sftp.UploadFile(fileStream, fileUsername, null);

                return "Success";
            }
            catch (Exception ex)
            {
                ShellToast toast = new ShellToast();
                toast.Title = ex.Message.ToString();
                toast.Content = DateTime.Now.ToString();
                toast.Show();
                return ex.Message;// "Server is not connected.";  
            }
        }  
        
    }
}