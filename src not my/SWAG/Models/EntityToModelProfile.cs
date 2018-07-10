using AutoMapper;
using SWAG.Data;

namespace SWAG.Models
{
    public class EntityToModelProfile
        : Profile
    {
        public EntityToModelProfile()
            : base()
        {
            CreateMap<OperationEntity, OperationModel>()
                ;
        }
    }
}
