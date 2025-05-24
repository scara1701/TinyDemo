using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TinyDemo.SharedLib.Entities;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.MVVM
{
    public partial class MainViewModel : ObservableObject
    {
        ILottoService _lottoService;


        [ObservableProperty]
        string statusMessage;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NotBusy))]
        bool busy;

        public bool NotBusy => !Busy;

        [ObservableProperty]
        Lotto lotto;
        public MainViewModel(ILottoService lottoService)
        {

            _lottoService = lottoService;
            _ = GenerateLottoAsync();
        }


        private async Task GenerateLottoAsync()
        {
            try
            {
                Busy = true;
                StatusMessage = "Generating lotto numbers. Please wait....";
                Lotto = await _lottoService.GenerateLotto();
                StatusMessage = $"Lotto numbers generated on {Lotto.GeneratedOnTime.ToShortTimeString()}";

            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                //throw;
            }
            finally
            {
                Busy = false;
            }
        }


        [RelayCommand]
        private async void Generate()
        {
            await GenerateLottoAsync();
        }
    }
}
