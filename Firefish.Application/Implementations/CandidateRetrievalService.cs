using Firefish.Application.Interfaces;
using Firefish.Application.Models.Response;
using Firefish.Domain.Entities;
using Firefish.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skill = Firefish.Application.Models.Response.Skill;

namespace Firefish.Application.Implementations
{
    public class CandidateRetrievalService : ICandidateRetrievalService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateRetrievalService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<CandidateDetailed> RetrieveCandidate(int id)
        {
            Candidate candidate = await _candidateRepository.GetByIdAsync(id);

            if(candidate == null)
            {
                return null;
            }

            return new CandidateDetailed()
            {
                Id = candidate.Id,
                FirstName = candidate.FirstName,
                LastName = candidate.Surname,
                DateOfBirth = candidate.DateOfBirth,
                Address1 = candidate.Address1,
                Town = candidate.Town,
                Country = candidate.Country,
                PostCode = candidate.PostCode,
                PhoneHome = candidate.PhoneHome,
                PhoneMobile = candidate.PhoneMobile,
                PhoneWork = candidate.PhoneWork,
                CreatedDate = candidate.CreatedDate,
                UpdatedDate = candidate.UpdatedDate,
                Skills = candidate.Skills.Select(s => new Skill()
                {
                    Id = s.Id,
                    Name = s.Name
                }).ToList()
            };
        }

        public async Task<List<CandidateBasic>> RetrieveCandidateList()
        {
            IList<Candidate> candidates = await _candidateRepository.GetAllAsync();

            return candidates.Select(c => new CandidateBasic()
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.Surname,
                AddDate = c.CreatedDate
            }).ToList();
        }
    }
}
