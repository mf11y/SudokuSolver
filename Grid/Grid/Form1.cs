using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grid
{
    public partial class Form1 : Form
    {
        int mouseClickX;
        int mouseClickY;

        public Form1()
        {
            InitializeComponent();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            //this.ResizeRedraw = true;
            Graphics gr = e.Graphics;
            Pen myPen = new Pen(Brushes.Black, 1);
            Font myFont = new Font("Arial", 10);
            int NumLines = 10;

            int x = 0;
            int y = 0;
            int spacing = 40;

            int linelength = spacing * 9;

            for (int i = 0; i < NumLines; i++)
            {
                if (i % 3 == 0 && i != 0)
                    myPen.Width = 3;
                else
                    myPen.Width = 1;

                gr.DrawLine(myPen, x, y, x, linelength);
                x += spacing;
            }

            x = 0;

            for (int i = 0; i < NumLines; i++)
            {

                if (i % 3 == 0 && i != 0)
                    myPen.Width = 3;
                else
                    myPen.Width = 1;

                gr.DrawLine(myPen, x, y, linelength, y);
                y += spacing;
            }

            
        }

        private void P1_Click(object sender, EventArgs e)
        {
            mouseClickX = panel1.PointToClient(Cursor.Position).X;
            mouseClickY = panel1.PointToClient(Cursor.Position).Y;

        }


        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (mouseClickX != 0)
            {
                Graphics gr = panel1.CreateGraphics();
                Pen myPen = new Pen(Brushes.Black, 1);
                Font myFont = new Font("Arial", 10);

                int drawX = (mouseClickX / 40) * 40;
                int drawY = (mouseClickY / 40) * 40;

                char number = (char)e.KeyValue;

                if(number >= '0' && number <= '9' )
                    gr.DrawString(number.ToString(), myFont, Brushes.Black, drawX + 14, drawY + 12);
            }
            mouseClickX = 0;
            mouseClickY = 0;
        }
    }
}
