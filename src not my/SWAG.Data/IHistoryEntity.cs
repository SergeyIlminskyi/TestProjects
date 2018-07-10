using System;

namespace SWAG.Data
{
    public interface IHistoryEntity
        : IEntity
    {
        DateTime? ModifiedOn { get; set; }
    }
}
