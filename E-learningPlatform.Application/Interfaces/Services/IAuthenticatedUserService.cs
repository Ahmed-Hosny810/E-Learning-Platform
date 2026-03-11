using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Application.Interfaces.Services
{
    public interface IAuthenticatedUserService
    {
        string? UserId { get; }    
        string? Email { get; }       
        IEnumerable<string> Roles { get; } 
    }
}
