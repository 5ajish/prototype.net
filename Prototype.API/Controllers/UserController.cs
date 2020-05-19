using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Prototype.Domain;
using Prototype.Service;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Prototype.API.Controllers
{
    public class UserController : CommonController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetUsers();
            if (users.Count() > 0)
            {
                return Ok(users);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var user = _userService.GetUser(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }
    }
}
