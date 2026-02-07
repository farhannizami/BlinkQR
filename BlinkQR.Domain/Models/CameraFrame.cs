namespace BlinkQR.Domain.Models
{
    public class CameraFrame
    {
        public byte[] Data { get; }
        public int Width { get; }
        public int Height { get; }

        public CameraFrame(byte[] data, int width, int height)
        {
            Data = data;
            Width = width;
            Height = height;
        }
    }
}
