using GestaoPedidos.Application.Services;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Interfaces.Services;
using GestaoPedidos.Infrastructure.Data.Repositories;
using System.Diagnostics.CodeAnalysis;

namespace GestaoPedidos.Web.DependencyInjections
{
    [ExcludeFromCodeCoverage]
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPromocaoRepository, PromocaoRepository>();
            services.AddTransient<IItemPromocaoRepository, ItemPromocaoRepository>();
            services.AddTransient<IHistoricoUsoPromocaoRepository, HistoricoUsoPromocaoRepository>();

            services.AddTransient<IPromocaoService, PromocaoService>();
            services.AddTransient<IItemPromocaoService, ItemPromocaoService>();
            services.AddTransient<IHistoricoUsoPromocaoService, HistoricoUsoPromocaoService>();
        }
    }
}
