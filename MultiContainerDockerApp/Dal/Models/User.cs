using NodaTime;
using System.Text.Json.Serialization;

namespace MultiContainerDockerApp.Dal.Models
{
    public class User
    {
        public int Id { get; set; }
        public LocalDateTime Created { get; set; }
        public LocalDateTime Modified { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }
        [JsonIgnore]
        public string PasswordHash { get; set; }
        public string? County { get; set; }
        public string? State { get; set; }
        public string? Zipcode { get; set; }

        //BloodType Relation
        public int? BloodTypeId { get; set; }
        public virtual BloodType? BloodType { get; set; }
    }
}
