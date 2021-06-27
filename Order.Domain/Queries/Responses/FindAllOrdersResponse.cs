namespace Order.Domain.Queries.Responses
{
    public class FindAllOrdersResponse
    {
        public System.Collections.Generic.IReadOnlyList<Order> Orders { get; private set; }

        public FindAllOrdersResponse(System.Collections.Generic.IReadOnlyList<Order> orders)
        {
            Orders = orders;
        }
    }
}
