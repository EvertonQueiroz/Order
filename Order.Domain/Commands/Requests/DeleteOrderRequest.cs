namespace Order.Domain.Commands.Requests
{
    public class DeleteOrderRequest
    {
        public string Number { get; private set; }

        public DeleteOrderRequest(string number)
        {
            Number = number;
        }
    }
}
