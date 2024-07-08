using AutoMapper;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Enums;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Infrastructure.Data.Contexts;
using GestaoPedidos.Infrastructure.Data.Entities.ItensPedido;
using GestaoPedidos.Infrastructure.Data.Entities.Pedidos;

namespace GestaoPedidos.Infrastructure.Data.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly IMapper _mapper;
    private readonly PedidoContext _pedidoContext;

    public PedidoRepository(IMapper mapper, PedidoContext pedidoContext)
    {
        _mapper = mapper;
        _pedidoContext = pedidoContext;
    }

    public void Inserir(Pedido pedido, IEnumerable<int> produtoList)
    {
        var pedidoEntity = _mapper.Map<PedidoEntity>(pedido);

        var pedidoSaved = _pedidoContext.Pedidos.Add(pedidoEntity);

        IEnumerable<ItensPedidoEntity> itensPedidos = produtoList.Select(MapItensPedido(pedidoSaved.Entity));

        _pedidoContext.ItensPedidos.AddRange(itensPedidos);
    }

    private static Func<int, ItensPedidoEntity> MapItensPedido(PedidoEntity pedido)
    {
        return idProduto => new ItensPedidoEntity()
        {
            Pedido = pedido,
            IdProduto = idProduto
        };
    }

    public PedidoEntity? ObterSemMapear(int id) => _pedidoContext.Pedidos.Find(id);

    public Pedido Obter(int id)
    {
        var entity = _pedidoContext.Pedidos.Find(id);

        return _mapper.Map<Pedido>(entity);
    }

    public IEnumerable<Pedido> ObterTodosPedidos(int? idCliente, Status? statusPedido)
    {
        var query = _pedidoContext
            .Pedidos
            .AsQueryable();

        if (idCliente is not null)
            query = query.Where(p => p.IdCliente.Equals(idCliente));

        if (statusPedido is not null)
            query = query.Where(p => p.Status.Equals(statusPedido));

        query = query
                .OrderBy(p => p.Status == Status.Recebido)
                .ThenBy(p => p.Status == Status.Solicitado)
                .ThenBy(p => p.Status == Status.EmPreparo)
                .ThenBy(p => p.Status == Status.Pronto)
                .ThenBy(p => p.HorarioInicio);

        query = query.Where(p => p.Status != Status.Finalizado);
            
        var result = query.ToList();

        return _mapper.Map<IEnumerable<Pedido>>(result);
    }

    public bool Deletar(int id)
    {
        var entity = ObterSemMapear(id);

        if (entity is null)
            return false;

        _pedidoContext.Pedidos.Remove(entity);
        _pedidoContext.SaveChanges();
        return true;
    }

    public bool Atualizar(Pedido pedido)
    {
        var pedidoAnterior = ObterSemMapear(pedido.Id);

        if (pedidoAnterior is null)
            return false;

        pedidoAnterior.IdCliente = pedido.IdCliente;
        pedidoAnterior.Status = pedido.Status;
        pedidoAnterior.HorarioInicio = pedido.HorarioInicio;
        pedidoAnterior.HorarioFim = pedido.HorarioFim;
        pedidoAnterior.ValorPedido = pedido.ValorPedido;

        _pedidoContext.Pedidos.Update(pedidoAnterior);
        _pedidoContext.SaveChanges();
        return true;
    }

    public IEnumerable<Pedido> ObterPorCliente(int? idCliente)
    {
        var entity = _pedidoContext
            .Pedidos
            .Where(p => p.IdCliente.Equals(idCliente))
            .ToList();

        return _mapper.Map<IEnumerable<Pedido>>(entity);
    }

    public void SaveChanges()
    {
        _pedidoContext.SaveChanges();
    }
}