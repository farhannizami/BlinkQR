using BlinkQR.Application.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace BlinkQR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = App.Services.GetRequiredService<MainViewModel>();
        }

        private async void StartScan_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MainViewModel)DataContext;
            await viewModel.StartScanningAsync();
        }
    }
}