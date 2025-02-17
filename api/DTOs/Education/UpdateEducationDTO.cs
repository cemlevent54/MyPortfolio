namespace api.DTOs.Education
{
    public class UpdateEducationDTO
    {
        public string Education_Title { get; set; }
        public string? Education_Organization { get; set; }
        public string? Education_Subject { get; set; }
        public string? Education_StartDate { get; set; }
        public string? Education_EndDate { get; set; }
        public string? Education_CertificationUrl { get; set; }
        public string User_ID { get; set; }
        public int EducationCategory_ID { get; set; }
    }
}
