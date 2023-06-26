using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : GeneralController<IUniversityRepository, University>
    {
        public UniversityController(IUniversityRepository generalRepository) : base(generalRepository)
        {
        }

        [HttpGet("nama")]
        public IActionResult GetByName(string name)
        {

            var nama = _generalRepository.GetByName(name);
            if (nama == null)
            {
                return NotFound();
            }

            return Ok(nama);
        }
    }
}
