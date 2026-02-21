using BlinkQR.Domain.Abstractions;
using BlinkQR.Domain.Models;

namespace BlinkQR.Application.UseCases
{
    public class ScanQrCodeUseCase
    {
        private readonly ICamera _camera;
        private readonly IQrScanner _scanner;

        public ScanQrCodeUseCase(ICamera camera, IQrScanner scanner)
        {
            _camera = camera;
            _scanner = scanner;
        }

        public (CameraFrame? Frame, ScanResult? Result) ExecuteWithFrame()
        {
            var frame = _camera.CaptureFrame();
            if (frame == null)
            {
                return (null, null);
            }

            return (frame, _scanner.Scan(frame));
        }

        public ScanResult? Execute()
        {
            var (_, result) = ExecuteWithFrame();
            return result;
        }
    }
}
