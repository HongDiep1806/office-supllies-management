using MediatR;
using Office_supplies_management.DTOs.Request;

public class GetRequestsByDepartmentQuery : IRequest<List<RequestDto>>
{
    public string DepartmentName { get; }

    public GetRequestsByDepartmentQuery(string departmentName)
    {
        DepartmentName = departmentName;
    }
}

