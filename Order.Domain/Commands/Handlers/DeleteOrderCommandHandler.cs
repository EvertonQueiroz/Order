using Order.Domain.Commands.Requests;
using Order.Domain.Exceptions;
using Order.Domain.Interfaces.Commands.Handlers;
using Order.Domain.Interfaces.Data;
using Order.Domain.Interfaces.Data.Repositories;

namespace Order.Domain.Commands.Handlers
{
    public class DeleteOrderCommandHandler : IDeleteOrderCommandHandler
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteOrderCommandHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
        }

        public void Handle(DeleteOrderRequest command)
        {
            var order = _orderRepository.Get(command.Number);

            if (order is null)
                throw new OrderNotFoundException(command.Number);

            _orderRepository.Remove(order);

            _unitOfWork.Commit();
        }
    }
}
