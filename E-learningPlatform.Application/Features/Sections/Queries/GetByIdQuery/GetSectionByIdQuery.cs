using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.Sections.DTO;
using E_learningPlatform.Application.Features.Sections.Queries.GetAllQuery;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Modules.Queries.GetByIdQuery
{
    public class GetSectionByIdQuery: IRequest<Response<SectionDetailedVm>>
    {
        public int Id { get; set; }
        public SectionIncludes Includes { get; set; }
    }
    public class GetSectionByIdQueryHandler : IRequestHandler<GetSectionByIdQuery, Response<SectionDetailedVm>>
    {
        private readonly ISectionRepositoryAsync _sectionRepository;
        private readonly IMapper _mapper;

        public GetSectionByIdQueryHandler(ISectionRepositoryAsync sectionRepository,IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper;
        }
        public async Task<Response<SectionDetailedVm>> Handle(GetSectionByIdQuery request, CancellationToken cancellationToken)
        {
            var section=await _sectionRepository.GetSectionByIdAsync(request.Id,request.Includes);
            if(section==null) throw new ApiException("Section not found");
            var sectionVm=_mapper.Map<SectionDetailedVm>(section);
            return new Response<SectionDetailedVm>(sectionVm);
        }
    }
}
