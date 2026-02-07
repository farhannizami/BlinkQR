using BlinkQR.Domain.Models;

namespace BlinkQR.Domain.Abstractions
{
    public interface ICamera
    {
        CameraFrame? CaptureFrame();
    }
}
