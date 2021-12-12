using FluentValidation.Results;
using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Interfaces;
using Milos_Bencek_technical_challenge_02122021.Models;
using Milos_Bencek_technical_challenge_02122021.Validation;
using System.Collections.Generic;

namespace Milos_Bencek_technical_challenge_02122021.Services
{
    public class Manager : IShipManager, IGameManager
    {

        private IServiceParameters<Position> _parameters;
        private List<IShip> _ships;
        private readonly IBoard _board;
        private bool _gameInProgress;
        private bool _lost;


        public Manager(int x, int y)
        {
            _board = new Board(x, y);
            _ships = new List<IShip>();
            _parameters = new ServiceParameters<Position>(new Position());
        }



        public bool GameInProgress => _gameInProgress;

        public bool Lost => _lost;



        public IServiceResult<IShip> CreateShip(ShipClass shipClass)
        {
            var ship = Factory.CreateShip(shipClass);

            return new ServiceResult<IShip>(ship, true, string.Format("Ship of type {0} was created!", shipClass.ToString()));
        }



        public IServiceResult<ValidationResult> PlaceShipOnBoard(IShip ship, char xChar, int y, bool horizontal)
        {
            // Converting letter to number in coordinates. F.e: 'B,7' to '2,7'
            var x = GameServices.Service.PositionCharToInt(xChar);

            // Calculating end position coordinates based on direction and size of ship:
            var endCoordinates = GameServices.Service.DirectionToCoordinates(x, y, horizontal, ship.Body.Length);

            // Crteating 'Position' model for validation for validation request:
            _parameters.Data = new Position
            {
                Board = _board,
                startX = x,
                startY = y,
                endX = endCoordinates.Item1,
                endY = endCoordinates.Item2
            };

            // Validate ship position, size and direction before placed on board:
            var validationResult = PositionValidator.Position.Validate(_parameters.Data);

            // Get indexes of ship position on board:
            var shipIndexes = GameServices.Service.PositionsToIndexes
                (
                _parameters.Data.startX, 
                _parameters.Data.startY, 
                _parameters.Data.endX, 
                _parameters.Data.endY, 
                _parameters.Data.Board.SizeX
                );

            // Does ship overlap another ship on board?:
            var isColidingWithAnotherShip = GameServices.Service.DoesOverlapAnotherShip(shipIndexes, _parameters.Data.Board);


            if (validationResult.IsValid && !isColidingWithAnotherShip)
            {
                // Temporary variable for looping:
                int index = 0;

                // Joining board array with ship 'body' array by 'Square' model class reference:
                foreach (int i in shipIndexes)
                {
                    var square = new Square { State = true };
                    _board.Grid[i] = ship.Body[index++] = square;
                }

                // Add ship into list of active ships:
                _ships.Add(ship);

                return new ServiceResult<ValidationResult>(validationResult, true, "Success!");
            }


            return new ServiceResult<ValidationResult>(validationResult, false, "Fail!");
        }



        // Attack on coordinates:
        public IServiceResult Hit(char xChar, int y)
        {
            // Converting letter in coordinates to number. F.e: 'B,7' to '2,7'
            var x = GameServices.Service.PositionCharToInt(xChar);

            // Convert attacked X,Y position to board index:
            var index = GameServices.Service.PositionToIndex(x, y, _board.SizeX);

            // Identify related Square on board:
            var square = _board.Grid[index];
            

            if (square.State)
            {
                // Marking square on board occupied by ship as 'hit':
                square.State = false;

                // Indication whether there is any remaining ships after hit:
                _lost = !GameServices.Service.ShipsExist(_ships);

                var message = _lost ? "  - Last ship was sunk. Game is lost!" : "";

                return new ServiceResult(true, string.Format("Hit at: {0}{1}{2}", char.ToUpper(xChar), y, message));
            }


            return new ServiceResult(false, string.Format("Miss at: {0}{1}", char.ToUpper(xChar), y));
        }



        // Start Game:
        public IServiceResult Start()
        {
            if (!_gameInProgress)
            {
                if (_ships.Count > 0)
                {
                    _gameInProgress = true;
                    return new ServiceResult(_gameInProgress, "Game has started. Game content can't be changed!");
                }

                return new ServiceResult(_gameInProgress, "Place at least one ship before you start game!");
            }


            return new ServiceResult(!_gameInProgress, "Game is already in progress!");
        }



        // Indicating whether game is lost or not:
        public IServiceResult IsGameLost()
        {
            return _lost ? new ServiceResult(true, "Game is lost!") : new ServiceResult(false, "Game is not lost yet!");
        }

    }
}
