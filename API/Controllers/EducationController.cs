using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Educations")]
    public class EducationController : GeneralController<Education>
    {
        // private readonly IEducationRepository _edurep;

        public EducationController(IEducationRepository edurep) : base(edurep)
        {

        }

    }
}
