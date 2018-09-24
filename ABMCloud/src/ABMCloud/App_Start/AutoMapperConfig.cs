using ABMCloud.Models;
using ABMCloud.Entites;
using AutoMapper;

namespace ABMCloud
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<EmployeeInfo, EmployeeDetailsModel>();
                cfg.CreateMap<EmployeeDetailsModel, EmployeeInfo>();
            });
        }
    }
}