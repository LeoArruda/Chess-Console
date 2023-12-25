using board;
namespace chess
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color) { }
        private bool canMove(Position position)
        {
            Piece p = board.piece(position);
            return p == null || p.color != this.color;
        }
        public override bool[,] possibleMoviments()
        {
            bool[,] brd = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            // 
            pos.setValues(position.row - 1, position.column -2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }

            //
            pos.setValues(position.row - 2, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }

            //
            pos.setValues(position.row -2, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }

            //
            pos.setValues(position.row -1, position.column + 2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }
            //
            pos.setValues(position.row + 1, position.column + 2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }
            //
            pos.setValues(position.row + 2, position.column + 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }
            //
            pos.setValues(position.row + 2, position.column - 1);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }
            //
            pos.setValues(position.row + 1, position.column - 2);
            if (board.isValidPosition(pos) && canMove(pos))
            {
                brd[pos.row, pos.column] = true;
            }
            return brd;

        }

        public override string ToString()
        {
            return "N";
        }
    }
}
