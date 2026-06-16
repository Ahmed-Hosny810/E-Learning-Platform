using E_learningPlatform.Application.Features.Notifications.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Interfaces.Hubs
{
    public interface INotificationClient
    {
        Task ReceiveNotification(NotificationDto notification);
        Task NotificationRead(int notificationId);
    }
}
