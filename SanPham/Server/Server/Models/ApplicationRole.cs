using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace Server.Models
{
    [CollectionName("Roles")]
    public class ApplicationRole:MongoIdentityRole<Guid>
    {
    }
}
