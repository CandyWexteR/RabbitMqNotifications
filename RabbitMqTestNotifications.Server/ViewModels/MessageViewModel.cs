using System.Reactive;
using RabbitMqTestNotifications.Core;
using ReactiveUI;

namespace RabbitMqTestNotifications.Server.ViewModels
{
    public class MessageViewModel : ReactiveObject, IRoutableViewModel
    {
        private MessageBase _message;

        public MessageViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;
            Message = new MessageBase();
        }

        public MessageBase Message
        {
            get => _message;
            set
            {
                this.RaiseAndSetIfChanged(ref _message, value);
            }
        }

        public ReactiveCommand<Unit, Unit> SendMessageCommand { get; set; }
        public string UrlPathSegment => nameof(MessageViewModel);
        public IScreen HostScreen { get; }
    }
}