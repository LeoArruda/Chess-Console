using System;
using System.Collections.Generic;
namespace board
{
    internal abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int amtMoviments { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {

            this.board = board;
            this.color = color;
            this.position = null;
            this.amtMoviments = 0;
        }

        public void increaseAmtMoviments()
        {
            amtMoviments++;
        }

        public void decreaseAmtMoviments()
        {
            amtMoviments--;
        }

        public bool existsPossibleMoviments()
        {
            bool[,] mat = possibleMoviments();
            for (int i = 0; i < board.rows; i++)
            {
                for (int j = 0; j < board.columns; j++)
                {
                    if (mat[i, j])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool isPossibleMoviment(Position pos)
        {
            return possibleMoviments()[pos.row, pos.column];
        }

        public abstract bool[,] possibleMoviments();
    }
}
