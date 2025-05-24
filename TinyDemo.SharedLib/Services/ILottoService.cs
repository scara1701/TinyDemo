using TinyDemo.SharedLib.Entities;

namespace TinyDemo.SharedLib.Services
{
    public interface ILottoService
    {
        Task<Lotto> GenerateLotto();
    }
}