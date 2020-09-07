using System.ComponentModel.DataAnnotations;

namespace AuthApi.Domain.Helpers

{
    public class UserRole
    {
        [Key]
        public int MyId { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }
}
