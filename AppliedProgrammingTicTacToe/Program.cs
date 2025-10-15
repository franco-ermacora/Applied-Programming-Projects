using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TicTacToeUI; // Para usar Form1

class Program
{
    // Para adjuntar/detachar consola desde un ejecutable WinExe
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    [DllImport("kernel32.dll")]
    private static extern bool FreeConsole();

    [STAThread]
    static void Main()
    {
        // Estas deben estar antes de cualquier Form
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        bool playAgain = true;

        while (playAgain)
        {
            string? choice = ShowModeSelectionDialog();  // esto muestra un Form

            if (choice == "1")
            {
                AllocConsole();
                StartConsoleGame();

                Console.Write("Queres jugar otra vez? (S/N): ");
                string? answer = Console.ReadLine();
                playAgain = answer != null && answer.Trim().ToUpper() == "S";
                FreeConsole();
            }
            else if (choice == "2")
            {
                // Cuando termina el juego en interfaz, preguntamos cómo seguir
                do
                {
                    using (Form1 gameForm = new Form1())
                    {
                        Application.Run(gameForm);
                    }

                    // Mostrar un diálogo con 3 opciones: consola, interfaz o salir
                    var msgResult = MessageBox.Show(
                        "Queres jugar otra vez pero en consola? (Coloca No para seguir en interfaz, Cancelar para salir)",
                        "Repetir",
                        MessageBoxButtons.YesNoCancel,
                        MessageBoxIcon.Question);

                    if (msgResult == DialogResult.Yes)
                    {
                        // Volver a jugar en consola
                        AllocConsole();
                        StartConsoleGame();

                        Console.Write("Queres jugar otra vez? (S/N): ");
                        string? answer = Console.ReadLine();
                        playAgain = answer != null && answer.Trim().ToUpper() == "S";
                        FreeConsole();

                        // Para forzar salir del ciclo de interfaz
                        if (!playAgain)
                            break;

                        // Volver a mostrar selector de modo para elegir entre consola/interfaz
                        choice = ShowModeSelectionDialog();
                        if (choice != "2")
                            break;  // Cambió el modo, salgo para reiniciar ciclo principal

                    }
                    else if (msgResult == DialogResult.No)
                    {
                        // Seguir en interfaz, repetir
                        playAgain = true; 
                    }
                    else
                    {
                        // Cancelar = salir
                        playAgain = false;
                        break;
                    }
                } while (playAgain);
            }

            else
            {
                playAgain = false;
            }
        }
    }

    static string? ShowModeSelectionDialog()
    {
        using (var selector = new ModeSelectionForm())
        {
            selector.ShowDialog();
            return selector.SelectedMode; // "1", "2" o null
        }
    }



    static void StartConsoleGame()
    {
        TicTacToeGame game = new TicTacToeGame();

        while (!game.GameOver)
        {
            try
            {
                Console.Clear();
            }
            catch (IOException)
            {
                // Si no hay consola real, evitamos el crash
            }

            PrintBoard(game.Board);
            Console.WriteLine($"Turno del jugador: {game.CurrentPlayer}");

            int row = ReadCoordinate("fila (0-2)");
            int col = ReadCoordinate("columna (0-2)");

            if (!game.MakeMove(row, col))
            {
                Console.WriteLine("Movimiento invalido! Presiona Enter para intentar de nuevo.");
                Console.ReadLine(); // Más seguro que ReadKey()
            }
        }

        try
        {
            Console.Clear();
        }
        catch (IOException)
        {
            // Evita crash final si no hay consola
        }

        PrintBoard(game.Board);

        if (game.Winner == "Draw")
        {
            Console.WriteLine("Juego terminado! Es un empate.");
        }
        else
        {
            Console.WriteLine($"Juego terminado! El ganador es {game.Winner}.");
        }

        Console.WriteLine("Presiona Enter para continuar.");
        Console.ReadLine();

        SaveGameResult(game.Winner ?? "Desconocido", "Consola");

    }


    static int ReadCoordinate(string prompt)
    {
        int coordinate;
        Console.Write($"Ingresa la {prompt}: ");
        while (!int.TryParse(Console.ReadLine(), out coordinate) || coordinate < 0 || coordinate > 2)
        {
            Console.WriteLine("Entrada invalida. Por favor, ingresa un numero entre 0 y 2.");
            Console.Write($"Ingresa la {prompt}: ");
        }
        return coordinate;
    }

    static void PrintBoard(string[,] board)
    {
        Console.WriteLine("-------------");
        for (int i = 0; i < 3; i++)
        {
            Console.Write("| ");
            for (int j = 0; j < 3; j++)
            {
                string val = string.IsNullOrEmpty(board[i, j]) ? " " : board[i, j];
                Console.Write($"{val} | ");
            }
            Console.WriteLine("\n-------------");
        }
    }

    static void SaveGameResult(string winner, string interfaceUsed)
    {
        string filePath = @"C:\Users\frank\Documents\Applied-Programming-Projects\AppliedProgrammingTicTacToe\game_results.txt";

        string dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        string line = $"{dateTime} | Ganador: {winner} | Interfaz: {interfaceUsed}";

        try
        {
            System.IO.File.AppendAllText(filePath, line + Environment.NewLine);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error guardando el resultado: {ex.Message}");
        }
    }

}
