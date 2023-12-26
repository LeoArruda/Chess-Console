using System;
using System.Collections.Generic;
namespace board
{
    internal abstract class Piece
    {
        public Position position { get; set; }
        public Color color { get; protected set; }
        public int amtMovements { get; protected set; }
        public Board board { get; protected set; }

        public Piece(Board board, Color color)
        {

            this.board = board;
            this.color = color;
            this.position = null;
            this.amtMovements = 0;
        }

        public void increaseAmtMovements()
        {
            amtMovements++;
        }

        public void decreaseAmtMovements()
        {
            amtMovements--;
        }

        public bool existsPossibleMovements()
        {
            bool[,] mat = possibleMovements();
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

        public bool canMoveTo(Position pos)
        {
            return possibleMovements()[pos.row, pos.column];
        }

        public bool isPossibleMoviment(Position pos)
        {
            return possibleMovements()[pos.row, pos.column];
        }

        public abstract bool[,] possibleMovements();
    }
}
