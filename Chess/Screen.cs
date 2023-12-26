using board;
using chess;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text.RegularExpressions;

namespace Chess
{
    internal class Screen
    {
        public static void displayMatch(ChessMatch match)
        {
            Screen.displayBoard(match.board);
            Console.WriteLine();
            displayCapturedPieces(match);
            Console.WriteLine("Turn: " + match.turn);
            if (!match.hasFinished)
            {
                Console.WriteLine("Waiting Player: " + match.currentPlayer);
                if (match.check)
                {
                    Console.WriteLine("CHECK!");
                }
            }
            else
            {
                Console.WriteLine("CHECKMATE!");
                Console.WriteLine("Winner: " + match.currentPlayer);
            }
        }
        public static void displayCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Captured Pieces:");
            Console.Write("Whites : ");
            displaySet(match.capturedPieces(Color.White));
            Console.WriteLine();
            Console.Write("Blacks : ");
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            displaySet(match.capturedPieces(Color.Black));
            Console.ForegroundColor = aux;
            Console.WriteLine();
        }
        public static void displaySet(HashSet<Piece> pieceSet)
        {
            Console.Write("[");
            foreach (Piece x in pieceSet)
            {
                Console.Write(x + " ");
            }
            Console.Write("]");
        }

        public static void displayBoard(Board board)
        {

            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    displayPiece(board.piece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }
        public static void displayBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor newBackground = ConsoleColor.DarkGray;
            for (int i = 0; i < board.rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.columns; j++)
                {
                    if (possiblePositions[i, j])
                    {
                        Console.BackgroundColor = newBackground;
                    }
                    else
                    {
                        Console.BackgroundColor = originalBackground;
                    }
                    displayPiece(board.piece(i, j));
                    Console.BackgroundColor = originalBackground;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void displayPiece(Piece piece)
        {

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.color == Color.White)
                {
                    Console.Write(piece);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }
        }

        public static ChessPosition readChessPosition()
        {
            string s = Console.ReadLine();
            char col = s[0];
            int row = int.Parse(s[1] + "");
            return new ChessPosition(col, row);
        }
    }
}