using FluentValidation.Results;
using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Interfaces;
using Milos_Bencek_technical_challenge_02122021.Services;
using NUnit.Framework;

namespace NUnitTests_Battleships
{
    class ManagerTest
    {

        private Manager _manager;
        private ShipClass _shipClass;
        private IShip _ship;
        private IServiceResult<IShip> _createShipResult;
        private char _posX;
        private int _posY;
        private bool _horizontal;
        private IServiceResult<ValidationResult> _addShipResult;

        [SetUp]
        public void Setup()
        {
            _posX = 'b';
            _posY = 2;
            _horizontal = true;

            _manager = new Manager(10,10);
            _shipClass = ShipClass.Destroyer;
            _createShipResult = _manager.CreateShip(_shipClass);
            _ship = _createShipResult.Data;
            _addShipResult = _manager.PlaceShipOnBoard(_ship, _posX, _posY, _horizontal);
        }




        [Test]
        public void CreateShip()
        {
            Assert.That(_createShipResult.Status, Is.True);
            Assert.That(_createShipResult.Message, Is.EqualTo("Ship of type Destroyer was created!"));
            Assert.That(_ship, Is.TypeOf<Ship>());
            Assert.That(_ship.ShipClass, Is.EqualTo(ShipClass.Destroyer.ToString()));
        }


        [Test]
        public void PlaceShipOnBoard()
        {
            Assert.That(_addShipResult.Status, Is.True);
            Assert.That(_addShipResult.Message, Is.EqualTo("Success!"));
        }


        [Test]
        [TestCase('c', 2, true, "Hit at: C2")]
        [TestCase('c', 3, false, "Miss at: C3")]
        public void Hit(char xChar, int y, bool resultState, string resultMessage)
        {
            var hitResult = _manager.Hit(xChar, y);

            Assert.That(hitResult.Status, Is.EqualTo(resultState));
            Assert.That(hitResult.Message, Is.EqualTo(resultMessage));

        }


        [Test]
        [TestCase('d', 2, true, "Hit at: D2  - Last ship was sunk. Game is lost!")]
        public void Hit_LastShip(char xChar, int y, bool resultState, string resultMessage)
        {
            _manager.Hit('b', 2);
            _manager.Hit('c', 2);

            var hitResult = _manager.Hit(xChar, y);

            Assert.That(hitResult.Status, Is.EqualTo(resultState));
            Assert.That(hitResult.Message, Is.EqualTo(resultMessage));

        }


        [Test]
        public void Start()
        {
            var startResult = _manager.Start();

            Assert.That(startResult.Status, Is.True);
            Assert.That(startResult.Message, Is.EqualTo("Game has started. Game content can't be changed!"));

        }


        [Test]
        public void Start_GameAlreadyInProgress()
        {
            var startResult = _manager.Start();
            startResult = _manager.Start();

            Assert.That(startResult.Status, Is.False);
            Assert.That(startResult.Message, Is.EqualTo("Game is already in progress!"));

        }


        [Test]
        public void Start_NoShipsOnBoard()
        {
            var manager = new Manager(10,10);

            var startResult = manager.Start();

            Assert.That(startResult.Status, Is.False);
            Assert.That(startResult.Message, Is.EqualTo("Place at least one ship before you start game!"));

        }


        [Test]
        [TestCase('d', 2, true, "Game is lost!")]
        [TestCase('c', 2, false, "Game is not lost yet!")]
        public void IsLost(char x, int y, bool resultState, string resultMessage)
        {
            _manager.Hit('b', 2);
            _manager.Hit('c', 2);
            _manager.Hit(x, y);

            var isLostResult = _manager.IsGameLost();

            Assert.That(isLostResult.Status, Is.EqualTo(resultState));
            Assert.That(isLostResult.Message, Is.EqualTo(resultMessage));
        }

    }
}
