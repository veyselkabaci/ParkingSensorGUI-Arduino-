using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySerialPort
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Location = new Point(300, 140);
            pictureBox2.Location = new Point(0, 74);
            pictureBox4.Location = new Point(675, 74);
            pictureBox2.Controls.Add(pictureBox3);
            pictureBox3.Location = new Point(0, 0);
            pictureBox3.BackColor = Color.Transparent;
            pictureBox4.Controls.Add(pictureBox5);
            pictureBox5.Location = new Point(0, 0);
            pictureBox5.BackColor = Color.Transparent;
            serialPort1.Open();
            serialPort1.DataReceived += serialPort1_DataReceived;
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string POT = serialPort1.ReadLine();
            this.BeginInvoke(new LineReceivedEvent(LineReceived), POT);
        }

        private delegate void LineReceivedEvent(string POT);

       private void LineReceived(string POT)
       {

           int voltaje,voltaje2;

           string[] numbers = Regex.Split(POT, @"\D+");
           foreach (string value in numbers)
           {
               if (!string.IsNullOrEmpty(value))
               {
                   int i = int.Parse(value);
               }
           }
           
           

           if (Int32.TryParse(numbers[0], out voltaje))
           {
               voltaje = int.Parse(numbers[0]);
               label1.Text = (voltaje ).ToString();
           }
           if (Int32.TryParse(numbers[1], out voltaje2))
           {
               voltaje2 = int.Parse(numbers[1]);
               label2.Text = (voltaje2 ).ToString();
           }

           
           Image Image = Image.FromFile("D:\\image\\car.png");
           Image Image2 = Image.FromFile("D:\\image\\back_gray.png");
           Image Image3 = Image.FromFile("D:\\image\\back1.png");
           Image Image4 = Image.FromFile("D:\\image\\front1.png");
           Image Image5 = Image.FromFile("D:\\image\\front_gray.png");


           Bitmap Original2 = new Bitmap(Image2);
           Bitmap TrasparentImage2 = new Bitmap(Image2.Width, Image2.Height);

           Color c2 = Color.White;
           Color v2 = Color.White;

           for (int i = 0; i < Image2.Width; i++)
           {
               for (int y = 0; y < Image2.Height; y++)
               {
                   c2 = Original2.GetPixel(i, y);
                   v2 = Color.FromArgb(255, c2.R, c2.G, c2.B);
                   if (i > 300-(voltaje*3))
                   {
                       if (c2.R != 0 && c2.G != 0 && c2.B != 0)
                       {
                           TrasparentImage2.SetPixel(i, y, v2);
                       }
                   }

               }
           }

           Bitmap Original5 = new Bitmap(Image5);
           Bitmap TrasparentImage5 = new Bitmap(Image5.Width, Image5.Height);

           Color c5 = Color.White;
           Color v5 = Color.White;

           for (int i2 = 0; i2 < Image5.Width; i2++)
           {
               for (int y2 = 0; y2 < Image5.Height; y2++)
               {
                   c5 = Original5.GetPixel(i2, y2);
                   v5 = Color.FromArgb(255, c5.R, c5.G, c5.B);
                   if (i2 < voltaje2*3)
                   {
                       if (c5.R != 0 && c5.G != 0 && c5.B != 0)
                       {
                           TrasparentImage5.SetPixel(i2, y2, v5);
                       }
                   }
               }
           }

           pictureBox1.Image = Image;
           pictureBox2.Image = Image3;
           pictureBox3.Image = TrasparentImage2;
           pictureBox4.Image = Image4;
           pictureBox5.Image = TrasparentImage5;

       }



    }
}
