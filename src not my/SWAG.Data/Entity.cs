using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAG.Data
{
    [Serializable]
    public abstract class Entity
        : IEntity
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Column(Order = 1)]
        [DataType(DataType.Date)]
        public DateTime? CreatedOn { get; set; }

        public Entity()
        { }
    }
}
