using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAG.Data
{
    [Serializable]
    public abstract class HistoryEntity
        : Entity, IHistoryEntity, IEntity
    {
        [Column(Order = 2)]
        [DataType(DataType.Date)]
        public DateTime? ModifiedOn { get; set; }

        public HistoryEntity()
            : base()
        { }
    }
}
