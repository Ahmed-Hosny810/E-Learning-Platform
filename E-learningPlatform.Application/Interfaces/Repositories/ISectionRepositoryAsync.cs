
using E_learningPlatform.Application.Features.Sections.Queries.GetAllQuery;
using E_learningPlatform.Application.Wrappers;
using E_learningPlatform.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Repositories
{
    public interface ISectionRepositoryAsync: IGenericRepositoryAsync<Section, int>
    {
        Task<PagedResponse<IEnumerable<Section>>> GetSectionsPagedResponseAsync(SectionFilter filter, SectionIncludes includes,
            SectionOrderKey orderKey, bool orderDescending, int currentPage, int pageSize);

        Task<Section> GetSectionByIdAsync(int id, SectionIncludes includes);
    }
}
