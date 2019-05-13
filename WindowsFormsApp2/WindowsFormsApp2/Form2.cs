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

    public partial class Form2 : Form
    {
        public Form2()
        {
        
            InitializeComponent();
            Wypelnij();
        }

        List<Instrument> inst = new List<Instrument>();
        public void Wypelnij()
        {
            List<Notowanie> n = new List<Notowanie>();
            var conn = new SqlConnection();
            conn.ConnectionString = @"Data Source = DESKTOP-29587F4\SQLEXPRESS; " + "Initial Catalog=baza;" + "Integrated Security=SSPI;";
            conn.Open();
            SqlCommand command = new SqlCommand("Select distinct nazwa  From dbo.dane_gieldowe", conn);

            SqlDataReader reader = command.ExecuteReader();
            listBox1.DisplayMember = "nazwa";
            while (reader.Read())
            {

                inst.Add(new Instrument { nazwa = reader.GetString(0), notowania = n });
                listBox1.Items.Add(new Instrument { nazwa = reader.GetString(0), notowania = n });
            }
            listBox1.DisplayMember = "nazwa";
            listBox1.ValueMember = "wolumen";
        }

            private void Button1_Click(object sender, EventArgs e)  //dodawanie do listy wybranych
            {
            listBox2.DisplayMember = "nazwa";
            listBox2.ValueMember = "wolumen";
            List<Instrument> sl = new List<Instrument>();
            foreach (var item in listBox1.SelectedItems)
            {
                sl.Add((Instrument)item);
            }
           foreach (var item in sl)
           {
                if(!listBox2.Items.Contains(item))
                {
                    listBox2.Items.Add(item);
                }
           }

        }
        private void Button2_Click(object sender, EventArgs e)  //usuwanie po zaznaczonych
        {
            listBox2.Items.Remove(listBox2.Items[listBox2.SelectedIndex]);
        }
         List<Instrument> przesylane = new List<Instrument>();
        public void Button3_Click(object sender, EventArgs e) //przekazywanie listy instrumentów do Form1
        {
           
            listai.Clear();
            foreach (Instrument item in listBox2.Items)
            {
                listai.Add(item);
            }
            this.Hide();
            Form1 f = new Form1();
            f.listBox1.Select();
            f.listBox1.Items.Clear();
            f.listBox1.Visible = true;
            f.listBox1.DisplayMember = "nazwa";
            f.listBox1.ValueMember = "wolumen";
            f.listBox1.DataSource = listai;
            f.ShowDialog();
            this.Close();
        }
        public static List<Instrument> listai=new List<Instrument>();   //lista instrumentów przekazywanych do listboksów
        private void TextBox1_TextChanged(object sender, EventArgs e) //filtrowanie po nazwie
        {
            if (textBox1.Text != null)
            {
                listBox1.Items.Clear();
                foreach (Instrument item in inst)
                {
                    if (item.nazwa.StartsWith(textBox1.Text.ToUpper()))
                    {
                        listBox1.Items.Add((Instrument)item);
                    }
                }
            }
            listBox1.Refresh();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
