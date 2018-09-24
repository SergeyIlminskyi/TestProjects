using ABMCloud.Models;
using ABMCloud.Entities;
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
                cfg.CreateMap<VacationFilter, VacationFilterModel>();
                cfg.CreateMap<EmployeeFilter, EmployeeFilterModel>();
            });
        }
    }
}