using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMqTestNotifications.Core;
using ReactiveUI;

namespace RabbitMqTestNotifications.Server.ViewModels
{
    public class MessageViewModel : ReactiveObject, IRoutableViewModel
    {
        private MessageBase _message;
        private readonly ConnectionFactory _connectionFactory;

        public MessageViewModel(IScreen hostScreen, ConnectionFactory connectionFactory)
        {
            HostScreen = hostScreen;
            _connectionFactory = connectionFactory;
            SendMessageCommand = ReactiveCommand.CreateFromTask(SendMessage);
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

        public ReactiveCommand<Unit, Unit> SendMessageCommand { get; }
        public string UrlPathSegment => nameof(MessageViewModel);
        public IScreen HostScreen { get; }

        private async Task SendMessage()
        {
            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            channel.QueueDeclare(queue: "MessageBus",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            
            string message = JsonConvert.SerializeObject(Message);
            
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                routingKey: "MessageBus",
                basicProperties: null,
                body: body);
            
            connection.Close(0,"Сообщение отправлено");
        }
    }
}