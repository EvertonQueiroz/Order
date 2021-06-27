using Order.Domain.Exceptions;
using Order.Domain.Interfaces.Data.Repositories;
using Order.Domain.Interfaces.Queries.Handlers;
using Order.Domain.Queries.Requests;
using Order.Domain.Queries.Responses;

namespace Order.Domain.Queries.Handlers
{
    public class FindOrderByNumberQueryHandler : IFindOrderByNumberQueryHandler
    {
        private readonly IOrderRepository _orderRepository;

        public FindOrderByNumberQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public FindOrderByNumberResponse Handle(FindOrderByNumberRequest command)
        {
            var order = _orderRepository.Get(command.Number);

            if (order is null)
                throw new OrderNotFoundException(command.Number);

            return (FindOrderByNumberResponse) order;
        }
    }
}
