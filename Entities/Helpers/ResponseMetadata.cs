namespace Entities
{
    public class ResponseMetadata<T>
    {
        public IEnumerable<T> Data { get; set; }
        public Metadata Meta { get; set; }
    }
}
