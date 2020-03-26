using Firefish.Application.Interfaces;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Firefish.WebApi.Controllers
{
    [RoutePrefix("api/skill")]
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]

    public class SkillController : ApiController
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        /*
         * Returns a list of all skills
         */
        [HttpGet]
        [Route("", Name = "Get Skills")]
        public async Task<IHttpActionResult> GetSkills()
        {
            var response = await _skillService.RetrieveAllSkills();

            return Ok(response);
        }

    }
}
