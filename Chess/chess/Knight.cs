using board;
namespace chess
{
    internal class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color) { }
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
