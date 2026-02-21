using BlinkQR.Application.UseCases;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlinkQR.Application.ViewModels
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<string> ScanHistory { get; } = [];

        private readonly ScanQrCodeUseCase _scanUseCase;
        private CancellationTokenSource? _cts;

        private const int MaxHistory = 10;

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
                    AddToHistory(result.Text);
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

        private void AddToHistory(string text)
        {
            if (ScanHistory.Contains(text))
                return;

            ScanHistory.Insert(0, text); // newest on top

            if (ScanHistory.Count > MaxHistory)
                ScanHistory.RemoveAt(ScanHistory.Count - 1);
        }
    }
}
