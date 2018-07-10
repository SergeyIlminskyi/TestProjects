using SWAG.Data;
using System;
using System.Linq;
using Xunit;

namespace SWAG.Test
{
    public class OperationEntityTest
    {
        public OperationEntityTest()
        { }

        [Theory(DisplayName = nameof(Operation_Entity_Test))]
        [Trait("Category", "Entity")]
        [InlineData("1;7", 8D)]
        [InlineData("", 0D)]
        [InlineData("1", 1D)]
        public void Operation_Entity_Test(String value, Double result)
        {
            OperationEntity operationEntity = new OperationEntity
            {
                Id = Guid.NewGuid(),
                CreatedOn = DateTime.UtcNow,
                Type = OperationType.Addition,
                Serialized = value,
                Result = result,
            };

            Assert.NotNull(operationEntity.Value);

            if (!String.IsNullOrEmpty(value))
            {
                Assert.NotEmpty(operationEntity.Value);
            }

            Assert.Equal(operationEntity.Result, operationEntity.Value.Sum());
        }

        [Fact(DisplayName = nameof(Operation_Entity_Failed_Test))]
        [Trait("Category", "Entity")]
        public void Operation_Entity_Failed_Test()
        {
            Assert.Throws<InvalidCastException>(() =>
            {
                OperationEntity operationEntity = new OperationEntity
                {
                    Id = Guid.NewGuid(),
                    CreatedOn = DateTime.UtcNow,
                    Type = OperationType.Addition,
                    Serialized = "a",
                };
            });
        }
    }
}
