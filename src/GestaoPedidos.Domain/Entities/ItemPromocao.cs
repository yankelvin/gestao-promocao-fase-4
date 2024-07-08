using Amazon.DynamoDBv2.DataModel;

namespace GestaoPedidos.Domain.Entities
{
    [DynamoDBTable("ItemPromocao")]
    public class ItemPromocao : Entidade
    {
        [DynamoDBProperty]
        public int IdPromocao { get; private set; }

        [DynamoDBProperty]
        public int IdProduto { get; private set; }

        [DynamoDBProperty]
        public decimal Desconto { get; private set; }

        public ItemPromocao(int idPromocao, int idProduto, decimal desconto)
        {
            IdPromocao = idPromocao;
            IdProduto = idProduto;
            Desconto = desconto;
        }
    }
}
