using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Data.SqlTypes;
namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Button1_Click(object sender, EventArgs e)
        {
        
            WebClient Client = new WebClient();
            Client.DownloadFile("https://info.bossa.pl/pub/ciagle/mstock/sesjacgl/sesjacgl.prn", @"C:\Users\mariu\Downloads\akcje.txt");
            var conn = new SqlConnection();
            conn.ConnectionString = @"Data Source = DESKTOP-29587F4\SQLEXPRESS; " + "Initial Catalog=baza;" + "Integrated Security=SSPI;";
            conn.Open();
           // string[] pliki=Directory.GetFiles(@"C:\Users\mariu\Downloads\2009");       
            SqlCommand command = new SqlCommand("insert into dbo.dane_gieldowe (nazwa,data,kurs,otwarcie,max,min,wolumen) values (@nazwa,@data,@kurs,@otwarcie,@max,@min,@wolumen);", conn);
            command.Parameters.Add("@nazwa", System.Data.SqlDbType.VarChar);
            command.Parameters.Add("@data", System.Data.SqlDbType.Float);
            command.Parameters.Add("@kurs", System.Data.SqlDbType.Float);
            command.Parameters.Add("@otwarcie", System.Data.SqlDbType.Float);
            command.Parameters.Add("@max", System.Data.SqlDbType.Float);
            command.Parameters.Add("@min", System.Data.SqlDbType.Float);
            command.Parameters.Add("@wolumen", System.Data.SqlDbType.Float);
           // foreach (var plik in pliki)
          //  {
                string[] allLines = File.ReadAllLines(@"C:\Users\mariu\Downloads\akcje.txt");
                for (int i = 0; i < allLines.Length; i++)
                {
                    string[] items = allLines[i].Split(',');
                    string[] items2 = new string[7];
                    for (int j = 0; j < items.Length; j++)
                    {
                        items2[j] = items[j].Replace('.', ',');
                    }
                    foreach (var item in items)
                    command.Parameters["@nazwa"].Value = items[0];
                    command.Parameters["@data"].Value = Convert.ToDouble(items[1]);
                    command.Parameters["@kurs"].Value = double.Parse(items2[2]);
                    command.Parameters["@otwarcie"].Value = Convert.ToDouble(items2[3]);
                    command.Parameters["@max"].Value = Convert.ToDouble(items2[4]);
                    command.Parameters["@min"].Value = Convert.ToDouble(items2[5]);
                    command.Parameters["@wolumen"].Value = Convert.ToDouble(items2[6]);
                    command.ExecuteNonQuery();
                }
          //  }
            conn.Dispose();
            conn.Close();
            MessageBox.Show("Zaktualizowano Bazę");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void Button2_Click(object sender, EventArgs e)
        {
           
            this.Hide();
            var form2 = new Form2();
            form2.ShowDialog();        
            this.Close();


        }
        List<Instrument> lista = new List<Instrument>();
        private void Button3_Click(object sender, EventArgs e)
        {
            List<Notowanie> n = new List<Notowanie>();
            Form3 form3 = new Form3();
            foreach (var item in Form2.listai)
            {
                Form3.listai.Add(new Instrument { nazwa = item.nazwa,notowania=n });                
            }         
            if (Form3.listai.Count == 0)
            {
                MessageBox.Show("Brak wybranych instrumentów");
            }
            else form3.ShowDialog();
        }

     
    }
}



            