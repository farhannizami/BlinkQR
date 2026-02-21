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

        private byte[]? _cameraPreviewData;
        public byte[]? CameraPreviewData
        {
            get => _cameraPreviewData;
            private set
            {
                _cameraPreviewData = value;
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
            {
                return;
            }

            IsScanning = true;
            _cts = new CancellationTokenSource();

            while (!_cts.IsCancellationRequested)
            {
                var (frame, result) = _scanUseCase.ExecuteWithFrame();

                if (frame != null)
                {
                    CameraPreviewData = frame.Data;
                }

                if (result != null)
                {
                    ScanResult = result.Text;
                    StopScanning();
                    return;
                }

                try
                {
                    await Task.Delay(33, _cts.Token);
                }
                catch (TaskCanceledException)
                {
                    return;
                }
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
