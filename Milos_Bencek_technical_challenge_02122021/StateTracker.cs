﻿using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Interfaces;

namespace Milos_Bencek_technical_challenge_02122021.Services
{
    class StateTracker
    {

        private Manager _manager;


        public StateTracker(int x = 10, int y = 10)
        {
            _manager = new Manager(x, y);

        }



        public IServiceResult<IShip> CreateShip(ShipClass shipClass)
        {
            return _manager.GameInProgress 
                ? new ServiceResult<IShip>(null, false, "Can't create ship while game is in progress!") : _manager.CreateShip(shipClass);
        }


        // Coordinates are defined by letter for horizontal axis, f.e: 'E4'
        public IServiceResult AddShipToBoard(IShip ship, char x, int y, bool horizontal)
        {
            return _manager.GameInProgress 
                ? new ServiceResult<IShip>(null, false, "Can't add ship to board while game is in progress!") : _manager.PlaceShipOnBoard(ship, x, y, horizontal);
        }


        // Start game. 'GameInProgress' in Manager is set to true which prevents setup operations during the game
        public IServiceResult StartGame()
        {
            return _manager.Start();
        }


        // Hit the board at coordinates
        public IServiceResult Attack(char x, int y)
        {
            return _manager.Lost ? new ServiceResult(false, "Can't attack when game is lost!") : _manager.Hit(x, y);
        }


        // 'true' results indicates that game is lost, all ships were sunk
        public IServiceResult Lost()
        {
            return _manager.IsLost();
        }

    }
}
