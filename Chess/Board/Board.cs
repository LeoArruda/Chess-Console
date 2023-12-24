namespace board
{
    internal class Board
    {
        public int rows { get; set; }
        public int columns { get; set; }
        private Piece[,] pieces;

        public Board(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            pieces = new Piece[rows, columns];
        }

        public Piece piece(int row, int column)
        {
            return pieces[row, column];
        }

        public Piece piece(Position pos)
        {
            return pieces[pos.row, pos.column];
        }

        public bool existsPiece(Position pos)
        {
            verifyPosition(pos);
            return piece(pos) != null;
        }

        public void putPiece(Piece p, Position pos)
        {
            if (existsPiece(pos))
            {
                throw new BoardException("There's a piece in this position!");
            }
            pieces[pos.row, pos.column] = p;
            p.position = pos;
        }

        public Piece removePiece(Position pos)
        {
            if (piece(pos) == null)
            {
                return null;
            }
            Piece aux = piece(pos);
            aux.position = null;
            pieces[pos.row, pos.column] = null;
            return aux;
        }

        public bool isValidPosition(Position pos)
        {
            if (pos.row < 0 || pos.row >= rows || pos.column < 0 || pos.column >= columns)
            {
                return false;
            }
            return true;
        }

        public void verifyPosition(Position pos)
        {
            if (!isValidPosition(pos))
            {
                throw new BoardException("Invalid Position!");
            }
        }
    }
}
