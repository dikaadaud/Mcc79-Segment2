using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Educations")]
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _edurep;

        public EducationController(IEducationRepository edurep)
        {
            _edurep = edurep;
        }
        private readonly IBookingRepository _bookingRepository;

        [HttpGet]
        public IActionResult GetAll()
        {
            var educations = _edurep.GetAll();
            if (educations == null)
            {
                return NotFound();
            }
            return Ok(educations);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _edurep.GetByGuid(guid);
            if (booking == null)
            {
                return NotFound();
            }
            return Ok(booking);
        }

        [HttpPost]
        public IActionResult Create(Education education)
        {
            var educations = _edurep.Create(education);
            if (educations == null)
            {
                return NotFound();
            }
            return Ok(education);
        }

        [HttpPut]
        public IActionResult Update(Education education)
        {
            var educations = _edurep.Update(education);
            if (!educations)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var educations = _edurep.Delete(guid);
            if (!educations)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
