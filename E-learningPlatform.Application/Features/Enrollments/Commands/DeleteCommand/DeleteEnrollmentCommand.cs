using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Enrollments.Commands.DeleteCommand
{
    public class DeleteEnrollmentCommand: IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteEnrollmentCommandHandler : IRequestHandler<DeleteEnrollmentCommand, Response<int>>
    {
        private readonly IEnrollmentRepositoryAsync _repository;

        public DeleteEnrollmentCommandHandler(IEnrollmentRepositoryAsync repository)
        {
            _repository = repository;
        }
        public async Task<Response<int>> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var enrollment = await _repository.GetByIdAsync(request.Id);

            if (enrollment == null)
                throw new ApiException("Enrollment not found");

            // Check if they've paid before allowing a "True" delete
            if (enrollment.IsPaid)
            {
                // Safety: Don't delete paid records, just deactivate them
                enrollment.IsActive = false;
                await _repository.UpdateAsync(enrollment);
                return new Response<int>(enrollment.Id);
            }

            // If it was a mistake/not paid (Hard delete)
            await _repository.DeleteAsync(enrollment);
            return new Response<int>(enrollment.Id);
        }
    }
}
