using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using GestaoPedidos.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class DynamoDbService<T> where T : Entidade
{
    private readonly IAmazonDynamoDB _dynamoDbClient;
    private readonly DynamoDBContext _context;

    public DynamoDbService(IAmazonDynamoDB dynamoDbClient)
    {
        _dynamoDbClient = dynamoDbClient;
        _context = new DynamoDBContext(_dynamoDbClient);
    }

    public async Task Cadastrar(T entity)
    {
        await _context.SaveAsync(entity);
    }

    public async Task<T> Obter(int id)
    {
        return await _context.LoadAsync<T>(id);
    }

    public async Task Atualizar(T entity)
    {
        await _context.SaveAsync(entity);
    }

    public async Task Remover(int id)
    {
        await _context.DeleteAsync<T>(id);
    }

    public async Task<IEnumerable<T>> Obter()
    {
        var scanConditions = new List<ScanCondition>();
        var result = await _context.ScanAsync<T>(scanConditions).GetRemainingAsync();
        return result;
    }
}
