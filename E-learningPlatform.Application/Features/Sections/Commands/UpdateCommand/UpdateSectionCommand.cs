using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Sections.Commands.UpdateCommand
{
    public class UpdateSectionCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public int? DurationMinutes { get; set; }
        public bool IsPublished { get; set; }
    }
    public class UpdateSectionCommandHandler : IRequestHandler<UpdateSectionCommand, Response<int>>
    {
        private readonly ISectionRepositoryAsync _sectionRepository;

        public UpdateSectionCommandHandler(ISectionRepositoryAsync sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }
        public async Task<Response<int>> Handle(UpdateSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await _sectionRepository.GetByIdAsync(request.Id);
            if (section == null) throw new ApiException("Section not found");
            section.Title = request.Title;
            section.Description = request.Description;
            section.DisplayOrder = request.DisplayOrder;
            section.DurationMinutes = request.DurationMinutes;
            section.IsPublished = request.IsPublished;
            await _sectionRepository.UpdateAsync(section);
            return new Response<int>(section.Id);
        }
    }
}
