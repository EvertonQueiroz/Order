using Microsoft.EntityFrameworkCore;
using Order.Domain.Interfaces.Data.Repositories;
using Order.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;

namespace Order.Infra.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbSet<Domain.Order> _orders;

        public OrderRepository(OrderDBContext context)
        {
            _orders = context.Set<Domain.Order>();
        }

        public void Add(Domain.Order order)
        {
            _orders.Add(order);
        }

        public Domain.Order Get(string number)
        {
            return _orders
                .Include(order => order.Items)
                .FirstOrDefault(order => order.Number == number);
        }

        public IReadOnlyList<Domain.Order> GetAll()
        {
            return _orders.Include(order => order.Items).ToList();
        }

        public void Remove(Domain.Order order)
        {
            _orders.Remove(order);
        }
    }
}
