using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Quizzes.Commands.DeleteCommand
{
    public class DeleteQuizCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }

    public class DeleteQuizCommandHandler : IRequestHandler<DeleteQuizCommand, Response<int>>
    {
        private readonly IQuizRepositoryAsync _quizRepository;

        public DeleteQuizCommandHandler(IQuizRepositoryAsync quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<Response<int>> Handle(DeleteQuizCommand request, CancellationToken cancellationToken)
        {
            var quiz = await _quizRepository.GetByIdAsync(request.Id);

            if (quiz == null) throw new ApiException($"Quiz Not Found.");

            await _quizRepository.DeleteAsync(quiz);

            return new Response<int>(quiz.Id);
        }
    }
}
