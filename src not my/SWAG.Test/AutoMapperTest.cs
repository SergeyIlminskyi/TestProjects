using AutoMapper;
using SWAG.Models;
using Xunit;

namespace SWAG.Test
{
    public class AutoMapperTest
    {
        [Fact]
        [Trait("Category", "AutoMapper")]
        public void MappingProfile()
        {
            Mapper mapper = new Mapper(new MapperConfiguration(options =>
            {
                options.AddProfile(new EntityToModelProfile());
                options.AddProfile(new ModelToEntityProfile());
            }));

            (mapper as IMapper).ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
