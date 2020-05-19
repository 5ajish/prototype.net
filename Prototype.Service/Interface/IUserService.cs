using Microsoft.AspNetCore.Identity;
using Prototype.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Prototype.Service
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers();
        UserDTO GetUser(Guid id);
    }

}
