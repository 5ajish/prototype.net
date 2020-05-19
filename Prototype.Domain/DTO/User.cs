using System;
using System.Collections.Generic;
using System.Linq;

namespace Prototype.Domain
{
    public class UserDTO : SignupDTO
    {
        public Guid? Id { get; set; }
        public List<string> Roles { get; set; }

        public UserDTO MapToUserDTO(ApplicationUser user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName
            };
        }
    }
}
