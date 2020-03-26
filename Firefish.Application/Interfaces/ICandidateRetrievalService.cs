using Firefish.Application.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Firefish.Application.Interfaces
{
    public interface ICandidateRetrievalService
    {
        Task<CandidateDetailed> RetrieveCandidate(int id);
        Task<List<CandidateBasic>> RetrieveCandidateList();
    }
}