using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TinyDemo.SharedLib.Entities;
using TinyDemo.SharedLib.Services;

namespace TinyDemo.MVVM
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ILottoService _lottoService;
        private CancellationTokenSource? _cancellationTokenSource;

        [ObservableProperty]
        string statusMessage;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NotBusy))]
        bool busy;

        public bool NotBusy => !Busy;

        [ObservableProperty]
        Lotto? lotto;

        public MainViewModel(ILottoService lottoService)
        {
            _lottoService = lottoService ?? throw new ArgumentNullException(nameof(lottoService));
            StatusMessage = "Ready to generate lotto numbers";
            Busy = false;
            _ = GenerateLottoAsync();
        }

        private async Task GenerateLottoAsync()
        {
            await GenerateLottoAsync(CancellationToken.None).ConfigureAwait(false);
        }

        private async Task GenerateLottoAsync(CancellationToken cancellationToken)
        {
            // Cancel any existing operation safely
            try
            {
                _cancellationTokenSource?.Cancel();
                if (_cancellationTokenSource != null)
                {
                    try
                    {
                        await Task.Delay(100, cancellationToken).ConfigureAwait(false); // Give existing operation time to cancel
                    }
                    catch (OperationCanceledException)
                    {
                        // Expected if cancellation is requested
                    }
                    _cancellationTokenSource.Dispose();
                }
            }
            catch (ObjectDisposedException)
            {
                // Ignore if already disposed
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error cleaning up previous operation: " + ex.Message);
            }
            
            CancellationTokenSource linkedCts;
            try
            {
                linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            }
            catch (ObjectDisposedException)
            {
                // Fallback if cancellationToken is already disposed
                linkedCts = new CancellationTokenSource();
            }
            
            _cancellationTokenSource = linkedCts;

            try
            {
                Busy = true;
                StatusMessage = "Generating lotto numbers. Please wait....";

                var lotto = await _lottoService.GenerateLotto(linkedCts.Token).ConfigureAwait(false);
                Lotto = lotto;
                StatusMessage = $"Lotto numbers generated on {lotto.GeneratedOnTime:t}";
            }
            catch (OperationCanceledException) when (linkedCts.IsCancellationRequested)
            {
                StatusMessage = "Lotto generation was cancelled.";
            }
            catch (InvalidOperationException ex)
            {
                StatusMessage = "Failed to generate lotto numbers: " + ex.Message;
            }
            catch (Exception ex)
            {
                StatusMessage = "An unexpected error occurred: " + ex.Message;
            }
            finally
            {
                Busy = false;
                // Only dispose if this is not the current cancellation token source
                if (linkedCts == _cancellationTokenSource)
                {
                    try
                    {
                        linkedCts.Dispose();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error disposing cancellation token source: " + ex.Message);
                    }
                    _cancellationTokenSource = null;
                }
            }
        }

        [RelayCommand]
        private async Task Generate()
        {
            try
            {
                await GenerateLottoAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Handle any unexpected errors from the Generate command
                StatusMessage = "Error generating lotto: " + ex.Message;
            }
        }

        [RelayCommand]
        private void Cancel()
        {
            try
            {
                _cancellationTokenSource?.Cancel();
            }
            catch (ObjectDisposedException)
            {
                // Ignore if already disposed
            }
            catch (Exception ex)
            {
                StatusMessage = "Error cancelling operation: " + ex.Message;
            }
        }

        public void Dispose()
        {
            try
            {
                _cancellationTokenSource?.Cancel();
            }
            catch (ObjectDisposedException)
            {
                // Ignore if already disposed
            }
            catch (Exception ex)
            {
                // Log or handle disposal errors
                Console.WriteLine("Error during disposal: " + ex.Message);
            }
            
            try
            {
                _cancellationTokenSource?.Dispose();
            }
            catch (Exception ex)
            {
                // Log or handle disposal errors
                Console.WriteLine("Error disposing cancellation token source: " + ex.Message);
            }
            
            _cancellationTokenSource = null;
        }
    }
}
