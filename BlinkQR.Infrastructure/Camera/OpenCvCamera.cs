using BlinkQR.Domain.Abstractions;
using BlinkQR.Domain.Models;
using OpenCvSharp;

namespace BlinkQR.Infrastructure.Camera
{
    public class OpenCvCamera : ICamera, IDisposable
    {
        private readonly VideoCapture _capture;
        
        public OpenCvCamera(int cameraIndex = 0)
        {
            _capture = new VideoCapture(cameraIndex);
            if (!_capture.IsOpened())
            {
                throw new InvalidOperationException("Unable to open camera.");
            }
        }

        public CameraFrame? CaptureFrame()
        {
            using var mat = new Mat();
            _capture.Read(mat);

            if(mat.Empty())
            {
                return null;
            }

            return new CameraFrame(mat.ToBytes(), mat.Width, mat.Height);
        }

        public void Dispose()
        {
            _capture.Release();
            _capture.Dispose();
        }
    }
}
