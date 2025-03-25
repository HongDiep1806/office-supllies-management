using MediatR;
using Office_supplies_management.DTOs.Request;
using System.Collections.Generic;

public class GetRequestsByProductIDQuery : IRequest<List<RequestDto>>
{
    public int ProductID { get; set; }

    public GetRequestsByProductIDQuery(int productID)
    {
        ProductID = productID;
    }
}

