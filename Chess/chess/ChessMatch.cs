using System;
using System.Collections.Generic;
using board;

namespace chess
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool hasFinished { get; private set; }
        public bool check {  get; private set; }
        public Piece enPassantVulnerable { get; private set; }

        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;

        public ChessMatch() 
        {
            board = new Board(8,8);
            turn = 1;
            currentPlayer = Color.White;
            hasFinished = false;
            check = false;
            enPassantVulnerable = null;
            captured = new HashSet<Piece>();
            pieces = new HashSet<Piece>();
            putPieces();
        }

        public void movePiece(Position origin, Position target) 
        {
            Piece p = board.removePiece(origin);
            p.increaseAmtMovements();
            Piece capturedPiece = board.removePiece(target);
            board.putPiece(p, target);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            turn++;
            changePlayer();
            //return capturedPiece
        }

        public void putNewPiece(char column, int row, Piece piece)
        {
            board.putPiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }

        public void verifyOrigin(Position pos)
        {
            if ( board.piece(pos) == null)
            {
                throw new BoardException("There's no piece in that position");
            }
            if (currentPlayer != board.piece(pos).color) 
            {
                throw new BoardException("That's not your piece!");
            }
            if (!board.piece(pos).existsPossibleMovements())
            {
                throw new BoardException("There are no possible movements to that piece");
            }

        }

        public void verifyTarget(Position origin, Position target)
        {
            if ( !board.piece(origin).canMoveTo(target))
            {
                throw new BoardException("Invalid target position!");
            }
        }

        public void changePlayer()
        {
            if (currentPlayer == Color.White)
            {
                currentPlayer = Color.Black;
            } 
            else
            {
                currentPlayer = Color.White;
            }
        }

        private void putPieces()
        {
            /*putNewPiece('a', 1, new Rook(board, Color.White));
            putNewPiece('b', 1, new Knight(board, Color.White));*/
            putNewPiece('c', 1, new Bishop(board, Color.White));
            putNewPiece('d', 1, new Queen(board, Color.White));
            putNewPiece('e', 1, new King(board, Color.White));
            putNewPiece('f', 1, new Bishop(board, Color.White));
            /*putNewPiece('g', 1, new Knight(board, Color.White));*/
            putNewPiece('h', 1, new Rook(board, Color.White));
            /*putNewPiece('a', 2, new Pawn(board, Color.White));
            putNewPiece('b', 2, new Pawn(board, Color.White));
            putNewPiece('c', 2, new Pawn(board, Color.White));
            putNewPiece('d', 2, new Pawn(board, Color.White));
            putNewPiece('e', 2, new Pawn(board, Color.White));
            putNewPiece('f', 2, new Pawn(board, Color.White));
            putNewPiece('g', 2, new Pawn(board, Color.White));
            putNewPiece('h', 2, new Pawn(board, Color.White));*/

            /*putNewPiece('a', 8, new Rook(board, Color.Black));*/
            putNewPiece('b', 8, new Knight(board, Color.Black));
            putNewPiece('c', 8, new Bishop(board, Color.Black));
            putNewPiece('d', 8, new Queen(board, Color.Black));
            putNewPiece('e', 8, new King(board, Color.Black));
            putNewPiece('f', 8, new Bishop(board, Color.Black));
            putNewPiece('g', 8, new Knight(board, Color.Black));
            /*putNewPiece('h', 8, new Rook(board, Color.Black));
           putNewPiece('a', 7, new Pawn(board, Color.Black));
           putNewPiece('b', 7, new Pawn(board, Color.Black));
           putNewPiece('c', 7, new Pawn(board, Color.Black));
           putNewPiece('d', 7, new Pawn(board, Color.Black));
           putNewPiece('e', 7, new Pawn(board, Color.Black));
           putNewPiece('f', 7, new Pawn(board, Color.Black));
           putNewPiece('g', 7, new Pawn(board, Color.Black));
           putNewPiece('h', 7, new Pawn(board, Color.Black));*/
        }
    }
}
