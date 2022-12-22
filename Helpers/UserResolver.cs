using System;
namespace StoreCRM.Helpers
{
	public class UserResolver
	{
        private readonly IHttpContextAccessor _context;
        public UserResolver(IHttpContextAccessor context)
        {
            _context = context;
        }

        public virtual Guid GetUserId()
        {
            var userId = _context.HttpContext.User?.Identity?.Name;
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            return new Guid(userId);
        }
    }
}

