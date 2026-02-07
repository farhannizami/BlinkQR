using BlinkQR.Domain.Abstractions;
using BlinkQR.Domain.Models;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using ZXing;
using ZXing.Common;
using ZXing.Windows.Compatibility;

namespace BlinkQR.Infrastructure.QR
{
    public sealed class ZxingQrScanner : IQrScanner
    {
        private readonly BarcodeReader _reader;
        public ZxingQrScanner()
        {
            _reader = new BarcodeReader
            {
                AutoRotate = true,
                TryInverted = true,
                Options = new DecodingOptions
                {
                    PossibleFormats = new[]
                    {
                        BarcodeFormat.QR_CODE
                    }
                }
            };
        }

        public ScanResult? Scan(CameraFrame frame)
        {
            using var mat = Mat.FromImageData(frame.Data);
            using var bitmap = BitmapConverter.ToBitmap(mat);

            var result = _reader.Decode(bitmap);
            return result == null? null : new ScanResult(result.Text);
        }
    }
}
