// File: Features/Summary/Queries/GetSummariesByUserIdQuery.cs
using MediatR;
using Office_supplies_management.DTOs.Summary;
using System.Collections.Generic;

namespace Office_supplies_management.Features.Summary.Queries
{
    public class GetSummariesByUserIdQuery : IRequest<List<SummaryDto>>
    {
        public int UserId { get; }

        public GetSummariesByUserIdQuery(int userId)
        {
            UserId = userId;
        }
    }
}
