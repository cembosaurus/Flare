using FluentValidation.Results;
using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Services;

namespace Milos_Bencek_technical_challenge_02122021.Interfaces
{
    public interface IShipManager
    {
        IServiceResult<IShip> CreateShip(ShipClass shipClass);
        IServiceResult<ValidationResult> PlaceShipOnBoard(IShip ship, char xChar, int y, bool horizontal);
    }
}