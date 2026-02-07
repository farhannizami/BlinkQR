using BlinkQR.Domain.Abstractions;
using BlinkQR.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

        public ScanResult? Execute()
        {
            var frame = _camera.CaptureFrame();
            if(frame == null)
            {
                return null;
            }
            return _scanner.Scan(frame);
        }
    }
}
