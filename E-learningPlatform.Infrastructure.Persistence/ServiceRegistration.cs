using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using E_learningPlatform.Infrastructure.Persistence.Repositories;
using E_learningPlatform.Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Register DbContext with SQL Server provider
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Cs")));

            // Generic Repository
            services.AddScoped(typeof(IGenericRepositoryAsync<,>), typeof(GenericRepositoryAsync<,>));

            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();

            // Specific Repositories
            services.AddScoped<ICategoryRepositoryAsync, CategoryRepositoryAsync>();

            services.AddScoped<ICourseRepositoryAsync, CourseRepositoryAsync>();
            services.AddScoped<ICourseCategoryRepositoryAsync, CourseCategoryRepositoryAsync>();
            services.AddScoped<IUserProfileRepositoryAsync, UserProfileRepositoryAsync>();
            services.AddScoped<ISectionRepositoryAsync, SectionRepositoryAsync>();
            services.AddScoped<ILessonRepositoryAsync, LessonRepositoryAsync>();
            services.AddScoped<ILessonContentRepositoryAsync, LessonContentRepositoryAsync>();
            services.AddScoped<IEnrollmentRepositoryAsync, EnrollmentRepositoryAsync>();
            services.AddScoped<IPaymentRepositoryAsync, PaymentRepositoryAsync>();
            services.AddTransient<IFileStorageService,LocalFileStorageService>();

            services.AddHttpClient<IPaymentService, PaymentService>(client =>
            {
                // This matches the Base URL of your Payment Minimal API
                client.BaseAddress = new Uri(configuration["PaymentSettings:BaseUrl"]);

                client.DefaultRequestHeaders.Add("X-Payment-Secret", configuration["PaymentSettings:WebhookSecret"]);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            }).SetHandlerLifetime(TimeSpan.FromMinutes(5));

            services.AddScoped<IQuizRepositoryAsync, QuizRepositoryAsync>();
            services.AddScoped<IQuizAttemptRepositoryAsync, QuizAttemptRepositoryAsync>();
            services.AddScoped<IQuestionRepositoryAsync, QuestionRepositoryAsync>();

            //services.AddScoped<IQuestionOptionRepositoryAsync, QuestionOptionRepositoryAsync>();



            return services;
        }
    }
}
