﻿using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _repository;

        public UniversityController(IUniversityRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var university = _repository.GetAll();

            if (!university.Any())
            {
                return NotFound();
            }

            return Ok(university);
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var university = _repository.GetByGuid(guid);

            if (university is null)
            {
                return NotFound();
            }

            return Ok(university);
        }

        [HttpPost]
        public IActionResult Create(University university)
        {
            var createUniversity = _repository.Create(university);
            return Ok(createUniversity);
        }

        [HttpPut]
        public IActionResult Update(University university)
        {
            var updateUniversity = _repository.Update(university);

            if (!updateUniversity)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var deleteUniversity = _repository.Delete(guid);

            if (!deleteUniversity)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
