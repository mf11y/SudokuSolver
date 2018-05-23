using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sudoku
{
    public partial class Game : Form
    {
        private BoardManagement mgn;

        public Game()
        {
            InitializeComponent();

            mgn = new BoardManagement(panel1);

            this.KeyPreview = true;
        }

        //Paint Grid Lines
        private void Panel1_Paint(object sender, PaintEventArgs e) => mgn.DrawBoard();

        //When mouse clicked inside panel, get location
        private void P1_Click(object sender, EventArgs e) => mgn.ManagePanelClick();

        //Check for Keypresses
        private void Form1_KeyUp(object sender, KeyEventArgs e) => mgn.ManageKeyBoardClick(e);

        async private void solve_button(object sender, EventArgs e)
        {
            panel1.Enabled = false;
            await Task.Run(() => mgn.Solve());
            panel1.Enabled = true;
        }
    }
}
