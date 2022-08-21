using System.Reactive;
using Microsoft.Extensions.Hosting;
using ReactiveUI;

namespace RabbitMqTestNotifications.Server.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IRoutableViewModel
    {
        public MainWindowViewModel(IScreen hostScreen, MessageViewModel messageViewModel)
        {
            HostScreen = hostScreen;
            MessageViewModel = messageViewModel;
            NavigateToSendMessageForm = ReactiveCommand.CreateFromTask(async _ =>
            {
                HostScreen.Router.Navigate.Execute(MessageViewModel);
            });
        }

        public string UrlPathSegment => nameof(MainWindowViewModel);
        public IScreen HostScreen { get; }
        public ReactiveCommand<Unit, Unit> NavigateToSendMessageForm { get; }
        public MessageViewModel MessageViewModel { get; }
    }
}