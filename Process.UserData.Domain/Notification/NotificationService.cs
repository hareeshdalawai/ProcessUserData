using AutoMapper;
using Process.UserData.FunctionApp.Domain.Interfaces;
using Process.UserData.FunctionApp.Domain.Models;

namespace Process.UserData.FunctionApp.Domain.Notification
{
    public class NotificationService : INotificationService
    {
        private IDataRepository _dataRepository;
        private IMessagingRepository _messagingRepository;
        private IMapper _mapper;

        public NotificationService(IDataRepository dataRepository, IMessagingRepository messagingRepository, IMapper mapper)
        {
            _dataRepository = dataRepository;
            _messagingRepository = messagingRepository;
            _mapper = mapper;
        }

        public void SendNotificationMessage(DateTime lastExecutionTime)
        {
            var users = _dataRepository.GetUsers(lastExecutionTime);

            var notificationMessages = _mapper.Map<List<NotificationMessage>>(users);

            foreach (var notification in notificationMessages)
            {
                notification.CorrelationId = Guid.NewGuid().ToString();
                _messagingRepository.SendToNotificationQueue(notification);
            }            
        }
    }
}
