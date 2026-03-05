using AutoMapper;
using E_learningPlatform.Application.Features.Enrollments.DTO;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Enrollments.Queries.GetAllQuery
{
    public class GetAllEnrollmentsQuery:IRequest<PagedResponse<IEnumerable<EnrollmentVm>>>
    {
        public GetAllEnrollmentsParameter Parameter { get; set; }
    }
    public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, PagedResponse<IEnumerable<EnrollmentVm>>>
    {
        private readonly IEnrollmentRepositoryAsync _enrollmentRepository;
        private readonly IMapper _mapper;
        public GetAllEnrollmentsQueryHandler(IEnrollmentRepositoryAsync enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }
        public async Task<PagedResponse<IEnumerable<EnrollmentVm>>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            var pagedEnrollments = await _enrollmentRepository.GetEnrollmentsPagedResponseAsync(request.Parameter.Filter, request.Parameter.Includes,
                        request.Parameter.OrderKey,request.Parameter.OrderDescending, request.Parameter.PageNumber, request.Parameter.PageSize);
            var enrollmentVms = _mapper.Map<IEnumerable<EnrollmentVm>>(pagedEnrollments.Data);
             return new PagedResponse<IEnumerable<EnrollmentVm>>(enrollmentVms, request.Parameter.PageNumber, request.Parameter.PageSize, pagedEnrollments.TotalCount);
        }
    }

}
