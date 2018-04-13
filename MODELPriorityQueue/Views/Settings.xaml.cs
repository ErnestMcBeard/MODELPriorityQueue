using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MODELPriorityQueue.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Settings : Page
    {
        public Settings()
        {
            this.InitializeComponent();
        }

        private async void AddManagerButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.AddManager();
        }

        private async void AddTechnicianButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.AddTechnician();
        }

        private async void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.AddCustomer();
        }
    }
}
