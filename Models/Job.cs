namespace contractor.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }

    }

    public class ContractorJobViewModel : Job
    {
        public int ContractorJobId { get; set; }
    }
}