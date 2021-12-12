namespace Milos_Bencek_technical_challenge_02122021.Interfaces
{
    public interface IServiceResult<T>
    {
        T Data { get; }
        string Message { get; }
        bool Status { get; }
    }

    public interface IServiceResult
    {
        string Message { get; }
        bool Status { get; }
    }
}