using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : GeneralController<University>
    {
        // private readonly IUniversityRepository _repository;

        public UniversityController(IUniversityRepository repository) : base(repository)
        {
            // _repository = repository;
        }
    }
}
