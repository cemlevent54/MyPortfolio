namespace api.DTOs.Talent
{
    public class TalentsModelDTO
    {
        public List<TalentsIDDTO> talentItemsForUser { get; set; } = new List<TalentsIDDTO>();
    }
}
