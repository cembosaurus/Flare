using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Services;
using NUnit.Framework;
using System;

namespace NUnitTests_Battleships
{
    class FactoryTest
    {

        [Test]
        public void CreateShip()
        {
            var ship = Factory.CreateShip(ShipClass.Destroyer);

            Assert.That(ship, Is.TypeOf<Ship>());
            Assert.That(ship.ShipClass, Is.EqualTo(ShipClass.Destroyer.ToString()));
            Assert.That(Array.Exists(ship.Body, b => b.State == false), Is.False);
        }

    }
}
