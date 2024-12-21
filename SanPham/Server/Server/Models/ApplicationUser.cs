using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Server.Models
{
    [CollectionName("Users")]
    public class ApplicationUser:MongoIdentityUser<Guid>
    {
    }
}
