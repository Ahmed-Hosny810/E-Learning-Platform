using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Enrollments.Commands.UpdateCommand
{
    public class UpdateEnrollmentProgressCommand:IRequest<Response<int>>
    {
        public int EnrollmentId { get; set; }
        public decimal ProgressPercentage { get; set; }
    }
    public class UpdateEnrollmentProgressCommandHandler : IRequestHandler<UpdateEnrollmentProgressCommand, Response<int>>
    {
        private readonly IEnrollmentRepositoryAsync _repository;

        public UpdateEnrollmentProgressCommandHandler(IEnrollmentRepositoryAsync repository)
        {
            _repository = repository;
        }
        public async Task<Response<int>> Handle(UpdateEnrollmentProgressCommand request, CancellationToken cancellationToken)
        {
            var enrollment= await _repository.GetByIdAsync(request.EnrollmentId);

            if (enrollment == null) throw new ApiException("Enrollment not found");

            if (request.ProgressPercentage > enrollment.ProgressPercentage)
            {
                enrollment.ProgressPercentage = request.ProgressPercentage;
            }

            if (enrollment.ProgressPercentage >= 100 && !enrollment.CompletedAt.HasValue)
            {
                enrollment.CompletedAt = DateTime.UtcNow;
            }

            await _repository.UpdateAsync(enrollment);
            return new Response<int>(request.EnrollmentId);
        }
    }

}
