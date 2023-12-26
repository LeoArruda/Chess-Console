using board;
namespace chess
{
    internal class Queen : Piece
    {
        public Queen(Board board, Color color) : base(board, color) { }

        private bool canMove(Position position)
        {
            Piece p = board.piece(position);
            return p == null || p.color != this.color;
        }
        public override bool[,] possibleMovements()
        {
            bool[,] brd = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            // Left
            pos.setValues(position.row, position.column - 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row, pos.column - 1);
            }

            // Right
            pos.setValues(position.row, position.column + 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row, pos.column + 1);
            }

            // Up
            pos.setValues(position.row - 1, position.column);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row - 1, pos.column);
            }

            // Down
            pos.setValues(position.row + 1, position.column);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row + 1, pos.column);
            }

            // Up-Left
            pos.setValues(position.row - 1, position.column - 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row - 1, pos.column - 1);
            }
            // Up-Right
            pos.setValues(position.row - 1, position.column + 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row - 1, pos.column + 1);
            }

            // Down-Right
            pos.setValues(position.row + 1, position.column + 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != color)
                {
                    break;
                }
                pos.setValues(pos.row + 1, pos.column + 1);
            }

            // Down-Left
            pos.setValues(position.row + 1, position.column - 1);
            while (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
                if (board.piece(pos) != null && board.piece(pos).color != this.color)
                {
                    break;
                }
                pos.setValues(pos.row + 1, pos.column - 1);
            }
            return brd;
        }

        public override string ToString()
        {
            return "Q";
        }
    }
}
