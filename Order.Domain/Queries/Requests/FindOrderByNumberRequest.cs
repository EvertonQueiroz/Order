namespace Order.Domain.Queries.Requests
{
    public class FindOrderByNumberRequest
    {
        public string Number { get; private set; }

        public FindOrderByNumberRequest(string number)
        {
            Number = number;
        }
    }
}
