using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Features.Enrollments.DTO;
using E_learningPlatform.Application.Features.Enrollments.Queries.GetAllQuery;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Enrollments.Queries.GetByIdQuery
{
    public class GetEnrollmentByIdQuery:IRequest<Response<EnrollmentDetailedVm>>
    {
        public int Id { get; set; }
        public EnrollmentIncludes Includes { get; set; }
    }
    public class GetEnrollmentByIdQueryHandler:IRequestHandler<GetEnrollmentByIdQuery, Response<EnrollmentDetailedVm>>
    {
        private readonly IEnrollmentRepositoryAsync _enrollmentRepository;
        private readonly IMapper _mapper;

        public GetEnrollmentByIdQueryHandler(IEnrollmentRepositoryAsync enrollmentRepository,IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }
        public async Task<Response<EnrollmentDetailedVm>> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(request.Id, request.Includes);
            if (enrollment == null) throw new ApiException("Enrollment not found");
            
             var enrollmentVm = _mapper.Map<EnrollmentDetailedVm>(enrollment);

            return new Response<EnrollmentDetailedVm>(enrollmentVm);
        }
    }
}
