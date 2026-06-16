using AutoMapper;
using E_learningPlatform.Application.Features.Messages.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Enums;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Application.Features.Messages.Commands.SendMessage
{
    public class SendMessageCommand : IRequest<Response<MessageDto>>
    {
        public string ReceiverId { get; set; } = null!;
        public int? CourseId { get; set; }
        public string MessageText { get; set; } = null!;
        public MessageType MessageType { get; set; }
    }
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, Response<MessageDto>>
    {
        private readonly IMessageRepositoryAsync _messageRepo;
        private readonly IUserService _userService;
        private readonly IRealtimeService _realtimeService;
        private readonly IMapper _mapper;

        public SendMessageHandler(
            IMessageRepositoryAsync messageRepo,
            IUserService userService,
            IRealtimeService realtimeService,
            IMapper mapper)
        {
            _messageRepo = messageRepo;
            _userService = userService;
            _realtimeService = realtimeService;
            _mapper = mapper;
        }

        public async Task<Response<MessageDto>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                SenderId = _userService.UserId,
                ReceiverId = request.ReceiverId,
                CourseId = request.CourseId,
                MessageText = request.MessageText,
                MessageType = request.MessageType,
                IsRead = false,
                SentAt = DateTime.UtcNow
            };

            await _messageRepo.AddAsync(message);

            var messageDto = _mapper.Map<MessageDto>(message);

            // Push to receiver instantly
            await _realtimeService.SendMessageAsync(request.ReceiverId, messageDto);

            return new Response<MessageDto>(messageDto);
        }


    }
}
