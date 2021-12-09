using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Interfaces;

namespace Milos_Bencek_technical_challenge_02122021.Services
{
    public class Validator : IValidator
    {

        private static readonly Validator _validations = new Validator();

        static Validator() { }
        private Validator() { }

        public static Validator Validate => _validations;


        public ServiceResult Position(IServiceParameters<Position> model)
        {
            // Copying values to local variables for better readability of validation code:
            int startX = model.Data.startX;
            int startY = model.Data.startY;
            int endX = model.Data.endX;
            int endY = model.Data.endY;
            Square[] arr = model.Data.Board.Grid;
            int sizeX = model.Data.Board.SizeX;



            // Positive value of position integers:
            if (startX < 0 || startY < 0 || endX < 0 || endY < 0)
                return new ServiceResult(false, "Position coordinates should be larger than 0!");

            // Start position of ship must be lower than end position:
            if (startX > endX || startY > endY)
                return new ServiceResult(false, "End position coordinates should be larger than start position coordinates!");

            // Array size of board must be bigger than 'sizeX' (size of horizontal axis):
            if (arr.Length < sizeX)
                return new ServiceResult(false, "Array size must be larger than horizontal line!");

            // Board must be rectangle:
            if (arr.Length % sizeX != 0)
                return new ServiceResult(false, "Array size must be integral multiple of size of horizontal line!");

            // Direction of the ship must be vertical or horizontal, not diagonal:
            if (startX < endX && startY < endY)
                return new ServiceResult(false, "Direction of placed ship should be horizontal or vertical!");

            // Size of ship is not limited (independent from game rules) but must not overlap board border:
            if (endX > sizeX || endY > arr.Length / sizeX)
                return new ServiceResult(false, "Size of ship must not exceed available space on board!");

            // 'indexes' - board indexes allocated for new ship:
            var indexes = Service.Get.PositionsToIndexes(startX, startY, endX, endY, sizeX);

            // Ship should not be in colision with another ship.
            // Check whether indexes allocated for ship are available:
            foreach (int i in indexes)
            {
                // Identify whether board index allocated for ship is not occupied by another ship:
                if (arr[i].State)
                    return new ServiceResult(false, "Ship should not overlap another ship!");
            }



            // Validation successful, allocated space for ship is available:
            return new ServiceResult(true, "Success!");
        }
    }
}
