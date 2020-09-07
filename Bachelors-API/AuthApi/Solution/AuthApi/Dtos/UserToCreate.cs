namespace AuthApi.Dtos
{
    public class UserToCreate
    {
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string name { get; set; }
        public string connection { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public string password { get; set; }
    }
}
