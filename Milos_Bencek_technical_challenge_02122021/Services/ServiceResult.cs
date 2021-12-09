using Milos_Bencek_technical_challenge_02122021.Interfaces;

namespace Milos_Bencek_technical_challenge_02122021.Services
{
    public class ServiceResult<T> : ServiceResult, IServiceResult<T>
    {

        private T _data;

        public ServiceResult(T data)
        {
            _data = data;
        }

        public ServiceResult(T data = default(T), bool status = false, string message = "") 
            : base(status, message)
        {
            _data = data;
        }


        public T Data => _data;

        public new bool Status => _status;

        public new string Message => _message;

    }


    public class ServiceResult : IServiceResult
    {

        private protected bool _status;
        private protected string _message = string.Empty;


        public ServiceResult(bool status = false, string message = "")
        {
            _status = status;
            _message = message;
        }



        public bool Status => _status;

        public string Message => _message;


    }
}
