using E_learningPlatform.Application.Features.Sections.Queries.GetAllQuery;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using E_learningPlatform.Infrastructure.Persistence.QueryHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;


namespace E_learningPlatform.Infrastructure.Persistence.Repositories
{
    public class SectionRepositoryAsync : GenericRepositoryAsync<Section, int>,ISectionRepositoryAsync
    {
        private readonly ApplicationDbContext _context;

        public SectionRepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedResponse<IEnumerable<Section>>> GetSectionsPagedResponseAsync(SectionFilter filter, SectionIncludes includes, SectionOrderKey orderKey, bool orderDescending, int pageNumber, int pageSize)
        {
            var helper = new SectionQueryHelper(_context.Sections.AsQueryable())
                .ApplyFilters(filter)
                .ApplyIncludes(includes);

            // Get count before ordering/paging for efficiency
            var totalRecords = await helper.Build().CountAsync();

            var sections = await helper.ApplyOrdering(orderKey, orderDescending)
                .Build()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<IEnumerable<Section>>(sections, pageNumber, pageSize, totalRecords);
        }

        public async Task<Section> GetSectionByIdAsync(int id, SectionIncludes includes)
        {
            return await new SectionQueryHelper(_context.Sections.AsQueryable())
                .ApplyIncludes(includes)
                .Build()
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
