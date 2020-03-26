using System.Collections.Generic;

namespace Firefish.Application.Models.Input
{
    public class CandidateDetailsWithSkills : CandidateDetails
    {
        public List<int> SkillIds { get; set; }
    }
}
