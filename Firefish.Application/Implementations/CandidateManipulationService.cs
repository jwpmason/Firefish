using Firefish.Application.Interfaces;
using Firefish.Application.Models.Input;
using Firefish.Domain.Entities;
using Firefish.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace Firefish.Application.Implementations
{
    public class CandidateManipulationService : ICandidateManipulationService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateManipulationService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        public async Task<int> AddCandidate(CandidateDetailsWithSkills details)
        {
            var timeNow = DateTime.Now;

            var candidateObject = new Candidate()
            {
                FirstName = details.FirstName,
                Surname = details.LastName,
                DateOfBirth = details.DateOfBirth,
                Address1 = details.Address1,
                Town = details.Town,
                Country = details.Country,
                PostCode = details.PostCode,
                PhoneHome = details.PhoneHome,
                PhoneMobile = details.PhoneMobile,
                PhoneWork = details.PhoneWork,
                CreatedDate = timeNow,
                UpdatedDate = timeNow
            };


            var candidateId = await _candidateRepository.Add(candidateObject);

            if(details.SkillIds != null)
            {
                int numSkills = details.SkillIds.Count;
                if (numSkills > 0)
                {
                    // If number of records created doesn't match size of input list consider rolling back entire transaction
                    var numRecords = await _candidateRepository.AddSkills(candidateId, details.SkillIds);
                }
            }

            return candidateId;
        }

        public async Task<bool> UpdateCandidate(int id, CandidateDetails details)
        {
            var candidate = await _candidateRepository.GetByIdAsync(id);

            if(candidate == null)
            {
                throw new NullReferenceException();
            }

            candidate.FirstName = details.FirstName;
            candidate.Surname = details.LastName;
            candidate.DateOfBirth = details.DateOfBirth;
            candidate.Address1 = details.Address1;
            candidate.Town = details.Town;
            candidate.Country = details.Country;
            candidate.PostCode = details.PostCode;
            candidate.PhoneHome = details.PhoneHome;
            candidate.PhoneMobile = details.PhoneMobile;
            candidate.PhoneWork = details.PhoneWork;
            candidate.UpdatedDate = DateTime.Now;

            return await _candidateRepository.Update(id, candidate);
        }
    }
}
