using AutoMapper;
using Office_supplies_management.DTOs.Request;
using Office_supplies_management.Models;

namespace Office_supplies_management.Mappings
{
    public class RequestProfile : Profile
    {
        public RequestProfile() 
        {
            CreateMap<RequestDto, Request>();
            CreateMap<Request, RequestDto>();
            CreateMap<CreateRequestDto, Request>();
            CreateMap<UpdateRequestDto, Request>();
        }
    }
}
