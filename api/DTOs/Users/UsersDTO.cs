namespace api.DTOs.Users
{
    public class UsersDTO
    {
        public string id { get; set; }
        public string Email { get; set; }
        public string userName { get; set; }
        public string user_Name { get; set; }
        public string user_Surname { get; set; }
        public string user_Email { get; set; }
        public string user_Password { get; set; }
        public string user_PhoneNumber { get; set; }
        public string user_About { get; set; }
        public DateTime user_BirthDate { get; set; }
        public DateTime user_RegisteredAt { get; set; }
        public string user_PhotoUrl { get; set; }
        public bool user_State { get; set; }
        public string user_LivingCity { get; set; }
        public string user_CvUrl { get; set; }
        public string user_Job { get; set; }

        public string userRole { get; set; }
    }
}
