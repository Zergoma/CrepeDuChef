using Avalonia.Controls;
using CrepeDuChef.Avalonia.ViewModels;

namespace CrepeDuChef.Avalonia.Views
{
    public partial class MainWindow : Window
    {
        // Avalonia ctor required to eleminate "Avalonia warning AVLN3001"
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();

            DataContext = vm;
        }
    }
}