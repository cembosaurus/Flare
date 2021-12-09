using Milos_Bencek_technical_challenge_02122021.Interfaces;
using System;

namespace Milos_Bencek_technical_challenge_02122021.Entities
{
    public class Board : IBoard
    {
        private readonly int _sizeX;
        private Square[] _grid;


        public Board(int sizeX, int sizeY)
        {
            _sizeX = sizeX;
            _grid = new Square[sizeX * sizeY];
            Array.Fill(_grid, new Square());
        }



        public Square[] Grid
        {
            get { return _grid; }
            set { _grid = value; }
        }


        public int SizeX => _sizeX;

    }
}
