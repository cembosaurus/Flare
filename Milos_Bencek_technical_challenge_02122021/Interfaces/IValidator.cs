using Milos_Bencek_technical_challenge_02122021.Entities;
using Milos_Bencek_technical_challenge_02122021.Services;

namespace Milos_Bencek_technical_challenge_02122021.Interfaces
{
    public interface IValidator
    {
        ServiceResult Position(IServiceParameters<Position> model);
    }
}