using board;
using chess;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;
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
            Console.WriteLine("Check ? : "+ match.check);

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
            /* TODO
                white chess king	♔	U+2654	&#9812;	&#x2654;
                white chess queen	♕	U+2655	&#9813;	&#x2655;
                white chess rook	♖	U+2656	&#9814;	&#x2656;
                white chess bishop	♗	U+2657	&#9815;	&#x2657;
                white chess knight	♘	U+2658	&#9816;	&#x2658;
                white chess pawn	♙	U+2659	&#9817;	&#x2659;
                black chess king	♚	U+265A	&#9818;	&#x265A;
                black chess queen	♛	U+265B	&#9819;	&#x265B;
                black chess rook	♜	U+265C	&#9820;	&#x265C;
                black chess bishop	♝	U+265D	&#9821;	&#x265D;
                black chess knight	♞	U+265E	&#9822;	&#x265E;
                black chess pawn	♟︎	U+265F	&#9823;	&#x265F;
                */

            if (piece == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (piece.color == Color.White)
                {
                    Console.OutputEncoding = Encoding.UTF8;
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