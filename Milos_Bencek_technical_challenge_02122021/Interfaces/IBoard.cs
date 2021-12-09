using Milos_Bencek_technical_challenge_02122021.Entities;

namespace Milos_Bencek_technical_challenge_02122021.Interfaces
{
    public interface IBoard
    {
        Square[] Grid { get; set; }
        int SizeX { get; }
    }
}