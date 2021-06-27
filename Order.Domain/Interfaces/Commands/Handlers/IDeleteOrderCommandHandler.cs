using Order.Domain.Commands.Requests;

namespace Order.Domain.Interfaces.Commands.Handlers
{
    public interface IDeleteOrderCommandHandler
    {
        void Handle(DeleteOrderRequest command);
    }
}
