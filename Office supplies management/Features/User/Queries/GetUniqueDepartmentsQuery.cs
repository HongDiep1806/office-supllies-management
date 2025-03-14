using MediatR;
using System.Collections.Generic;

namespace Office_supplies_management.Features.User.Queries
{
    public class GetUniqueDepartmentsQuery : IRequest<List<string>>
    {
    }
}
