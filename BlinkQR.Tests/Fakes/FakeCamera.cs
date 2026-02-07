using BlinkQR.Domain.Abstractions;
using BlinkQR.Domain.Models;

namespace BlinkQR.Tests.Fakes
{
    public class FakeCamera : ICamera
    {
        private readonly CameraFrame? _frame;
        public FakeCamera(CameraFrame? frame)
        {
            _frame = frame;
        }

        public CameraFrame? CaptureFrame()
        {
            return _frame;
        }
    }
}
