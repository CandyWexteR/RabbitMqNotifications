using RabbitMqTestNotifications.Server.ViewModels;
using ReactiveUI;

namespace RabbitMqTestNotifications.Server.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : IViewFor<MainWindowViewModel>
    {

        public MainWindowView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            this.OneWayBind(ViewModel, model => model.HostScreen.Router, view => view.RoutedViewHost.Router);
            this.BindCommand(ViewModel, model => model.NavigateToSendMessageForm,
                view => view.Menu.SendMessageMenuItem);
            this.OneWayBind(ViewModel, model => model.Messages, view => view.Messages.ItemsSource);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MainWindowViewModel) value;
        }

        public MainWindowViewModel ViewModel { get; set; }
    }
}