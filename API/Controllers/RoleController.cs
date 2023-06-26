﻿using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/Roles")]

    public class RoleController : GeneralController<IRolesRepository, Role>
    {
        public RoleController(IRolesRepository rolesRepository) : base(rolesRepository)
        {
        }

    }
}
