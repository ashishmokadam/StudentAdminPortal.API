using AutoMapper;
using StudentAdminPortal.API.DomainModels;
using DataModels =  StudentAdminPortal.API.DataModels;

namespace StudentAdminPortal.API.Profiles
{
    public class AutoMapperPofiles : Profile
    {
        public AutoMapperPofiles()
        {
            CreateMap<DataModels.Student, Student>();

            CreateMap<DataModels.Gender, Gender>();

            CreateMap<DataModels.Address, Address>();

            // CreateMap<DataModels.Student, Student>().ReverseMap();
        }
    }
}
