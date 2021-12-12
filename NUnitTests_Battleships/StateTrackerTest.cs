using FluentValidation.Results;
using Milos_Bencek_technical_challenge_02122021;
using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Interfaces;
using NUnit.Framework;

namespace NUnitTests_Battleships
{
    public class StateTrackerTest
    {
        private ShipClass _shipClass;
        private IStateTracker _stateTracker;
        private IShip _ship;
        private IServiceResult<IShip> _createShipResult;
        private char _posX;
        private int _posY;
        private bool _horizontal;
        private IServiceResult<ValidationResult> _addShipResult;

        [SetUp]
        public void Setup()
        {
            _stateTracker = new StateTracker(10, 10);
            _shipClass = ShipClass.Destroyer;
            _createShipResult = _stateTracker.CreateShip(_shipClass);
            _ship = _createShipResult.Data;
            _posX = 'b';
            _posY = 2;
            _horizontal = true;
            _addShipResult = _stateTracker.AddShipToBoard(_ship, _posX, _posY, _horizontal);
        }



        [Test]
        public void CreateShip()
        {
            var result = _stateTracker.CreateShip(_shipClass);

            var ship = result.Data;

            Assert.That(ship, Is.TypeOf<Ship>());
            Assert.That(ship.ShipClass, Is.EqualTo(ShipClass.Destroyer.ToString()));
        }


        [Test]
        public void AddShipToBoard()
        {
            _stateTracker = new StateTracker(10, 10);
            _createShipResult = _stateTracker.CreateShip(_shipClass);
            _ship = _createShipResult.Data;
            _addShipResult = _stateTracker.AddShipToBoard(_ship, 'b', 2, true);

            Assert.That(_addShipResult.Status, Is.True);
            Assert.That(_addShipResult.Message, Is.EqualTo("Success!"));
        }


        [Test]
        public void StartGame_NoShipsOnBoard()
        {
            _stateTracker = new StateTracker(10, 10);

            var startGameResult = _stateTracker.StartGame();

            Assert.That(startGameResult.Status, Is.False);
            Assert.That(startGameResult.Message, Is.EqualTo("Place at least one ship before you start game!"));
        }


        [Test]
        public void StartGame_WithAtLeastOneShipOnBoard()
        {
            var startGameResult = _stateTracker.StartGame();

            Assert.That(startGameResult.Status, Is.True);
            Assert.That(startGameResult.Message, Is.EqualTo("Game has started. Game content can't be changed!"));
        }


        [Test]
        public void StartGame_GameAlreadyInProgress()
        {
            _ = _stateTracker.StartGame();
            IServiceResult startGameResult = _stateTracker.StartGame();


            Assert.That(startGameResult.Status, Is.False);
            Assert.That(startGameResult.Message, Is.EqualTo("Game is already in progress!"));
        }



        [Test]
        public void Attack_GameIsNotRunning()
        {
            var attackResult = _stateTracker.Attack('b', 2);

            Assert.That(attackResult.Status, Is.False);
            Assert.That(attackResult.Message, Is.EqualTo("Game is not running!"));

        }


        [Test]
        public void Attack_Hit()
        {
            _ = _stateTracker.StartGame();

            var attackResult = _stateTracker.Attack('b', 2);

            Assert.That(attackResult.Status, Is.True);
            Assert.That(attackResult.Message, Is.EqualTo("Hit at: B2"));
        }


        [Test]
        public void Attack_HitAndSinkLastShip()
        {
                            _ = _stateTracker.StartGame();
                            _ = _stateTracker.Attack('b', 2);
                            _ = _stateTracker.Attack('c', 2);
            var attackResult =  _stateTracker.Attack('d', 2);

            Assert.That(attackResult.Status, Is.True);
            Assert.That(attackResult.Message, Is.EqualTo("Hit at: D2  - Last ship was sunk. Game is lost!"));
        }        
        
        

        [Test]
        public void Attack_Miss()
        {
            _ = _stateTracker.StartGame();

            var attackResult = _stateTracker.Attack('e', 2);

            Assert.That(attackResult.Status, Is.False);
            Assert.That(attackResult.Message, Is.EqualTo("Miss at: E2"));
        }


        [Test]
        public void Attack_HitGameAlreadyLost()
        {
                            _ = _stateTracker.StartGame();
                            _ = _stateTracker.Attack('b', 2);
                            _ = _stateTracker.Attack('c', 2);
                            _ = _stateTracker.Attack('d', 2);
            var attackResult =  _stateTracker.Attack('b', 2);

            Assert.That(attackResult.Status, Is.False);
            Assert.That(attackResult.Message, Is.EqualTo("Can't attack when game is already lost!"));
        }



        [Test]
        public void GameState_GameIsNotLostYet()
        {
            _ = _stateTracker.StartGame();

            var lostResult = _stateTracker.GameState();

            Assert.That(lostResult.Status, Is.False);
            Assert.That(lostResult.Message, Is.EqualTo("Game is not lost yet!"));
        }


        [Test]
        public void GameState_GameIsLost()
        {
            _ = _stateTracker.StartGame();
            _ = _stateTracker.Attack('b', 2);
            _ = _stateTracker.Attack('c', 2);
            _ = _stateTracker.Attack('d', 2);

            var lostResult = _stateTracker.GameState();

            Assert.That(lostResult.Status, Is.True);
            Assert.That(lostResult.Message, Is.EqualTo("Game is lost!"));
        }


    }
}