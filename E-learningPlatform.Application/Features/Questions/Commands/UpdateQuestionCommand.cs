using E_learningPlatform.Application.Features.Questions.DTO;
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
    public class UpdateQuestionCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = null!;
        public int Points { get; set; }
        public string Explanation { get; set; } = null!;
        public List<UpdateOptionDto> Options { get; set; } = new();
    }

    public class UpdateQuestionCommandHandler : IRequestHandler<UpdateQuestionCommand, Response<int>>
    {
        private readonly IQuestionRepositoryAsync _questionRepository;

        public UpdateQuestionCommandHandler(IQuestionRepositoryAsync questionRepository)
        {
            _questionRepository = questionRepository;
        }

        public async Task<Response<int>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var question = await _questionRepository.GetQuestionWithDetailsAsync(request.Id);
            if (question == null) throw new Exception("Question not found");

            // 2. Update Question Header
            question.QuestionText = request.QuestionText;
            question.Points = request.Points;
            question.Explanation = request.Explanation;

            // a. Remove options that are no longer in the request
            var optionsToRemove = question.QuestionOptions
                .Where(oldOpt => !request.Options.Any(newOpt => newOpt.Id == oldOpt.Id))
                .ToList();

            foreach (var opt in optionsToRemove)
                question.QuestionOptions.Remove(opt);

            // b. Update existing or Add new
            foreach (var optDto in request.Options)
            {
                if (optDto.Id.HasValue && optDto.Id > 0)
                {
                    // Update existing
                    var existingOpt = question.QuestionOptions.FirstOrDefault(x => x.Id == optDto.Id);
                    if (existingOpt != null)
                    {
                        existingOpt.OptionText = optDto.OptionText;
                        existingOpt.IsCorrect = optDto.IsCorrect;
                        existingOpt.OrderIndex = optDto.OrderIndex;
                    }
                }
                else
                {
                    // Add new
                    question.QuestionOptions.Add(new QuestionOption
                    {
                        OptionText = optDto.OptionText,
                        IsCorrect = optDto.IsCorrect,
                        OrderIndex = optDto.OrderIndex,
                        QuestionId = question.Id
                    });
                }
            }

            await _questionRepository.UpdateAsync(question);

            return new Response<int>(question.Id);
        }
    }
}
