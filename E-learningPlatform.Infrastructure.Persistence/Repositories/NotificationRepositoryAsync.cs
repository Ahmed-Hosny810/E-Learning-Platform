using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Infrastructure.Persistence.Repositories
{
    public class NotificationRepositoryAsync
     : GenericRepositoryAsync<Notification, int>, INotificationRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(
            string userId, int pageNumber, int pageSize)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)   
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Notification?> GetByIdAndUserIdAsync(
            int notificationId, string userId)
        {
            return await _context.Notifications
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);
        }

        public async Task<IEnumerable<Notification>> GetUnreadAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)    
                .OrderByDescending(n => n.CreatedAt)
                .Take(50)                                      
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetUnreadCountAsync(string userId)
        {
            return await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)    
                .CountAsync();
        }

        public async Task MarkAsReadAsync(int notificationId, string userId)
        {
            var rowsAffected = await _context.Notifications
                .Where(n => n.Id == notificationId && n.UserId == userId && !n.IsRead)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(n => n.IsRead, true)
                           .SetProperty(n => n.ReadAt, DateTime.UtcNow));

            if (rowsAffected == 0)
                throw new ApiException("Notification not found."); 
        }

        public async Task MarkAllAsReadAsync(string userId)
        {
            await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ExecuteUpdateAsync(setters =>
                    setters.SetProperty(n => n.IsRead, true)
                           .SetProperty(n => n.ReadAt, DateTime.UtcNow)); 
        }

    }
}
