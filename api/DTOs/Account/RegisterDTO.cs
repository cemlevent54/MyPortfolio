namespace api.DTOs.Account
{
    public class RegisterDTO
    {
        public string? User_Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? User_Name { get; set; }
        public string? User_Surname { get; set; }
        public string? User_Email { get; set; }
        public string? User_Password { get; set; }
        public string? User_PhoneNumber { get; set; }
        public string? User_About { get; set; }
        public DateTime User_BirthDate { get; set; }
        public DateTime User_RegisteredAt { get; set; }
        public string? User_PhotoUrl { get; set; }
        public string? User_State { get; set; }
        public string? User_LivingCity { get; set; }
        public string? User_CvUrl { get; set; }

        //public IFormFile? file;
    }
}
