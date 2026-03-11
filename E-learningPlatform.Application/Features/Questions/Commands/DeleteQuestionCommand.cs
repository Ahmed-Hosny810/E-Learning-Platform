using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Questions.Commands
{
    public class DeleteQuestionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteQuestionCommandHandler : IRequestHandler<DeleteQuestionCommand, Response<int>>
    {
        private readonly IQuestionRepositoryAsync _questionRepository;

        public DeleteQuestionCommandHandler(IQuestionRepositoryAsync questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Response<int>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetByIdAsync(request.Id);
            if (question == null) throw new Exception("Question not found");

            await _questionRepository.DeleteAsync(question);
            return new Response<int>(request.Id);
        }
    }
}
