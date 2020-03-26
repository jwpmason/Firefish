using Firefish.Application.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firefish.Application.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<Skill>> RetrieveAllSkills();
    }
}