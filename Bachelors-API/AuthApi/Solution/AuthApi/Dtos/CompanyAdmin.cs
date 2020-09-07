namespace AuthApi.Dtos
{
    public class CompanyAdmin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int CompanyId { get; set; }
        public string Type { get; set; }
    }
}
