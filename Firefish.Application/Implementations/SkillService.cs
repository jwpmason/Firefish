using Firefish.Application.Interfaces;
using Firefish.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skill = Firefish.Application.Models.Response.Skill;

namespace Firefish.Application.Implementations
{
    public class SkillService : ISkillService
    {
        private readonly ISkillRepository _skillRepository;

        public SkillService(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<IEnumerable<Skill>> RetrieveAllSkills()
        {
            List<Domain.Entities.Skill> skills = await _skillRepository.GetAllSkillsAsync();

            return skills.Select(s => new Skill()
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
    }
}
