using board;
using System;
namespace chess
{
    internal class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color) { }
        private bool canMove(Position position)
        {
            Piece p = board.piece(position);
            return p == null || p.color != color;
        }
        public override bool[,] possibleMoviments()
        {
            bool[,] brd = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            // Up
            pos.setValues(position.row - 1, position.column);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.row = pos.row -1;
                Console.Write('.');
            }
            // Down
            pos.setValues(position.row +1, position.column);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.row = pos.row + 1;
                Console.Write('\\');
            }
            // Right
            pos.setValues(position.row, position.column+1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.column = pos.column + 1;
                Console.Write('|');
            }
            // Left
            pos.setValues(position.row, position.column -1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != this.color)
                {
                    break;
                }
                pos.column = pos.column - 1;
                Console.Write(':');
            }
            return brd;
        }
        public override string ToString()
        {
            return "R";
        }
    }
}
