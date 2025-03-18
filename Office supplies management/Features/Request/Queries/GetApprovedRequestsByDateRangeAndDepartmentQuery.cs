using MediatR;
using Office_supplies_management.DTOs.Request;
using System;

namespace Office_supplies_management.Features.Request.Queries
{
    public class GetApprovedRequestsByDateRangeAndDepartmentQuery : IRequest<List<RequestDto>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Department { get; set; }
    }
}

