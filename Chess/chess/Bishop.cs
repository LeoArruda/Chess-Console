﻿using board;
namespace chess
{
    internal class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color) { }
        public override bool[,] possibleMoviments()
        {
            throw new System.NotImplementedException();
        }

        public override string ToString()
        {
            return "B";
        }
    }
}