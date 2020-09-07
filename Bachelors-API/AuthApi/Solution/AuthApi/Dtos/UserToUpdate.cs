namespace AuthApi.Dtos
{
    public class UserToUpdate
    {
        public string email { get; set; }
        public UserMetadata user_metadata { get; set; }
    }
}
