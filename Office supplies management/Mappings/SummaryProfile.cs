using AutoMapper;
using Office_supplies_management.DTOs.Summary;
using Office_supplies_management.Models;

namespace Office_supplies_management.Mappings
{
    public class SummaryProfile: Profile
    {
        public SummaryProfile()
        {
            CreateMap<Summary, SummaryDto>()
           .ForMember(dest => dest.RequestIDs, opt => opt.MapFrom(src => src.Requests.Select(r => r.RequestID).ToList()));

            CreateMap<SummaryDto, Summary>()
                .ForMember(dest => dest.Requests, opt => opt.Ignore());
        }
       
    }
}
