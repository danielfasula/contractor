namespace contractor.Models
{
    public class ContractorJob
    {
        public int Id { get; set; }
        public int ContractorId { get; set; }
        public int JobId { get; set; }
        public string CreatorId { get; set; }
    }
}