namespace Entities
{
    public class CustomBase : EntityBase
    {
        public Tracker CreatedBy { get; set; }
        public Tracker UpdatedBy { get; set; }
    }
}
