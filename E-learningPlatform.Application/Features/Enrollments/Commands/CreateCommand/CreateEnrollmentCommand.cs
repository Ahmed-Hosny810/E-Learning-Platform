using AutoMapper;
using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Enrollments.Commands.CreateCommand
{
    public class CreateEnrollmentCommand:IRequest<Response<string>>
    {
        public int CourseId { get; set; }

    }
    public class CreateEnrollmentCommandHandler : IRequestHandler<CreateEnrollmentCommand, Response<string>>
    {
        private readonly IEnrollmentRepositoryAsync _enrollmentRepository;
        private readonly ICourseRepositoryAsync _courseRepository; // Add this
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public CreateEnrollmentCommandHandler(
            IEnrollmentRepositoryAsync enrollmentRepository,
            ICourseRepositoryAsync courseRepository,
            IMapper mapper,IPaymentService paymentService)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
            _paymentService = paymentService;
        }

        public async Task<Response<string>> Handle(CreateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            // 1. Get the course to find the ACTUAL current price
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null) throw new ApiException("Course not found");

            var enrollment = _mapper.Map<Enrollment>(request);

            enrollment.PurchasePrice = course.PriceUSD; 
            enrollment.IsPaid = false; 
            enrollment.EnrolledAt = DateTime.UtcNow;
            enrollment.ProgressPercentage = 0;
            enrollment.IsActive = false;

            // Check if student is already enrolled
            var isAlreadyEnrolled = await _enrollmentRepository.IsUserEnrolled("userIdFromUserService", request.CourseId);
            if (isAlreadyEnrolled)
            {
                throw new ApiException("You are already enrolled in this course.");
            }
            await _enrollmentRepository.AddAsync(enrollment);

            var checkoutUrl = await _paymentService.CreatePaymentSession(enrollment.Id, enrollment.PurchasePrice);

            return new Response<string>(checkoutUrl);
        }
    }
}
