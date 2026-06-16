using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Interfaces.Hubs
{
    public interface IAppClient: IChatClient, INotificationClient, IQuizClient, IDiscussionClient 
    { }

}
