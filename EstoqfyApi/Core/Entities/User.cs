using System.ComponentModel.DataAnnotations;

namespace EstoqFy.Core.Entities
{
    public class User : EntityBase
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Cnpj {  get; set; }
        public DateTime Create_time { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
