using FluentValidation.Results;
using Milos_Bencek_technical_challenge_02122021.Entities;

namespace Milos_Bencek_technical_challenge_02122021.Interfaces
{
    public interface IStateTracker
    {
        IServiceResult<ValidationResult> AddShipToBoard(IShip ship, char x, int y, bool horizontal);
        IServiceResult Attack(char x, int y);
        IServiceResult<IShip> CreateShip(ShipClass shipClass);
        IServiceResult GameState();
        IServiceResult StartGame();
    }
}