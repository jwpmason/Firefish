using Firefish.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firefish.Domain.Repositories
{
    public interface ICandidateRepository
    {
        Task<List<Candidate>> GetAllAsync();
        Task<Candidate> GetByIdAsync(int id);
        Task<int> Add(Candidate candidate);
        Task<int> AddSkills(int candidateId, IEnumerable<int> skillIds);
        Task<bool> Update(int id, Candidate candidate);
    }
}
