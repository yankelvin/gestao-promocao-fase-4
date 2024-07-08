﻿using Amazon.DynamoDBv2;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;

namespace GestaoPedidos.Infrastructure.Data.Repositories
{
    public class ItemPromocaoRepository : DynamoDbService<ItemPromocao>, IItemPromocaoRepository
    {
        public ItemPromocaoRepository(IAmazonDynamoDB dynamoDbClient) : base(dynamoDbClient)
        {
        }
    }
}
