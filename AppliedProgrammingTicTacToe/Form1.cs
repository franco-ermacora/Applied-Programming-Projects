using System;
using System.IO;
using System.Windows.Forms;

namespace TicTacToeUI
{
    public partial class Form1 : Form
    {
        private TicTacToeGame game;

        public Form1()
        {
            InitializeComponent();
            game = new TicTacToeGame();
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            // Extraemos la fila y columna del nombre del botón (ej: "btn12")
            int row = int.Parse(btn.Name[3].ToString());
            int col = int.Parse(btn.Name[4].ToString());

            // Guardamos quién está jugando ANTES de hacer el movimiento
            string playerWhoMoved = game.CurrentPlayer;

            if (game.MakeMove(row, col))
            {
                // El movimiento fue válido, actualizamos el texto del botón
                btn.Text = playerWhoMoved;
                // Actualizar el texto del turno
                Control? lbl = Controls["lblTurn"];
                if (lbl != null && lbl is Label)
                {
                    Label lblTurn = (Label)lbl;
                    if (!game.GameOver)
                        lblTurn.Text = $"Turno del jugador: {game.CurrentPlayer}";
                }

                if (game.GameOver)
                {
                    string message;
                    if (game.Winner == "Draw")
                    {
                        message = "¡Es un empate!";
                    }
                    else
                    {
                        message = $"¡El ganador es {game.Winner}!";
                    }
                    MessageBox.Show(message, "Juego Terminado");

                    // Guardar resultado en archivo con la interfaz usada "Gráfica"
                    SaveGameResult(game.Winner ?? "Desconocido", "Gráfica");

                    ResetBoard();
                }
            }
        }

        public void ResetBoard()
        {
            game.ResetGame();
            // Limpiamos el texto de todos los botones del tablero
            foreach (Control c in Controls)
            {
                if (c is Button && c.Name.StartsWith("btn"))
                {
                    c.Text = "";
                }
            }
            Control? lbl = Controls["lblTurn"];
            if (lbl != null && lbl is Label lblTurn)
            {
                lblTurn.Text = "Turno del jugador: X";
            }
        }

        private void SaveGameResult(string winner, string interfaceUsed)
        {
            try
            {
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"C:\Users\frank\Documents\Applied-Programming-Projects\AppliedProgrammingTicTacToe\game_results.txt");
                string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string line = $"{date} | Ganador: {winner} | Interfaz: {interfaceUsed}";
                File.AppendAllLines(filePath, new[] { line });
            }
            catch (Exception ex)
            {
                // Opcional: manejar error (ej: loguear)
                MessageBox.Show($"Error al guardar el resultado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
