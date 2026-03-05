using E_learningPlatform.Application.Exceptions;
using E_learningPlatform.Application.Interfaces.Services;
using E_learningPlatform.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CreatePaymentSession(int enrollmentId, decimal amount)
        {
            var requestBody = new { EnrollmentId = enrollmentId, Amount = amount };

            // This calls your Payment Minimal API
            var response = await _httpClient.PostAsJsonAsync("api/payments/create-session", requestBody);

            if (!response.IsSuccessStatusCode)
                throw new ApiException("Could not initialize payment session.");

            var result = await response.Content.ReadFromJsonAsync<PaymentSessionResponse>();
            return result.CheckoutUrl;
        }
    }
}
