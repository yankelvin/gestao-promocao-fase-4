namespace GestaoPedidos.Application.DTOs.Pedido;

public class CadastroPedidoDto
{
    public int IdCliente { get; set; }
    public IEnumerable<int> Produtos { get; set; }
}