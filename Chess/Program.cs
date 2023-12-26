using board;
using chess;
using System;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChessMatch match = new ChessMatch();
                while (!match.hasFinished)
                {
                    try
                    {
                        Console.Clear();
                        Screen.displayBoard(match.board);

                        Console.WriteLine();
                        Console.WriteLine("Turn: " + match.turn);
                        Console.WriteLine("Waiting Player: " + match.currentPlayer);
                        Console.Write("Origin: ");
                        Position origin = Screen.readChessPosition().toPosition();
                        match.verifyOrigin(origin);

                        bool[,] possiblePositions = match.board.piece(origin).possibleMovements();
                        Console.Clear();
                        Screen.displayBoard(match.board, possiblePositions);

                        Console.Write("Target: ");
                        Position target = Screen.readChessPosition().toPosition();
                        match.verifyTarget(origin, target);

                        match.movePiece(origin, target);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }

                }
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
