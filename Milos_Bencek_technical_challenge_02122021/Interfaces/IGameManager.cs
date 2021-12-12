namespace Milos_Bencek_technical_challenge_02122021.Interfaces
{
    public interface IGameManager
    {
        bool GameInProgress { get; }
        bool Lost { get; }

        IServiceResult Hit(char xChar, int y);
        IServiceResult IsGameLost();
        IServiceResult Start();
    }
}