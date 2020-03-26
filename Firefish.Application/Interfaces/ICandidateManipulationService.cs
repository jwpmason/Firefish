using Firefish.Application.Models.Input;
using System.Threading.Tasks;

namespace Firefish.Application.Interfaces
{
    public interface ICandidateManipulationService
    {
        Task<int> AddCandidate(CandidateDetailsWithSkills details);
        Task<bool> UpdateCandidate(int id, CandidateDetails details);

    }
}