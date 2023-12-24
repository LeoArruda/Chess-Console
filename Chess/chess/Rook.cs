using board;
namespace chess
{
    internal class Rook : Piece
    {
        public Rook(Board board, Color color) : base(board, color) { }
        public override bool[,] possibleMoviments()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "R";
        }
    }
}
