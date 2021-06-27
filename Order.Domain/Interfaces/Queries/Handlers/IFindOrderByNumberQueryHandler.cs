using Order.Domain.Queries.Requests;
using Order.Domain.Queries.Responses;

namespace Order.Domain.Interfaces.Queries.Handlers
{
    public interface IFindOrderByNumberQueryHandler
    {
        FindOrderByNumberResponse Handle(FindOrderByNumberRequest command);
    }
}
