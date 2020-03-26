using Firefish.Application.Interfaces;
using Firefish.Application.Models.Input;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Firefish.WebApi.Controllers
{
    [RoutePrefix("api/candidate")]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class CandidateController : ApiController
    {
        private readonly ICandidateRetrievalService _candidateRetrievalService;
        private readonly ICandidateManipulationService _candidateManipulationService;

        public CandidateController(ICandidateRetrievalService candidateRetrievalService, ICandidateManipulationService candidateManipulationService)
        {
            _candidateRetrievalService = candidateRetrievalService;
            _candidateManipulationService = candidateManipulationService;
        }

        [HttpGet]
        [Route("{id}", Name = "Get Candidate")]
        public async Task<IHttpActionResult> GetCandidate(int id)
        {
            var response = await _candidateRetrievalService.RetrieveCandidate(id);

            if(response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }


        [HttpGet]
        [Route("list", Name = "Get All Candidates")]

        public async Task<IHttpActionResult> GetCandidateList()
        {
            var response = await _candidateRetrievalService.RetrieveCandidateList();

            return Ok(response);
        }

        [HttpPost]
        [Route("", Name = "Create Candidate")]
        public async Task<IHttpActionResult> CreateCandidate(CandidateDetailsWithSkills details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await _candidateManipulationService.AddCandidate(details);

            return Ok(id);
        }

        [HttpPut]
        [Route("{id}", Name = "Update Candidate")]
        public async Task<IHttpActionResult> UpdateCandidate(int id, CandidateDetails details)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            try
            {
                var response = await _candidateManipulationService.UpdateCandidate(id, details);

                return Ok(response);
            } catch(NullReferenceException) {
                return BadRequest("Candidate does not exist");
            }
        }
    }
}
