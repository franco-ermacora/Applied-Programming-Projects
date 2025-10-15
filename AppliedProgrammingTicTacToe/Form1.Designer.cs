namespace TicTacToeUI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            int size = 100;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button btn = new Button();
                    btn.Name = $"btn{i}{j}";
                    btn.Text = "";
                    btn.Width = size;
                    btn.Height = size;
                    btn.Left = j * size;
                    btn.Top = i * size;
                    btn.Font = new System.Drawing.Font("Arial", 24);
                    btn.Click += new EventHandler(ButtonClick);
                    Controls.Add(btn);
                }
            }

            Label lblTurn = new Label();
            lblTurn.Name = "lblTurn";
            lblTurn.Text = "Turno del jugador: X";
            lblTurn.Width = 300;
            lblTurn.Top = 310; // Justo debajo del tablero
            lblTurn.Left = 10;
            lblTurn.Font = new System.Drawing.Font("Arial", 14);
            Controls.Add(lblTurn);
            lblTurn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            this.ClientSize = new System.Drawing.Size(3 * size, 3 * size + 50);
            this.Name = "Form1";
            this.Text = "Tic-Tac-Toe";
            this.ResumeLayout(false);
        }
    }
}
