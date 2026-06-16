using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Infrastructure.Persistence.Repositories
{
    public class MessageRepositoryAsync : GenericRepositoryAsync<Message, int>, IMessageRepositoryAsync
    {
        public MessageRepositoryAsync(ApplicationDbContext context) : base(context)
        {
        }
    }
}
