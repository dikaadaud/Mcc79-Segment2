using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GeneralController<TRepo, TEntity> : ControllerBase
        where TEntity : class
        where TRepo : IGeneralRepository<TEntity>
    {
        protected readonly TRepo _generalRepository;

        public GeneralController(TRepo generalRepository)
        {
            _generalRepository = generalRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _generalRepository.GetAll();

            if (!entities.Any())
            {
                return NotFound();
            }
            return Ok(entities);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var entities = _generalRepository.GetByGuid(guid);
            if (entities is null)
            {
                return NotFound();
            }
            return Ok(entities);
        }

        [HttpPost]
        public IActionResult Create(TEntity entity)
        {
            var entities = _generalRepository.Create(entity);
            if (entities is null)
            {
                return NotFound();
            }
            return Ok(entities);
        }

        [HttpPut]
        public IActionResult Update(TEntity entity)
        {
            var entities = _generalRepository.Update(entity);
            if (!entities)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var entities = _generalRepository.Delete(guid);
            if (!entities)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
