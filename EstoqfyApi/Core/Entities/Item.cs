namespace EstoqFy.Core.Entities
{
    public class Item : EntityBase
    {
        public string EAN { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public DateTime LastAdd { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
