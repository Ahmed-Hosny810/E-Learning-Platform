using AutoMapper;
using E_learningPlatform.Application.Features.Modules.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Sections.Queries.GetAllQuery
{
    public class GetAllSectionsQuery:IRequest<PagedResponse<IEnumerable<SectionVm>>>
    {
        public GetAllSectionsQueryParameter Parameter { get; set; } = new();
    }
    public class GetAllSectionsQueryHandler : IRequestHandler<GetAllSectionsQuery, PagedResponse<IEnumerable<SectionVm>>>
    {
        private readonly ISectionRepositoryAsync _sectionRepository;
        private readonly IMapper _mapper;

        public GetAllSectionsQueryHandler(ISectionRepositoryAsync sectionRepository,IMapper mapper)
        {
            _sectionRepository = sectionRepository;
            _mapper = mapper; 
        }
        public async Task<PagedResponse<IEnumerable<SectionVm>>> Handle(GetAllSectionsQuery request, CancellationToken cancellationToken)
        {
            var pagedSections = await _sectionRepository.GetSectionsPagedResponseAsync(request.Parameter.Filter, request.Parameter.Includes,
                request.Parameter.OrderKey, request.Parameter.OrderDescending, request.Parameter.PageNumber, request.Parameter.PageSize);
            var sectionVms = _mapper.Map<IEnumerable<SectionVm>>(pagedSections.Data);
            return new PagedResponse<IEnumerable<SectionVm>>(sectionVms, pagedSections.PageNumber, pagedSections.PageSize, pagedSections.TotalCount);


        }
    }
}
