namespace Order.API.Requests
{
    public class MudarStatusRequest
    {
        public string Status { get; set; }
        public decimal ItensAprovados { get; set; }
        public decimal ValorAprovado { get; set; }
        public string Pedido { get; set; }
    }
}
