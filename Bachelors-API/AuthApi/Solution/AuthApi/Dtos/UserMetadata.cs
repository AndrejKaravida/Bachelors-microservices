namespace AuthApi.Dtos
{
    public class UserMetadata
    {
        public string first_name { get; set; } = "";
        public string last_name { get; set; } = "";
        public string city { get; set; } = "";
        public string phone_number { get; set; } = "";
        public bool needToChangePassword { get; set; } = false;
    }
}
