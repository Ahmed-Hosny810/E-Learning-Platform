using AutoMapper;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Modules.Commands.CreateCommand
{
    public class CreateSectionCommand:IRequest<Response<int>>
    {
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int DisplayOrder { get; set; }
        public int? DurationMinutes { get; set; }
    }
    public class CreateSectionCommandHandler : IRequestHandler<CreateSectionCommand, Response<int>>
    {
        private readonly ISectionRepositoryAsync _sectionRepository;
        private readonly IMapper _mapper;

        public CreateSectionCommandHandler(ISectionRepositoryAsync sectionRepository,IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(CreateSectionCommand request, CancellationToken cancellationToken)
        {
            var section=_mapper.Map<Section>(request);
            section.IsPublished = false;
            await _sectionRepository.AddAsync(section);
            return new Response<int>(section.Id);

        }
    }
}
