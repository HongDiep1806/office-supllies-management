using MediatR;

public class UpdateTotalPriceCommand : IRequest<bool>
{
    public int RequestID { get; set; }

    public UpdateTotalPriceCommand(int requestID)
    {
        RequestID = requestID;
    }
}
