using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Constants;
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
        public string Status { get; set; } 
        public string? FailureReason { get; set; } 
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
            // 1. Idempotency — check by provider transaction ID first
            //var existingPayment = await _paymentRepo.GetByProviderTransactionIdAsync(request.ProviderTransactionId);
            //if (existingPayment != null)
            //    return new Response<int>(existingPayment.EnrollmentId);

            // 2. Validate enrollment exists
            var enrollment = await _enrollmentRepo.GetByIdAsync(request.EnrollmentId);
            if (enrollment == null)
                throw new ApiException("Enrollment not found.");

            // 3. Record the payment first
            var payment = new Payment
            {
                EnrollmentId = enrollment.Id,
                Amount = request.AmountPaid,
                Currency = request.Currency,
                ProviderPaymentId = request.ProviderTransactionId,
                Status = request.Status,
                PaidAt = request.Status == PaymentStatus.Succeeded ? DateTime.UtcNow : null,
                FailureReason = request.Status == PaymentStatus.Failed ? request.FailureReason : null
            };
            await _paymentRepo.AddAsync(payment);

            // 4. Only activate enrollment on success
            if (request.Status == PaymentStatus.Succeeded)
            {
                // Verify amount matches
                if (request.AmountPaid < enrollment.PurchasePrice)
                    throw new ApiException("Payment amount does not match course price.");

                enrollment.IsPaid = true;
                enrollment.IsActive = true;
                await _enrollmentRepo.UpdateAsync(enrollment);
            }

            return new Response<int>(enrollment.Id);
        }
    }
}
