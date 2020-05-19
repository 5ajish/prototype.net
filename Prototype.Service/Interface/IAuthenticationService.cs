using Microsoft.AspNetCore.Identity;
using Prototype.Domain;
using System.Threading.Tasks;

namespace Prototype.Service
{
    public interface IAuthenticationService
    {
        Task<IdentityResult> SignUp(SignupDTO singupUser);
        Task<SigninResponseDTO> SignIn(string userName, string password);
    }
}
