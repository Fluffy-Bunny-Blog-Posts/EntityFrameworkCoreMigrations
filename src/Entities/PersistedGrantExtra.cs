using Duende.IdentityServer.EntityFramework.Entities;

namespace Entities
{
    public class PersistedGrantExtra : PersistedGrant
    {
        public string RefreshTokenKey { get; set; }
    }
}