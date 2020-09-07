using AuthApi.Domain.Helpers;

namespace AuthApi.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string AuthId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public virtual UserRole Role { get; set; }
        public string PhoneNumber { get; set; }
        public bool NeedToChangePassword { get; set; } = false;
    }
}
