using board;
namespace chess
{
    internal class King : Piece
    {
        private ChessMatch match;
        public King(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }

        private bool canMove(Position position)
        {
            Piece p = board.piece(position);
            return p == null || p.color != this.color;
        }

        private bool testRookForCastling(Position pos)
        {
            Piece p = board.piece(pos);
            return p != null && p is Rook && p.color == color && p.amtMovements == 0;
        }
        public override bool[,] possibleMovements()
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

            // special moves
            if (amtMovements == 0 && !match.check)
            {
                // Small Castling -- error before here
                Position pos1 = new Position(pos.row, pos.column + 3);
                if (board.isValidPosition(pos1) && canMove(pos1))
                {
                    if (testRookForCastling(pos1))
                    {
                        Position p1 = new Position(pos.row, pos.column + 1);
                        Position p2 = new Position(pos.row, pos.column + 2);
                        if (board.piece(p1) == null && board.piece(p2) == null)
                        {
                            brd[pos.row, pos.column + 2] = true;
                        }
                    }
                }
                // Big Castling
                Position pos2 = new Position(pos.row, pos.column - 4);
                if (board.isValidPosition(pos2) && canMove(pos2))
                {
                    if (testRookForCastling(pos2))
                    {
                        Position p1 = new Position(pos.row, pos.column - 1);
                        Position p2 = new Position(pos.row, pos.column - 2);
                        Position p3 = new Position(pos.row, pos.column - 3);
                        if (board.piece(p1) == null && board.piece(p2) == null && board.piece(p3) == null)
                        {
                            brd[pos.row, pos.column - 2] = true;
                        }
                    }
                }

            }
            return brd;
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
