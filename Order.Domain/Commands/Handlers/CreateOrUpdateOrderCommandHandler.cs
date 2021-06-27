using Order.Domain.Commands.Requests;
using Order.Domain.Commands.Responses;
using Order.Domain.Exceptions;
using Order.Domain.Interfaces.Commands.Handlers;
using Order.Domain.Interfaces.Data;
using Order.Domain.Interfaces.Data.Repositories;
using System.Linq;

namespace Order.Domain.Commands.Handlers
{
    public class CreateOrUpdateOrderCommandHandler : ICreateOrUpdateOrderCommandHandler
    {
        private IOrderRepository _orderRepository;
        private IUnitOfWork _unitOfWork;

        public CreateOrUpdateOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public CreateOrUpdateOrderResponse Handle(CreateOrUpdateOrderRequest command)
        {
            if (!command.IsValid())
                throw new InvalidRequestException(command.Errors);

            var order = _orderRepository.Get(command.Number);

            if (order is null)
            {
                order = new Order(command.Number);
                _orderRepository.Add(order);
            }

            foreach (var item in command.Items)
            {
                order.AddOrUpdateItem(item.Description, item.UnitPrice, item.Amount);
            }

            var itemsToRemove = order.Items.Where(
            itemFromRepository =>
                !command.Items.Any(
                    itemFromCommand =>
                        itemFromCommand.Description == itemFromRepository.Description
                ));

            foreach (var itemToRemove in itemsToRemove)
            {
                order.RemoveItem(itemToRemove);
            }

            _unitOfWork.Commit();

            return new CreateOrUpdateOrderResponse(order);
        }
    }
}
