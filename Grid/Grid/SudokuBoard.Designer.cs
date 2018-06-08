namespace Sudoku
{
    partial class Game
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            this.Solvebutton = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.CancelButton_ = new System.Windows.Forms.Button();
            this.GameBoardPanel = new Sudoku.BufferedPanel();
            this.SuspendLayout();
            // 
            // Solvebutton
            // 
            this.Solvebutton.Location = new System.Drawing.Point(65, 31);
            this.Solvebutton.Name = "Solvebutton";
            this.Solvebutton.Size = new System.Drawing.Size(75, 23);
            this.Solvebutton.TabIndex = 1;
            this.Solvebutton.Text = "Solve";
            this.Solvebutton.UseVisualStyleBackColor = true;
            this.Solvebutton.Click += new System.EventHandler(this.SolveButton_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(65, 60);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(158, 17);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Follow backtrack algorithm?";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Click += new System.EventHandler(this.FollowAlongCheck_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.Enabled = false;
            this.ClearButton.Location = new System.Drawing.Point(350, 31);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 3;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // CancelButton_
            // 
            this.CancelButton_.Enabled = false;
            this.CancelButton_.Location = new System.Drawing.Point(207, 31);
            this.CancelButton_.Name = "CancelButton_";
            this.CancelButton_.Size = new System.Drawing.Size(75, 23);
            this.CancelButton_.TabIndex = 4;
            this.CancelButton_.Text = "Cancel";
            this.CancelButton_.UseVisualStyleBackColor = true;
            this.CancelButton_.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // GameBoardPanel
            // 
            this.GameBoardPanel.Location = new System.Drawing.Point(65, 89);
            this.GameBoardPanel.Name = "GameBoardPanel";
            this.GameBoardPanel.Size = new System.Drawing.Size(360, 360);
            this.GameBoardPanel.TabIndex = 0;
            this.GameBoardPanel.Click += new System.EventHandler(this.P1_Click);
            this.GameBoardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(484, 461);
            this.Controls.Add(this.CancelButton_);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.GameBoardPanel);
            this.Controls.Add(this.Solvebutton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Game";
            this.Text = "Sudoku Solver";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button Solvebutton;
        private BufferedPanel GameBoardPanel;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button CancelButton_;
    }
}

