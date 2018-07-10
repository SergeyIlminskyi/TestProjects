using System;

namespace SWAG.Data
{
    public interface IEntity
    {
        Guid Id { get; }

        DateTime? CreatedOn { get; set; }
    }
}
