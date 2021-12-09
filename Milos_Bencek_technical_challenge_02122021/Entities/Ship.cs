using Milos_Bencek_technical_challenge_02122021.Interfaces;
using System;

namespace Milos_Bencek_technical_challenge_02122021.Entities
{
    public class Ship : IShip
    {
        private readonly string _shipClass;
        private Square[] _body;

        public Ship(ShipClass shipClass)
        {
            _shipClass = shipClass.ToString();
            _body = new Square[(int)shipClass];
            Array.Fill(_body, new Square { State = true });
        }


        public string ShipClass => _shipClass;


        public Square[] Body
        {
            get { return _body; }
            set { _body = value; }
        }


        // Returns 'false' ('sunk') if ship's body doesn't contain any 'true' squares:
        public bool State
        {
            get
            {
                return Array.Exists(_body, b => b.State == true) ? true : false;
            }
        }

    }
}
