using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Enums;
using E_learningPlatform.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Features.Payments.Commands.CreateCommand
{
    public class ConfirmPaymentCommand:IRequest<Response<int>>
    {
        public int EnrollmentId { get; set; }
        public string ProviderTransactionId { get; set; } 
        public decimal AmountPaid { get; set; }
        public string Currency { get; set; }
        public PaymentStatus Status { get; set; } 
    }
    public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, Response<int>>
    {

        private readonly IEnrollmentRepositoryAsync _enrollmentRepo;
        private readonly IPaymentRepositoryAsync _paymentRepo;

        public ConfirmPaymentCommandHandler(IEnrollmentRepositoryAsync enrollmentRepo, IPaymentRepositoryAsync paymentRepo)
        {
            _enrollmentRepo = enrollmentRepo;
            _paymentRepo = paymentRepo;
        }
        
        public async Task<Response<int>> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            var enrollment = await _enrollmentRepo.GetByIdAsync(request.EnrollmentId);
            if (enrollment == null) throw new ApiException("Enrollment not found");

            // Logic to prevent duplicate processing (Idempotency)
            if (enrollment.IsPaid) return new Response<int>(request.EnrollmentId);

            enrollment.IsPaid = true;
            enrollment.IsActive = true;
            await _enrollmentRepo.UpdateAsync(enrollment);

            var payment = new Payment
            {
                EnrollmentId = enrollment.Id,
                Amount = request.AmountPaid,
                Currency = request.Currency,
                ProviderPaymentId = request.ProviderTransactionId,
                Status = request.Status
            };
            await _paymentRepo.AddAsync(payment);

            return new Response<int>(request.EnrollmentId);
        }
    }
}
