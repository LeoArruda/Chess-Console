using board;
namespace chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color) { }
        private bool canMove(Position position)
        {
            Piece p = board.piece(position);
            return p == null || p.color != this.color;
        }
        public override bool[,] possibleMoviments()
        {
            bool[,] brd = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            // Up
            pos.setValues(position.row - 1, position.column);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }

            //Up-Right
            pos.setValues(position.row - 1, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }

            //Right
            pos.setValues(position.row, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }

            //Down-Right
            pos.setValues(position.row + 1, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }
            //Down
            pos.setValues(position.row + 1, position.column);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }
            //Down-Left
            pos.setValues(position.row + 1, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }
            //Left
            pos.setValues(position.row, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }
            //Left-Up
            pos.setValues(position.row - 1, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }
            return brd;

        }

        public override string ToString()
        {
            return "K";
        }
    }
}
