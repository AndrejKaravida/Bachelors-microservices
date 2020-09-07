namespace AuthApi.Dtos
{
    public class UserFromServer
    {
        public string user_id { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public UserMetadata user_metadata { get; set; }
    }
}
