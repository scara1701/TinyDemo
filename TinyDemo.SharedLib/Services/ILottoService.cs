using System.Threading;
using System.Threading.Tasks;
using TinyDemo.SharedLib.Entities;

namespace TinyDemo.SharedLib.Services
{
    public interface ILottoService
    {
        Task<Lotto> GenerateLotto(CancellationToken cancellationToken = default);
    }
}