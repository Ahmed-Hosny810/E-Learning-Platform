using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Sections.Commands.DeleteCommand
{
    public class DeleteSectionCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, Response<int>>
    {
        private readonly ISectionRepositoryAsync _sectionRepository;

        public DeleteSectionCommandHandler(ISectionRepositoryAsync sectionRepository)
        {
            _sectionRepository = sectionRepository;
        }
        public async Task<Response<int>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
        {
            var section = await  _sectionRepository.GetByIdAsync(request.Id);
            if (section == null) throw new ApiException("Section not found");
            await _sectionRepository.DeleteAsync(section);
            return new Response<int>(section.Id);
        }
    }
}
