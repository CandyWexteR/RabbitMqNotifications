using System.Windows.Controls;
using RabbitMqTestNotifications.Server.ViewModels;
using ReactiveUI;

namespace RabbitMqTestNotifications.Server.Views
{
    public partial class MessageView : UserControl, IViewFor<MessageViewModel>
    {
        public MessageView(MessageViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;

            this.Bind(ViewModel, model => model.Message, view => view.MessageFrame.Content);
            this.BindCommand(ViewModel, model => model.SendMessageCommand, view => view.SendMessageButton);
        }

        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (MessageViewModel) value;
        }

        public MessageViewModel ViewModel { get; set; }
    }
}