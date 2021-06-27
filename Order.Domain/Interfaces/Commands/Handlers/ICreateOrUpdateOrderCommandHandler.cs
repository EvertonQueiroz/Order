using Order.Domain.Commands.Requests;
using Order.Domain.Commands.Responses;

namespace Order.Domain.Interfaces.Commands.Handlers
{
    public interface ICreateOrUpdateOrderCommandHandler
    {
        CreateOrUpdateOrderResponse Handle(CreateOrUpdateOrderRequest command);
    }
}
