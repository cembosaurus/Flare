using Milos_Bencek_technical_challenge_02122021.Interfaces;

namespace Milos_Bencek_technical_challenge_02122021.Services
{
    public class ServiceParameters<T> : IServiceParameters<T>
    {

        private T _parameter;

        public ServiceParameters(T parameter)
        {
            _parameter = parameter;
        }


        public T Data
        {
            get { return _parameter; }
            set { _parameter = value; }
        }

    }
}
