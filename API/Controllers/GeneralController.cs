using API.Contracts;
using API.Ultilities.Handler;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found!"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<TEntity>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = entities
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var entities = _generalRepository.GetByGuid(guid);
            if (entities is null)
            {
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not Found"
                });
            }
            return Ok(new ResponseHandler<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data has Found",
                Data = entities
            });
        }

        [HttpPost]
        public IActionResult Create(TEntity entity)
        {
            var entities = _generalRepository.Create(entity);
            if (entities is null)
            {
                return BadRequest(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Failed To Create",
                });
            }
            return Ok(new ResponseHandler<TEntity>
            {
                Code = StatusCodes.Status201Created,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Success to create",
                Data = entities
            });
        }

        [HttpPut]
        public IActionResult Update(TEntity entity)
        {
            //var getGuid = (Guid)typeof(TEntity).GetProperty("Guid")!.GetValue(entity)!;
            //var isFound = _generalRepository.isExist(getGuid);
            //if (!isFound)
            //{
            //    return NotFound(new ResponseHandler<TEntity>
            //    {
            //        Code = StatusCodes.Status404NotFound,
            //        Status = HttpStatusCode.InternalServerError.ToString(),
            //        Message = "Id Not Found"
            //    });
            //}

            var isUpdate = _generalRepository.Update(entity);
            if (!isUpdate)
            {
                return BadRequest(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Check your data"
                });
            }
            return Ok(new ResponseHandler<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {

            var isFound = _generalRepository.isExist(guid);
            if (isFound == false)
            {
                return NotFound(new ResponseHandler<TEntity>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }

            //var entities = _generalRepository.Delete(guid);
            //if (!entities)
            //{
            //    return BadRequest(new ResponseHandler<TEntity>
            //    {
            //        Code = StatusCodes.Status500InternalServerError,
            //        Status = HttpStatusCode.InternalServerError.ToString(),
            //        Message = "Check Your Data"
            //    });
            //}
            return Ok(new ResponseHandler<TEntity>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Succesfull Delete"
            });
        }

    }
}
