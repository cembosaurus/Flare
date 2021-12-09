using Milos_Bencek_technical_challenge_02122021.Interfaces;
using System;
using System.Collections.Generic;

namespace Milos_Bencek_technical_challenge_02122021.Services
{
    public sealed class Service
    {

        private static readonly Service _services = new Service();

        static Service() { }
        private Service() { }

        public static Service Get => _services;


        // Converts X,Y position to index on board:
        public int PositionToIndex(int x, int y, int sizeX)
        {
            return (x - 1) + ((y - 1) * sizeX);
        }


        // Converts index on board to X,Y position:
        public static Tuple<int, int> IndexToPosition(int index, int sizeX)
        {
            int x = index - (index / sizeX) * sizeX - 1;
            int y = index / sizeX + 1;

            return new Tuple<int, int>(x, y);
        }


        // Converts letter X axis identifier to X axis position:
        public int PositionCharToInt(char c)
        {
            var ascii = (int)c;

            if (ascii > 64 && ascii < 91)
                return ascii - 64;
            if (ascii > 96 && ascii < 123)
                return ascii - 96;

            return 0;
        }



        // Calculating end X,Y position based on start X,Y position, direction (horizontal/vertical) and size of ship:
        public Tuple<int, int> DirectionToCoordinates(int x, int y, bool horizontal, int size)
        {
            if (horizontal)
                return new Tuple<int, int>(x + --size, y);

            return new Tuple<int, int>(x, y + --size);
        }


        // Converts all X,Y positions to array of board indexes allocated for ship:
        public int[] PositionsToIndexes(int startX, int startY, int endX, int endY, int sizeX)
        {
            int[] arr = new int[0];

            // end position must be equal or bigger than start position:
            if (startX <= endX && startY <= endY && endX <= sizeX)
            {
                /*  Note:
                    horizontal -> true
                    vertical -> false
                    1 square -> false
                */
                bool horizontal = startY == endY;
                horizontal = startX == endX
                    ? false
                    : horizontal;

                if (horizontal)
                {
                    arr = new int[endX - startX + 1];
                    for (int i = 0; i <= endX - startX; i++)
                        arr[i] = PositionToIndex(i + startX, startY, sizeX);

                }
                else
                {
                    arr = new int[endY - startY + 1];
                    for (int i = 0; i <= endY - startY; i++)
                        arr[i] = PositionToIndex(startX, startY + i, sizeX);

                }
            }

            return arr;
        }


        // Searching for 'active' ships in board. 'False' result indicates that all ships are sunk:
        public bool ShipsExist(List<IShip> ships)
        {
            // Read 'State' in all ships (State indicates 'active' or 'sunk'):
            foreach (IShip s in ships)
                if (s.State == true)
                    return true;

            return false;
        }
    }
}
