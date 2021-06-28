namespace Order.API.Requests
{
    public class PedidoItemRequest
    {
        public string Descricao { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Qtd { get; set; }
    }
}
