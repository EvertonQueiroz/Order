using Order.Domain.Interfaces.Data.Repositories;
using Order.Domain.Interfaces.Queries.Handlers;
using Order.Domain.Queries.Requests;
using Order.Domain.Queries.Responses;

namespace Order.Domain.Queries.Handlers
{
    public class FindAllOrdersQueryHandler : IFindAllOrdersQueryHandler
    {
        private readonly IOrderRepository _orderRepository;

        public FindAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public FindAllOrdersResponse Handle(FindAllOrdersRequest query)
        {
            var orders = _orderRepository.GetAll();

            return new FindAllOrdersResponse(orders);
        }
    }
}
