using board;
namespace chess
{
    internal class Pawn : Piece
    {
        private ChessMatch match;
        public Pawn(Board board, Color color, ChessMatch match) : base(board, color)
        {
            this.match = match;
        }
        private bool canMove(Position position)
        {
            Piece p = board.piece(position);
            return p == null || p.color != color;
        }
        private bool hasEnemy(Position position)
        {
            Piece p = board.piece(position);
            return p != null || p.color != color;
        }
        private bool isFree(Position position)
        {
            return board.piece(position) == null;
        }
        public override bool[,] possibleMoviments()
        {
            bool[,] brd = new bool[board.rows, board.columns];
            Position pos = new Position(0, 0);

            if (color == Color.White)
            {
                //
                pos.setValues(position.row - 1, position.column);
                if (board.isValidPosition(pos) && isFree(pos))
                {
                    brd[pos.row, pos.column] = true;
                }

                // P2
                pos.setValues(position.row - 2, position.column);
                Position p2 = new Position(position.row - 1, position.column);
                if (board.isValidPosition(p2) && isFree(p2) && board.isValidPosition(pos) && isFree(pos)) //add amount of movemnts
                {
                    brd[pos.row, pos.column] = true;
                }

                //
                pos.setValues(position.row - 1, position.column - 1);
                if (board.isValidPosition(pos) && hasEnemy(pos))
                {
                    brd[pos.row, pos.column] = true;
                }

                //
                pos.setValues(position.row - 1, position.column + 1);
                if (board.isValidPosition(pos) && hasEnemy(pos))
                {
                    brd[pos.row, pos.column] = true;
                }
            }
            else
            {
                //
                pos.setValues(position.row + 1, position.column);
                if (board.isValidPosition(pos) && isFree(pos))
                {
                    brd[pos.row, pos.column] = true;
                }

                // P2
                pos.setValues(position.row + 2, position.column);
                Position p2 = new Position(position.row + 1, position.column);
                if (board.isValidPosition(p2) && isFree(p2) && board.isValidPosition(pos) && isFree(pos)) //add amount of ovements control
                {
                    brd[pos.row, pos.column] = true;
                }

                //
                pos.setValues(position.row + 1, position.column - 1);
                if (board.isValidPosition(pos) && hasEnemy(pos))
                {
                    brd[pos.row, pos.column] = true;
                }

                //
                pos.setValues(position.row + 1, position.column + 1);
                if (board.isValidPosition(pos) && hasEnemy(pos))
                {
                    brd[pos.row, pos.column] = true;
                }

                // En Passant Special Move
                /*
                if (position.row == 4)
                {
                    Position left = new Position(position.row, position.column - 1);
                    if (board.isValidPosition(pos) && hasEnemy(pos) && board.piece(left) == "EnPassant")
                    {
                        brd[pos.row+1, left.column] = true;
                    }
                    Position right = new Position(position.row, position.column + 1);
                    if (board.isValidPosition(pos) && hasEnemy(pos) && board.piece(left) == "EnPassant")
                    {
                        brd[pos.row+1, right.column] = true;
                    }
                }*/
            }
            return brd;
        }

        public override string ToString()
        {
            return "P";
        }
    }
}
