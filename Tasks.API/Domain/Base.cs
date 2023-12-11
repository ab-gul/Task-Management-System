using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Tasks.API.Domain
{
    public abstract class Base
    {
        [Key]
        [Column("ID")]
        public Guid Id { get; protected set; } = Guid.NewGuid();
    }
}
