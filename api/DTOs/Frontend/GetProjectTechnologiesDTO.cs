namespace api.DTOs.Frontend
{
    internal class GetProjectTechnologiesDTO
    {
        public int Technology_ID { get; set; }
        public string? Technology_Name { get; set; }
        public string? Technology_IconUrl { get; set; }
    }
}