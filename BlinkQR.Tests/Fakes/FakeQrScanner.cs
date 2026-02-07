using BlinkQR.Domain.Abstractions;
using BlinkQR.Domain.Models;

namespace BlinkQR.Tests.Fakes
{
    public class FakeQrScanner : IQrScanner
    {
        private readonly ScanResult? _result;
        public FakeQrScanner(ScanResult? result)
        {
            _result = result;
        }
        public ScanResult? Scan(CameraFrame frame)
        {
            return _result;
        }
    }
}
