using System;
using System.Collections.Generic;

namespace Milos_Bencek_technical_challenge_02122021.Interfaces
{
    public interface IGameServices
    {
        Tuple<int, int> DirectionToCoordinates(int x, int y, bool horizontal, int size);
        bool DoesOverlapAnotherShip(int[] indexes, IBoard board);
        Tuple<int, int> IndexToPosition(int index, int sizeX);
        int PositionCharToInt(char c);
        int[] PositionsToIndexes(int startX, int startY, int endX, int endY, int sizeX);
        int PositionToIndex(int x, int y, int sizeX);
        bool ShipsExist(List<IShip> ships);
    }
}