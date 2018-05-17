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
        int mouseCoordsX;
        int mouseCoordsY;
        int historyPrevClickX = -1;
        int historyPrevClickY = -1; 

        bool isInBoard = false;
        bool activelyReadin = false;

        public Form1()
        {
            InitializeComponent();
        }


        //Paint Grid Lines
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
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

        //When mouse clicked inside panel, get location
        private void P1_Click(object sender, EventArgs e)
        {
            isInBoard = true;
            bool inSameBox = false;

            Graphics gr = panel1.CreateGraphics();
            Font myFont = new Font("Arial", 10);

            mouseCoordsX = panel1.PointToClient(Cursor.Position).X;
            mouseCoordsY = panel1.PointToClient(Cursor.Position).Y;

            int drawX = (mouseCoordsX / 40) * 40;
            int drawY = (mouseCoordsY / 40) * 40;

            Rectangle rect = new Rectangle(drawX + 14, drawY + 12, 20, 20);
            gr.FillRectangle(Brushes.White, rect);


            gr.DrawString("?", myFont, Brushes.Black, drawX + 14, drawY + 12);

            if (historyPrevClickX == drawX && historyPrevClickY == drawY)
                inSameBox = true;
            /*
            if(inSameBox)
            {
                gr.FillRectangle(Brushes.White, rect);
            }*/

            if (activelyReadin && !inSameBox)
            {
                Rectangle rect2 = new Rectangle(historyPrevClickX + 14, historyPrevClickY + 12, 20, 20);
                gr.FillRectangle(Brushes.White, rect2);
            }

            activelyReadin = true;


            historyPrevClickX = drawX;
            historyPrevClickY = drawY;
        }

        //Check for Keypresses
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (isInBoard)
            {
                Graphics gr = panel1.CreateGraphics();

                Font myFont = new Font("Arial", 10);

                int drawX = (mouseCoordsX / 40) * 40;
                int drawY = (mouseCoordsY / 40) * 40;

                char number = (char)e.KeyValue;

                Rectangle rect = new Rectangle(drawX + 14, drawY + 12, 20, 20);
                gr.FillRectangle(Brushes.White, rect);

                if (number >= '0' && number <= '9')
                {
                    gr.DrawString(number.ToString(), myFont, Brushes.Black, drawX + 14, drawY + 12);
                }
                activelyReadin = false;
            }

            isInBoard = false;
        }
    }
}
