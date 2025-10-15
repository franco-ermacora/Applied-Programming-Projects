using System;
using System.IO;

public class TicTacToeGame
{
    public string[,] Board { get; private set; }
    public string CurrentPlayer { get; private set; }
    public bool GameOver { get; private set; }
    public string? Winner { get; private set; } // Variable para saber el ganador

    public TicTacToeGame()
    {
        Board = new string[3, 3];
        CurrentPlayer = "X";
        GameOver = false;
        Winner = null; // Nadie ha ganado al inicio
    }

    public bool MakeMove(int row, int col)
    {
        if (row < 0 || row > 2 || col < 0 || col > 2 || Board[row, col] != null || GameOver)
        {
            return false; // Movimiento inválido
        }

        Board[row, col] = CurrentPlayer;

        if (CheckWinner())
        {
            GameOver = true;
            Winner = CurrentPlayer; // Guardamos quién ganó
            SaveResultToFile($"{Winner} wins!");
        }
        else if (IsBoardFull())
        {
            GameOver = true;
            Winner = "Draw"; // Es un empate
            SaveResultToFile("Draw");
        }
        else
        {
            SwitchPlayer();
        }
        return true;
    }

    public void ResetGame()
    {
        Board = new string[3, 3];
        CurrentPlayer = "X";
        GameOver = false;
        Winner = null; // Reiniciamos el ganador
    }

    private void SwitchPlayer()
    {
        CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";
    }

    public bool CheckWinner()
    {
        // Comprobaciones de filas y columnas
        for (int i = 0; i < 3; i++)
        {
            if (Board[i, 0] == CurrentPlayer && Board[i, 1] == CurrentPlayer && Board[i, 2] == CurrentPlayer)
                return true;
            if (Board[0, i] == CurrentPlayer && Board[1, i] == CurrentPlayer && Board[2, i] == CurrentPlayer)
                return true;
        }
        // Comprobaciones de diagonales
        if (Board[0, 0] == CurrentPlayer && Board[1, 1] == CurrentPlayer && Board[2, 2] == CurrentPlayer)
            return true;
        if (Board[0, 2] == CurrentPlayer && Board[1, 1] == CurrentPlayer && Board[2, 0] == CurrentPlayer)
            return true;

        return false;
    }

    public bool IsBoardFull()
    {
        foreach (var cell in Board)
        {
            if (cell == null)
                return false;
        }
        return true;
    }

    private void SaveResultToFile(string result)
    {
        try
        {
            File.AppendAllText("game_results.txt", $"{DateTime.Now}: {result}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar el resultado: {ex.Message}");
        }
    }
}
