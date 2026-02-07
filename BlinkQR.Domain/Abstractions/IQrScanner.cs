using BlinkQR.Domain.Models;

namespace BlinkQR.Domain.Abstractions
{
    public interface IQrScanner
    {
        ScanResult? Scan(CameraFrame frame);
    }
}
