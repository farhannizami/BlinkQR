using BlinkQR.Application.UseCases;
using BlinkQR.Domain.Models;
using BlinkQR.Tests.Fakes;
using Xunit;

namespace BlinkQR.Tests
{
    public class ScanQrCodeUseCaseTests
    {
        [Fact]
        public void Execute_Returns_Result_When_Qr_Is_Detected()
        {
            var frame = new CameraFrame(new byte[10], 100, 100);
            var camera = new FakeCamera(frame);
            var scanner = new FakeQrScanner(new ScanResult("blinkqr"));

            var useCase = new ScanQrCodeUseCase(camera, scanner);

            // Act
            var result = useCase.Execute();

            // Assert
            Assert.NotNull(result);
            Assert.Equal("blinkqr", result!.Text);
        }

        [Fact]
        public void Execute_Returns_Null_When_No_Frame()
        {
            var camera = new FakeCamera(null);
            var scanner = new FakeQrScanner(null);

            var useCase = new ScanQrCodeUseCase(camera, scanner);

            var result = useCase.Execute();

            Assert.Null(result);
        }
    }
}
