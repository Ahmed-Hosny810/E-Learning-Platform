using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Interfaces.Services
{
    public interface INotificationService
    {
        Task NotifyNewLessonAddedAsync(string userId, string courseTitle, string lessonTitle,int lessonId);
        Task NotifyQuizSubmittedAsync(string studentId, int attemptId, string quizTitle, int score, int totalPoints, bool isPassed);
        Task NotifyQuizExpiredAsync(string studentId, int attemptId, string quizTitle);
        Task NotifyCoursePublishedAsync(string userId, int courseId, string courseTitle);
    }
}
