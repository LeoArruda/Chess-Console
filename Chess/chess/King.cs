using board;
namespace chess
{
    internal class King : Piece
    {
        public King(Board board, Color color) : base(board, color) { }
        public override bool[,] possibleMoviments()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "K";
        }
    }
}
