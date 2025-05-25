using System.Collections.ObjectModel;
using TinyDemo.SharedLib.Entities;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.WebAPI.Services
{
    public class LottoService : ILottoService
    {
        public async Task<Lotto> GenerateLotto()
        {
            //Gwen -  Create a list of numbers from 1 to 45
            List<int> numbers = Enumerable.Range(1, 45).ToList();

            //Gwen - Shuffle the list and pick 7 unique numbers
            Random random = new Random();
            List<int> selectedNumbers = numbers.OrderBy(x => random.Next()).Take(7).ToList();

            //Gwen - Delay to simulate long running process
            await Task.Delay(250);

            Lotto lotto = new Lotto
            {
                GeneratedOnTime = DateTime.Now,
                Numbers = new ObservableCollection<int>(selectedNumbers),
            };

            return lotto;
        }
    }
}
