using E_learningPlatform.Application.Features.Sections.Queries.GetAllQuery;
using E_learningPlatform.Application.Interfaces.Repositories;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using E_learningPlatform.Infrastructure.Persistence.Contexts;
using E_learningPlatform.Infrastructure.Persistence.QueryExtensions;
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
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            pageSize = pageSize <= 0 ? 10 : pageSize;

            var query = _context.Sections.AsNoTracking();
            
            var totalRecords = await query.ApplyFilters(filter).CountAsync();

            var sections = await query.ApplyFilters(filter)
                .ApplyIncludes(includes).
                ApplyOrdering(orderKey, orderDescending)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResponse<IEnumerable<Section>>(sections, pageNumber, pageSize, totalRecords);
        }

        public async Task<Section> GetSectionByIdAsync(int id, SectionIncludes includes)
        {
          return await _context.Sections.AsNoTracking()
                .ApplyIncludes(includes)
                .FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
