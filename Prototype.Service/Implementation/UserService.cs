using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Prototype.Core;
using Prototype.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Transactions;

namespace Prototype.Service
{
    public class UserService: IUserService
    {

        private readonly IUnitOfWork _uow;
        
        public UserService(IUnitOfWork uow, UserManager<ApplicationUser> userManager)
        {
            _uow = uow;
        }

        public IEnumerable<UserDTO> GetUsers()
        {
            var items = _uow.GetRepository<ApplicationUser>().GetAll();
            return items.Select(x => new UserDTO().MapToUserDTO(x));
        }

        public UserDTO GetUser(Guid id)
        {
            var item = _uow.GetRepository<ApplicationUser>().GetById(id);
            return item != null ? new UserDTO().MapToUserDTO(item) : null;
        }
    } 
}
