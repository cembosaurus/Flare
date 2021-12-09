using Milos_Bencek_technical_challenge_02122021.Entities;

namespace Milos_Bencek_technical_challenge_02122021.Interfaces
{
    public interface IShip
    {
        Square[] Body { get; set; }
        string ShipClass { get; }
        bool State { get; }
    }
}