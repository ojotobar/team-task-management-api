using Contracts;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserRepository(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetLoggedInUserId()
        {
            var userClaim = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            return userClaim != null ? userClaim.Value : string.Empty;
        }
    }
}
