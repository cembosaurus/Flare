using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Interfaces;
using Milos_Bencek_technical_challenge_02122021.Services;
using NUnit.Framework;
using System.Collections.Generic;

namespace NUnitTests_Battleships
{
    public class GameServiceTest
    {

        private IShip shipActive;
        private IShip shipSunk;

        [SetUp]
        public void Setup()
        {
            shipActive = new Ship(ShipClass.Destroyer);
            shipActive.Body = new Square[(int)ShipClass.Destroyer] { 
                new Square() { State = false}, 
                new Square() { State = true }, 
                new Square() { State = false } 
            };

            shipSunk = new Ship(ShipClass.Destroyer);
            shipSunk.Body = new Square[(int)ShipClass.Destroyer] {
                new Square() { State = false},
                new Square() { State = false },
                new Square() { State = false }
            };
        }



        [Test]
        public void PositionToIndex()
        {
            var serviceResult = GameServices.Service.PositionToIndex(2, 3, 10);

            Assert.That(serviceResult, Is.EqualTo(21));
        }


        [Test]
        public void IndexToPosition()
        {
            var serviceResult = GameServices.Service.IndexToPosition(21, 10);

            Assert.That(serviceResult.Item1, Is.EqualTo(2));
            Assert.That(serviceResult.Item2, Is.EqualTo(3));
        }


        [Test]
        public void PositionCharToInt()
        {
            var serviceResult = GameServices.Service.PositionCharToInt('c');

            Assert.That(serviceResult, Is.EqualTo(3));
        }


        [Test]
        [TestCase(2, 3, true, 5, 6, 3)]
        [TestCase(2, 3, false, 5, 2, 7)]
        public void DirectionToCoordinates(int x, int y, bool direction, int size, int resultX, int resultY)
        {
            var serviceResult = GameServices.Service.DirectionToCoordinates(x, y, direction, size);

            Assert.That(serviceResult.Item1, Is.EqualTo(resultX));
            Assert.That(serviceResult.Item2, Is.EqualTo(resultY));
        }


        [Test]
        public void PositionsToIndexes()
        {
            var serviceResult = GameServices.Service.PositionsToIndexes(2, 3, 6, 3, 10);

            Assert.That(serviceResult, Is.EquivalentTo(new int[] { 21, 22, 23, 24, 25 }));
        }


        [Test]
        public void ShipsExist_ShipsAreActive()
        {
            var listOfShips = new List<IShip>();
            listOfShips.Add(shipActive);

            var shipsExistResult = GameServices.Service.ShipsExist(listOfShips);

            Assert.That(shipsExistResult, Is.True);
        }


        [Test]
        public void ShipsExist_ShipsAreSunk()
        {
            var listOfShips = new List<IShip>();
            listOfShips.Add(shipSunk);

            var shipsExistResult = GameServices.Service.ShipsExist(listOfShips);

            Assert.That(shipsExistResult, Is.False);
        }


        [Test]
        public void DoesOverlapAnotherShip()
        {
            var board = new Board(10,10);
            board.Grid[12] = new Square() { State = true};

            var shipColisionResultOverlaps = GameServices.Service.DoesOverlapAnotherShip(new int[] {2, 12, 22, 32, 42 }, board);
            var shipColisionResultDoesNotOverlap = GameServices.Service.DoesOverlapAnotherShip(new int[] { 22, 32, 42 }, board);

            Assert.That(shipColisionResultOverlaps, Is.True);
            Assert.That(shipColisionResultDoesNotOverlap, Is.False);
        }
    }
}
