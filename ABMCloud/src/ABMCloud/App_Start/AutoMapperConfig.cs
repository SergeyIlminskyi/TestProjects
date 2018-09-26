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
                cfg.CreateMap<VacationInfo,VacationDetailsModel>();
                cfg.CreateMap<VacationDetailsModel, VacationInfo>()
                    .ForPath(dest => dest.Vacationist.Id, opt => opt.MapFrom(c => c.VacationistId))
                    .ForPath(dest => dest.Substitutional.Id, opt => opt.MapFrom(c => c.SubstitutionalId));
                cfg.CreateMap<EmployeeInfo, EmployeeSimpleModel>()
                    .ForMember(dest => dest.FullName, opt => opt.MapFrom(c => c.Surname + " " + c.Name + " " + c.Patronymic));

                
            });
        }
    }
}