using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BLContracts.Entities;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            
        }
        public static List<Instrument> listai = new List<Instrument>();
        public List<TrackBar> suwaki = new List<TrackBar>();
        public List<RichTextBox> wartosci = new List<RichTextBox>();
        private void Form3_Load(object sender, EventArgs e)
        {

           
           // var model = new PlotModel { Title = "LinearAxis" };
         //   model.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
           // plotView1.Model = model;
            int x = 30;
            int y = 30;
            RichTextBox box;
            LinkLabel tag;
            TrackBar bar;
            for (int i = 0; i < listai.Count; i++)
            {
                bar = new TrackBar();
                tag = new LinkLabel();
                box = new RichTextBox();
                tag.Location = new Point(x, y);
                tag.Text = listai[i].Nazwa;
                tag.Size = new Size(30, 15);
                this.Controls.Add(tag);                
                bar.Location = new Point(x + 30, y);
                bar.Size = new Size(60, 30);
                bar.TickFrequency = 1;
                bar.Maximum = 100;
                bar.ValueChanged += przesuwanie;
                this.Controls.Add(bar);
                suwaki.Add(bar);                
                box.Location = new Point(x + 90, y);
                box.Text = "0";
                box.Size = new Size(60, 30);
                box.BackColor = Color.White;
                wartosci.Add(box);
                this.Controls.Add(box);
                y += 60;
            }
            foreach (var item in suwaki)
            {
                item.Value = 100 / suwaki.Count;
            }
        }
         int suma()
        {
            int i = 0;
            foreach (var item in suwaki)
            {
                i += item.Value;
            }
            return i;
        }
        private void przesuwanie(object sender, EventArgs e)
        {
            int suma = 0;
            label2.Text = this.suma().ToString();
            foreach (var item in suwaki)
            {
                int index = suwaki.IndexOf(item);
                wartosci[index].Text = suwaki[index].Value.ToString();
                suma += item.Value;
                item.Refresh();
            }
            TrackBar t = (TrackBar)sender;
            t.Maximum = 100 + t.Value - suma;
            foreach (var item in suwaki)
            {
                item.Refresh();
            }
        }
    }
}
