using System;

namespace SWAG
{
    public interface IHistoryModel
        : IModel
    {
        DateTime? ModifiedOn { get; set; }
    }
}
