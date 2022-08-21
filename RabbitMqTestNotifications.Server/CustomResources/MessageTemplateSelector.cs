using System.Windows;
using System.Windows.Controls;

namespace RabbitMqTestNotifications.Server.CustomResources
{
    public class MessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate MessageBaseTemplate { get; set; }
        
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is not Core.MessageBase message)
                return null;

            
            //TODO: Добавить новые типы сообщений
            return MessageBaseTemplate;
        }
    }
}