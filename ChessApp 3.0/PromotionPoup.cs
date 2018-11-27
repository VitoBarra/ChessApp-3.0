using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessApp_3._0
{
    public partial class PromotionPoup : Form
    {

        public Form1 form1;

       public void GetFormParent(Form1 _form1)
        {
            form1 = _form1;
        }
  


        public PromotionPoup()
        {
            InitializeComponent();
        }

        private void PromotionPoup_Load(object sender, EventArgs e)
        {

            if (Global.Player)
            {
                pictureBox1.Image = Global.SvgBitMap[8];
                pictureBox2.Image = Global.SvgBitMap[10];
                pictureBox3.Image = Global.SvgBitMap[0];
                pictureBox4.Image = Global.SvgBitMap[2];
            }
            else
            {
                pictureBox1.Image = Global.SvgBitMap[9];
                pictureBox2.Image = Global.SvgBitMap[11];
                pictureBox3.Image = Global.SvgBitMap[1];
                pictureBox4.Image = Global.SvgBitMap[3];
            }

            int offset = 15;
            this.Width = Global.width_Height * 4 + offset * 2;
            this.Height = Global.width_Height + offset * 2;
            pictureBox1.Width = Global.width_Height;
            pictureBox1.Height = Global.width_Height;
            pictureBox1.Location = new Point(offset, offset);

            pictureBox2.Width = Global.width_Height;
            pictureBox2.Height = Global.width_Height;
            pictureBox2.Location = new Point(offset + Global.width_Height, offset);

            pictureBox3.Width = Global.width_Height;
            pictureBox3.Height = Global.width_Height;
            pictureBox3.Location = new Point(offset + Global.width_Height * 2, offset);

            pictureBox4.Width = Global.width_Height;
            pictureBox4.Height = Global.width_Height;
            pictureBox4.Location = new Point(offset + Global.width_Height * 3, offset);
        }

        private void splitter3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
