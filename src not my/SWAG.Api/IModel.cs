using System;

namespace SWAG
{
    public interface IModel
    {
        Guid Id { get; set; }

        DateTime? CreatedOn { get; set; }
    }
}
