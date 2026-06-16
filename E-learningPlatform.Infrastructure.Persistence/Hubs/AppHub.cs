using E_learningPlatform.Application.Features.Messages.Commands.MarkMessageRead;
using E_learningPlatform.Application.Features.Messages.Commands.SendMessage;
using E_learningPlatform.Application.Features.QuizAttempts.Commands.SubmitCommand;
using E_learningPlatform.Application.Interfaces.Hubs;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_learningPlatform.Infrastructure.Persistence.Hubs
{
    public class AppHub:Hub<IAppClient>
    {
        private readonly IMediator _mediator;

        public AppHub(IMediator mediator)
        {
            _mediator = mediator;
        }


        // ── Chat ─────────────────────────────────────────────────────────

        public async Task SendMessage(SendMessageCommand command)
            => await _mediator.Send(command);

        public async Task MarkMessageRead(MarkMessageReadCommand command)
            => await _mediator.Send(command);

        // ── Discussion ───────────────────────────────────────────────────

        //public async Task PostComment(CreateDiscussionCommentCommand command)
        //    => await _mediator.Send(command);

        //public async Task EditComment(EditDiscussionCommentCommand command)
        //    => await _mediator.Send(command);

        // ── Quiz ─────────────────────────────────────────────────────────

        public async Task SubmitQuiz(SubmitQuizCommand command)
            => await _mediator.Send(command);

    }
}
