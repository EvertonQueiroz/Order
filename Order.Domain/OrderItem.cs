namespace Order.Domain
{
    public class OrderItem
    {
        public string Description { get; private set; }
        public decimal UnitPrice { get; set; }
        public decimal Amount { get; set; }

        protected OrderItem() { }
        public OrderItem(string description, decimal unitPrice, decimal amount)
        {
            Description = description;
            UnitPrice = unitPrice;
            Amount = amount;
        }
    }
}
