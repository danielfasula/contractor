namespace contractor.Models
{
    public class Contractor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatorId { get; set; }
        public Profile Creator { get; set; }
    }

}