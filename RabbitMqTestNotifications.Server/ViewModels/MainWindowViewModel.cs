using ReactiveUI;

namespace RabbitMqTestNotifications.Server.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IRoutableViewModel
    {
        public MainWindowViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
        }

        public string UrlPathSegment => nameof(MainWindowViewModel);
        public IScreen HostScreen { get; }
    }
}