namespace RabbitMqTestNotifications.Core
{
    public class MessageBase
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public virtual string Discriminator => nameof(MessageBase);
    }
}