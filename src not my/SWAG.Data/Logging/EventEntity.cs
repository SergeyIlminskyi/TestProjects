using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAG.Data
{
    [Serializable]
    public class EventEntity
        : Entity, IEntity
    {
        [Column(Order = 102)]
        public Int32? Code { get; set; }

        [Column(Order = 104)]
        public EventLevel Level { get; set; }

        [Column(Order = 105)]
        [MaxLength(4000)]
        public String Message { get; set; }

        public EventEntity()
            : base()
        { }
    }
}
