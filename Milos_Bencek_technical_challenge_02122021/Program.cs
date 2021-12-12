using Milos_Bencek_technical_challenge_02122021.Interfaces;

namespace Milos_Bencek_technical_challenge_02122021
{
    class Program
    {
        static void Main(string[] args)
        {

            int boardHorizontalSize = 10;
            int boardVerticalSize = 10;
            IStateTracker stateTracker = new StateTracker
                (
                    boardHorizontalSize, 
                    boardVerticalSize
                );


        }
    }
}
