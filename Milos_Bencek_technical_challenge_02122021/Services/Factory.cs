using Milos_Bencek_technical_challenge_02122021.Entities;

namespace Milos_Bencek_technical_challenge_02122021.Services
{
    public static class Factory
    {

        public static Ship CreateShip(ShipClass shipClass)
        {
            return new Ship(shipClass);
        }

    }
}
