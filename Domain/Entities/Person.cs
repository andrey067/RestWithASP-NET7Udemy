using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("person")]
    public class Person
    {
        [Column("id")]
        public long Id { get; private set; }

        [Column("first_name")]
        public string FirstName { get; private set; } = string.Empty;

        [Column("last_name")]
        public string LastName { get; private set; } = string.Empty;

        [Column("address")]
        public string Address { get; private set; } = string.Empty;
        [Column("gender")]
        public string Gender { get; private set; } = string.Empty;
    }

}