using System.Net.Sockets;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public Address Address { get; set; }
    }
}
