using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Services;

namespace Milos_Bencek_technical_challenge_02122021
{
    class Program
    {
        static void Main(string[] args)
        {






            /*  Note:
             *  
            Use 'StateTracker' class to run application.

            For simplicity of this project I haven't included few 
            helpful libraries and frameworks.
            In the real world scenario I would use Dependency Injection 
            instead of manual instantiation or singleton patern.
            For validation purpose I would use Fluent Validation library 
            instead my simple custom validation class designed for
            valdating variables related to proces of placing the ship on board.

            Main() method contains 'TEST' region with few lines for quick testing
            instead of implementing unit test framework.

             */






        #region  TEST:

            // Creating test grid array for test:
            // Size: 100
            var arr = new Square[100];
            for (int i = 0; i < 100; i++)
                arr[i] = new Square();

            // Placing test ship on board, coordinates: G5, size: 3, direction: vertical:
            // Position on grid: {7,5},{7,6},{7,7}
            arr[46].State = true;
            arr[56].State = true;
            arr[66].State = true;

            // Creating test Board with test grid array:
            // Size: {10,10}
            var board = new Board(10, 10);
            board.Grid = arr;


            // TEST 1:
            // (with one ship already placed on board)
            // creating new ship, validating its position, size, direction and whether it overlaps another ship or board boundary:

            // Fail: Message = "Position coordinates should be larger than 0!"
            var r1 = Validator.Validate.Position(new ServiceParameters<Position>(new Position { startX = 1, startY = 1, endX = -1, endY = 1, Board = board }));

            // Fail: Message = "End position coordinates should be larger than start position coordinates!"
            var r2 = Validator.Validate.Position(new ServiceParameters<Position>(new Position { startX = 2, startY = 1, endX = 1, endY = 1, Board = board }));
            var r3 = Validator.Validate.Position(new ServiceParameters<Position>(new Position { startX = 1, startY = 2, endX = 1, endY = 1, Board = board }));

            // Fail: Message = "Direction of placed ship should be horizontal or vertical!"
            var r6 = Validator.Validate.Position(new ServiceParameters<Position>(new Position { startX = 1, startY = 1, endX = 3, endY = 3, Board = board }));

            // Fail: Message = "Size of ship must not exceed available space on board!"
            var r7 = Validator.Validate.Position(new ServiceParameters<Position>(new Position { startX = 5, startY = 5, endX = 11, endY = 5, Board = board }));
            var r8 = Validator.Validate.Position(new ServiceParameters<Position>(new Position { startX = 5, startY = 5, endX = 5, endY = 11, Board = board }));

            // Fail: Message = "Ship should not overlap another ship!"
            var r9 = Validator.Validate.Position(new ServiceParameters<Position>(new Position { startX = 5, startY = 5, endX = 7, endY = 5, Board = board }));

            // Success: Message = "Success!"
            var r10 = Validator.Validate.Position(new ServiceParameters<Position>(new Position { startX = 3, startY = 2, endX = 7, endY = 2, Board = board }));
            var r11 = Validator.Validate.Position(new ServiceParameters<Position>(new Position { startX = 1, startY = 1, endX = 1, endY = 1, Board = board }));


            // TEST 2:
            // testing the service which converts tart and end X,Y positions to array of board indexes allocated for given ship.
            // operation requires size of horizontal axis 'X' of board (width):

            // Success: Vertical ship of size 3: {46,56,66}
            var t1 = Service.Get.PositionsToIndexes(7, 5, 7, 7, 10);

            // Success: Horizontal ship of size 3: {11,12,13}
            var t2 = Service.Get.PositionsToIndexes(2, 2, 4, 2, 10);

            // Success: Ship of size 1: {43}
            var t3 = Service.Get.PositionsToIndexes(4, 5, 4, 5, 10);

            // Fail: Horizontal ship of size 14 (Overlaping board size): Array with 0 size
            var t4 = Service.Get.PositionsToIndexes(2, 2, 15, 2, 10);



            // TEST 3:
            // game logic:

            // Game setup:
            var stateTracker = new StateTracker(10,10);

            // Create ship results:
            // Success: Message = "Ship of type Destroyer was created!"
            var result1 = stateTracker.CreateShip(ShipClass.Destroyer);

            // Success: Message = "Ship of type Carrier was created!"
            var result2 = stateTracker.CreateShip(ShipClass.Carrier);

            // Success: Message = "Ship of type PatrolBoat was created!"
            var result3 = stateTracker.CreateShip(ShipClass.PatrolBoat);

            // Ship instances:
            var ship1 = result1.Data;
            var ship2 = result2.Data;
            var ship3 = result3.Data;

            // Game start:
            // Fail: Message = "Place at least one ship before you start game!"
            var status1 = stateTracker.StartGame();

            // Place ships on board:
            // Success: Message = "Success!"
            var res1 = stateTracker.AddShipToBoard(ship1, 'b', 2, true);

            // Fail: Message = "Ship should not overlap another ship!"
            var res2 = stateTracker.AddShipToBoard(ship2, 'c', 1, false);

            // Success: Message = "Success!"
            var res3 = stateTracker.AddShipToBoard(ship3, 'd', 5, false);

            // Take attack:
            // Success: Message = "Hit at: C2"
            var attack1 = stateTracker.Attack('c', 2);

            // Game start:
            // Success: Message = "Game has started. Game content can't be changed!"
            var status2 = stateTracker.StartGame();

            // Create ship result:
            // Fail: Message = "Can't create ship while game is in progress!"
            var result4 = stateTracker.CreateShip(ShipClass.PatrolBoat);

            // Game start:
            // Fail: Message = "Game is already in progress!"
            var status3 = stateTracker.StartGame();

            // Take attacks and read results:
            // Message = "Hit at: B2"
            var attack2 = stateTracker.Attack('b', 2);
            // Message = "Game is not lost yet!"
            var status4 = stateTracker.Lost();
            // Message = "Hit at: D2"
            var attack3 = stateTracker.Attack('d', 2);
            // Message = "Game is not lost yet!"
            var status5 = stateTracker.Lost();
            // Message = "Hit at: D5"
            var attack4 = stateTracker.Attack('d', 5);
            // Message = "Game is not lost yet!"
            var status6 = stateTracker.Lost();

            // Attack on last 'active' position:
            // Message = "Hit at: D6  - Last ship was sunk. Game is lost!"
            var attack5 = stateTracker.Attack('d', 6);
            // Message = "Game is lost!"
            var status7 = stateTracker.Lost();

#endregion



            

        }
    }
}
