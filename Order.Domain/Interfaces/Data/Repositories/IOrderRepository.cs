using System.Collections.Generic;

namespace Order.Domain.Interfaces.Data.Repositories
{
    public interface IOrderRepository
    {
        IReadOnlyList<Order> GetAll();
        Order Get(string number);
        void Add(Order order);
        void Remove(Order order);
    }
}
