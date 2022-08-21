using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqTestNotifications.Core;
using ReactiveUI;

namespace RabbitMqTestNotifications.Server.ViewModels
{
    public class MainWindowViewModel : ReactiveObject, IRoutableViewModel
    {
        private readonly ObservableCollection<MessageBase> _messages;
        private readonly CompositeDisposable _disposables;
        private readonly ConnectionFactory _connectionFactory;
        public MainWindowViewModel(IScreen hostScreen, MessageViewModel messageViewModel, CompositeDisposable disposables, ConnectionFactory connectionFactory)
        {
            _messages = new ObservableCollection<MessageBase>();
            HostScreen = hostScreen;
            MessageViewModel = messageViewModel;
            _disposables = disposables;
            _connectionFactory = connectionFactory;
            NavigateToSendMessageForm = ReactiveCommand.CreateFromTask(async _ =>
            {
                HostScreen.Router.Navigate.Execute(MessageViewModel);
            });
            
            var connection = _connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queue: "MessageBus",
                autoAck: true,
                consumer: consumer);
            
            Observable.FromEventPattern<BasicDeliverEventArgs>(consumer, nameof(consumer.Received))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(HandleRecievedMessage)
                .DisposeWith(_disposables);
        }

        public string UrlPathSegment => nameof(MainWindowViewModel);
        public IScreen HostScreen { get; }
        public ReactiveCommand<Unit, Unit> NavigateToSendMessageForm { get; }

        public IReadOnlyCollection<MessageBase> Messages => _messages;
        public MessageViewModel MessageViewModel { get; }

        private void HandleRecievedMessage(EventPattern<BasicDeliverEventArgs> args)
        {
            var message = Encoding.UTF8.GetString(args.EventArgs.Body.ToArray());
            ;
        }
    }
}