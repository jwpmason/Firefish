using Firefish.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firefish.Domain.Repositories
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllSkillsAsync();
    }
}
