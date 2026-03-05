using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.Repositories
{
    public class PaymentRepositoryAsync : GenericRepositoryAsync<Payment, int>, IPaymentRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
