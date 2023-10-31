using System.Windows;

namespace Concesionario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new viewmodel.MainViewModel();
        }
    }
}
