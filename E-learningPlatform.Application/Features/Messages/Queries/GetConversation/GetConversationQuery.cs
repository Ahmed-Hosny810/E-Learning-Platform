using E_learningPlatform.Application.Features.Messages.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Features.Messages.Queries.GetConversation
{
    public class GetConversationQuery: IRequest<PagedResponse<IEnumerable<MessageDto>>>
    {
        public string OtherUserId { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 30;
    }

    public class GetConversationQueryHandler : IRequestHandler<GetConversationQuery, PagedResponse<IEnumerable<MessageDto>>>
    {
        private readonly IMessageRepositoryAsync _messageRepository;
        private readonly IUserService _userService;

        public GetConversationQueryHandler(IMessageRepositoryAsync messageRepository,IUserService userService)
        {
            _messageRepository = messageRepository;
            _userService = userService;
        }
        public Task<PagedResponse<IEnumerable<MessageDto>>> Handle(GetConversationQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
            
        }
    }
}
