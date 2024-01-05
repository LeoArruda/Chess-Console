using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using board;

namespace chess
{
    internal class ChessMatch
    {
        public Board board { get; private set; }
        public int turn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool hasFinished { get; private set; }
        public bool check { get; private set; }
        public Piece enPassantVulnerable { get; private set; }

        private HashSet<Piece> pieces;
        private HashSet<Piece> captured;

        public ChessMatch()
        {
            board = new Board(8, 8);
            turn = 1;
            currentPlayer = Color.White;
            hasFinished = false;
            check = false;
            enPassantVulnerable = null;
            captured = new HashSet<Piece>();
            pieces = new HashSet<Piece>();
            putPieces();
        }

        public Piece movePiece(Position origin, Position target)
        {
            Piece p = board.removePiece(origin);
            p.increaseAmtMovements();
            Piece capturedPiece = board.removePiece(target);
            board.putPiece(p, target);
            if (capturedPiece != null)
            {
                captured.Add(capturedPiece);
            }
            // Special move small Castling
            if (p is King && target.column == origin.column + 2)
            {
                Position originHook = new Position(origin.row, origin.column + 3);
                Position targetHook = new Position(origin.row, origin.column + 1);
                Piece H = board.removePiece(originHook);
                H.increaseAmtMovements();
                board.putPiece(H, targetHook);
            }
            // Special big small Castling
            if (p is King && target.column == origin.column - 2)
            {
                Position originHook = new Position(origin.row, origin.column -4);
                Position targetHook = new Position(origin.row, origin.column - 1);
                Piece H = board.removePiece(originHook);
                H.increaseAmtMovements();
                board.putPiece(H, targetHook);
            }
            // Special Move en Passant
            if (p is Pawn)
            {
                if (origin.column != target.column && capturedPiece == null)
                {
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(target.row + 1, target.column);
                    }
                    else
                    {
                        posP = new Position(target.row - 1, target.column);
                    }
                    capturedPiece = board.removePiece(posP);
                    captured.Add(capturedPiece);
                }
            }
            return capturedPiece;
        }

        public void chessMove(Position origin, Position target)
        {
            Piece capturedPiece = movePiece(origin, target);

            if (isInCheck(currentPlayer))
            {
                undoChessMove(origin, target, capturedPiece);
                throw new BoardException("You cannot put yourself in check!");
            }

            Piece p = board.piece(target);

            // Special Move Promotion
            if (p is Pawn)
            {
                if ((p.color == Color.White && target.row ==0) || (p.color == Color.Black && target.row ==7))
                {
                    p = board.removePiece(target);
                    pieces.Remove(p);
                    Piece queen = new Queen(board, p.color);
                    board.putPiece(queen, target);
                    pieces.Add(queen);
                }
            }

            if (isInCheck(opponentColor(currentPlayer)))
            {
                check = true;
            }
            else
            {
                check = false;
            }
            if (isCheckmate(opponentColor(currentPlayer)))
            {
                hasFinished = true;
            }
            else
            {
                turn++;
                changePlayer();
            }

            // Special Move En Passant
            if (p is Pawn && (target.row == origin.row -2 || target.row == origin.row + 2))
            {
                enPassantVulnerable = p;
            }
            else
            {
                enPassantVulnerable = null;
            }

        }

        public void undoChessMove(Position origin, Position target, Piece capturedPiece)
        {
            Piece p = board.removePiece(target);
            p.decreaseAmtMovements();
            if (capturedPiece != null)
            {
                board.putPiece(capturedPiece, target);
                captured.Remove(capturedPiece);
            }
            board.putPiece(p, origin);

            // Special move small Castling
            if (p is King && target.column == origin.column + 2)
            {
                Position originHook = new Position(origin.row, origin.column + 3);
                Position targetHook = new Position(origin.row, origin.column + 1);
                Piece H = board.removePiece(targetHook);
                H.decreaseAmtMovements();
                board.putPiece(H, originHook);
            }
            // Special big small Castling
            if (p is King && target.column == origin.column - 2)
            {
                Position originHook = new Position(origin.row, origin.column - 4);
                Position targetHook = new Position(origin.row, origin.column - 1);
                Piece H = board.removePiece(targetHook);
                H.decreaseAmtMovements();
                board.putPiece(H, originHook);
            }
            // Special Move En Passant
            if (p is Pawn)
            {
                if (origin.column != target.column && capturedPiece == enPassantVulnerable)
                {
                    Piece pawn = board.removePiece(target);
                    Position posP;
                    if (p.color == Color.White)
                    {
                        posP = new Position(3, target.column);
                    }
                    else
                    {
                        posP = new Position(4, target.column);
                    }
                    board.putPiece(pawn, posP);
                }
            }

        }

        public HashSet<Piece> capturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in captured)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Piece> inGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();
            foreach (Piece x in pieces)
            {
                if (x.color == color)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(capturedPieces(color));
            return aux;
        }

        public void putNewPiece(char column, int row, Piece piece)
        {
            board.putPiece(piece, new ChessPosition(column, row).toPosition());
            pieces.Add(piece);
        }

        public void verifyOrigin(Position pos)
        {
            if (board.piece(pos) == null)
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
            if (!board.piece(origin).possibleMovement(target))
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

        private Color opponentColor(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }
        private Piece king(Color color)
        {
            foreach (Piece x in inGamePieces(color))
            {
                if (x is King)
                {
                    return x;
                }
            }
            return null;
        }
        public bool isInCheck(Color color)
        {
            Piece K = king(color);
            if (K == null)
            {
                throw new BoardException("Não tem rei da cor " + color + " no tabuleiro!");
            }
            foreach (Piece x in inGamePieces(opponentColor(color)))
            {
                bool[,] brd = x.possibleMovements();
                if (brd[K.position.row, K.position.column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool isCheckmate(Color color)
        {
            if (!isInCheck(color))
            {
                return false;
            }
            foreach (Piece x in inGamePieces(color))
            {
                bool[,] brd = x.possibleMovements();
                for (int i = 0; i < board.rows; i++)
                {
                    for (int j = 0; j < board.rows; j++)
                    {
                        if (brd[i, j])
                        {
                            Position origin = x.position;
                            Position target = new Position(i, j);
                            Piece capturedPiece = movePiece(origin, target);
                            bool checkTest = isInCheck(color);
                            undoChessMove(origin, target, capturedPiece);
                            if (!checkTest)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private void putPieces()
        {
            /*putNewPiece('a', 1, new Rook(board, Color.White));
            putNewPiece('b', 1, new Knight(board, Color.White));*/
            putNewPiece('c', 1, new Bishop(board, Color.White));
            putNewPiece('d', 1, new Queen(board, Color.White));
            putNewPiece('e', 1, new King(board, Color.White, this));
            putNewPiece('f', 1, new Bishop(board, Color.White));
            /*putNewPiece('g', 1, new Knight(board, Color.White));*/
            putNewPiece('h', 1, new Rook(board, Color.White));
            putNewPiece('a', 2, new Pawn(board, Color.White, this));
            /*putNewPiece('b', 2, new Pawn(board, Color.White));
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
            putNewPiece('e', 8, new King(board, Color.Black, this));
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
