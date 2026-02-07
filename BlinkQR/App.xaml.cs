using BlinkQR.Application.UseCases;
using BlinkQR.Application.ViewModels;
using BlinkQR.Domain.Abstractions;
using BlinkQR.Infrastructure.Camera;
using BlinkQR.Infrastructure.QR;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BlinkQR
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static IServiceProvider Services { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            
            services.AddSingleton<ICamera, OpenCvCamera>();
            services.AddSingleton<IQrScanner, ZxingQrScanner>();
            services.AddSingleton<ScanQrCodeUseCase>();
            services.AddSingleton<MainViewModel>();

            Services = services.BuildServiceProvider();
            base.OnStartup(e);
        }
    }

}
