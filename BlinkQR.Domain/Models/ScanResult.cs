namespace BlinkQR.Domain.Models
{
    public class ScanResult
    {
        public string Text { get; }

        public ScanResult(string text)
        {
            Text = text;
        }
    }
}
