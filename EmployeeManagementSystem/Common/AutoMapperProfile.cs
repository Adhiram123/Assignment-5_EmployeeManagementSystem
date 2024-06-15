using AutoMapper;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entites;

namespace EmployeeManagementSystem.Common
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EmployeeBasicEntity, EmployeeBasicDTO>().ReverseMap();
            CreateMap<EmployeeAdditonalInfoEntity, AdditionalInfoDTO>().ReverseMap();

        }
    }
}
