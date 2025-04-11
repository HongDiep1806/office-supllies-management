using MediatR;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GenerateApprovedRequestsExcelQuery : IRequest<byte[]>
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }
        public string? Department { get; }

        public GenerateApprovedRequestsExcelQuery(DateTime startDate, DateTime endDate, string? department)
        {
            StartDate = startDate;
            EndDate = endDate;
            Department = department;
        }
    }
}
