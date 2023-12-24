using board;
using chess;
using System;

namespace Chess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);
            board.putPiece(new Rook(board, Color.Black), new Position(0, 0));
            board.putPiece(new Queen(board, Color.Black), new Position(0, 3));
            board.putPiece(new Knight(board, Color.Black), new Position(0, 6));
            board.putPiece(new Rook(board, Color.Black), new Position(1, 3));
            board.putPiece(new King(board, Color.Black), new Position(2, 4));
            board.putPiece(new Pawn(board, Color.Black), new Position(3, 2));
            board.putPiece(new Pawn(board, Color.Black), new Position(3, 7));
            Screen.displayBoard(board);
        }
    }
}
