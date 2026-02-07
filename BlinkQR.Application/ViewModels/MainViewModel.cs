using BlinkQR.Application.UseCases;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlinkQR.Application.ViewModels
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        private readonly ScanQrCodeUseCase _scanUseCase;
        private CancellationTokenSource? _cts;

        private string? _scanResult;
        public string? ScanResult
        {
            get => _scanResult;
            private set
            {
                _scanResult = value;
                OnPropertyChanged();
            }
        }

        private bool _isScanning;
        public bool IsScanning
        {
            get => _isScanning;
            private set
            {
                _isScanning = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel(ScanQrCodeUseCase scanUseCase)
        {
            _scanUseCase = scanUseCase;
        }

        public async Task StartScanningAsync()
        {
            if (IsScanning)
                return;

            IsScanning = true;
            _cts = new CancellationTokenSource();

            var result = await Task.Run(() =>
            {
                while (!_cts.IsCancellationRequested)
                {
                    var r = _scanUseCase.Execute();
                    if (r != null)
                        return r;
                }

                return null;
            });

            // ✅ Back on UI thread here
            if (result != null)
            {
                ScanResult = result.Text;
                StopScanning();
            }
        }


        public void StopScanning()
        {
            if (!IsScanning)
            {
                return;
            }
            _cts?.Cancel();
            IsScanning = false;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
