using System;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Game : Form
    {
        BoardManagement mgn;


        public Game()
        {
            InitializeComponent();
            mgn = new BoardManagement(panel1);
            this.KeyPreview = true;
        }

        private void solve_button(object sender, EventArgs e)
        {
            /*board.solve2();
            //board.solve(0);
            MessageBox.Show(board.printBoard());*/
        }

        //Paint Grid Lines
        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            mgn.DrawBoard();
        }

        //When mouse clicked inside panel, get location
        private void P1_Click(object sender, EventArgs e)
        {
            mgn.ManagePanelClick();
        }

        //Check for Keypresses
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            mgn.ManageKeyBoardClick(e);
        }


    }
}
