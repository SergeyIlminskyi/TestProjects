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
                cfg.CreateMap<VacationFilterModel, VacationFilter > ()
                    .ForMember(dest => dest.CurrenPage, opt => opt.MapFrom(c => c.CurrentPagingInfo.Page))
                    .ForMember(dest => dest.PageSize, opt => opt.MapFrom(c => c.CurrentPagingInfo.ItemsPerPage));
                cfg.CreateMap<EmployeeFilterModel, EmployeeFilter>()
                    .ForMember(dest => dest.CurrenPage, opt => opt.MapFrom(c => c.CurrentPagingInfo.Page))
                    .ForMember(dest => dest.PageSize, opt => opt.MapFrom(c => c.CurrentPagingInfo.ItemsPerPage));
            });
        }
    }
}