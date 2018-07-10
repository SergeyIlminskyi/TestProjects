using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace SWAG.Data
{
    public partial class AppDbContext
        : DbContext
    {
        protected virtual void FillData(EntityTypeBuilder<OperationEntity> entity)
        {
            entity.HasData(new OperationEntity[]
            {
                new OperationEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.UtcNow.Date,
                    Type = OperationType.Addition,
                    Serialized = "1;7",
                    Result = 8D,
                },
            });
        }
    }
}
