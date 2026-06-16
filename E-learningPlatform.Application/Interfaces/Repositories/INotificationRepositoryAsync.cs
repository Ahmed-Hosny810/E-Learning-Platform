using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface INotificationRepositoryAsync: IGenericRepositoryAsync<Notification,int>
    {
        Task<IEnumerable<Notification>> GetByUserIdAsync(
        string userId, int pageNumber, int pageSize);

        Task<IEnumerable<Notification>> GetUnreadAsync(string userId);

        Task<int> GetUnreadCountAsync(string userId);

        Task MarkAsReadAsync(int notificationId, string userId);

        Task MarkAllAsReadAsync(string userId);

        Task<Notification?> GetByIdAndUserIdAsync(int notificationId, string userId);
    }
}
