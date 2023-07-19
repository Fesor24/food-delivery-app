using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Identity
{
    public class Address : BaseEntity
    {
        public string City { get; set; }

        public string Street { get; set; }

        public string State { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
