
using MetroFramework.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace BarCodeDemo
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            metroTextBox1.Text = "http://doc.sillys.top";
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
          pictureBox1 .Image =   BarCodeHelper.GenerateBarCode(metroTextBox1.Text,50, 50);
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            if (metroCheckBox1.Checked)
                pictureBox2.Image = BarCodeHelper.GenerateQRCodeWithLogo(metroTextBox1.Text, 160, 160, new Bitmap(pictureBox3.Image));
            else
                pictureBox2.Image = BarCodeHelper.GenerateQRCode(metroTextBox1.Text, 160, 160);
        }

        private void metroTile1_Click(object sender, EventArgs e)
        {
            MetroStyleExtender manager = new MetroStyleExtender();
            metroTile1.TileCount++;
            Random rng = new Random();
            var styles = MetroFramework.MetroColorStyle.Black;
            metroStyleManager1.Style = styles;
            //while (true)
            //{
               
            //    if (metroStyleManager1.Style == styles) continue;
            //    metroStyleManager1.Style = styles;
            //    return;
            //}

        }

        private void metroToggle1_CheckedChanged(object sender, EventArgs e)
        {
            if (metroToggle1.Checked)

                metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            else

                metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Light;
        }
    }
}
