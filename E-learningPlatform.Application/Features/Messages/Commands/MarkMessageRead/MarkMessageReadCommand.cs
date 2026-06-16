using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Features.Messages.Commands.MarkMessageRead
{
    public class MarkMessageReadCommand : IRequest<Response<bool>>
    {
        public int MessageId { get; set; }
    }

    public class MarkMessageReadCommandHandler : IRequestHandler<MarkMessageReadCommand, Response<bool>>
    {
        private readonly IMessageRepositoryAsync _repository;
        private readonly IRealtimeService _realtimeService;
        public MarkMessageReadCommandHandler(IMessageRepositoryAsync repository,IRealtimeService realtimeService)
        {
            _repository = repository;
            _realtimeService = realtimeService;
        }
        public async Task<Response<bool>> Handle(MarkMessageReadCommand request, CancellationToken cancellationToken)
        {
            var message = await _repository.GetByIdAsync(request.MessageId);
            if (message == null)
            {
                throw new ApiException($"Message not found.");
            }

            message.IsRead = true;
            await _repository.UpdateAsync(message);

            await _realtimeService.SendMessageReadAsync(message.SenderId,message.Id);

            return new Response<bool>(true);

        }
    }
}