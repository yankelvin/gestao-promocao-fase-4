using Amazon.DynamoDBv2;
using GestaoPedidos.Infrastructure.Data.Repositories;
using GestaoPedidos.Web;
using GestaoPedidos.Web.DependencyInjections;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
builder.Services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));
AppSettings appSettings = new();
configuration.GetSection(nameof(AppSettings)).Bind(appSettings);

builder.Services.AddControllers(options => { });
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);
builder.Services.AddAWSService<IAmazonDynamoDB>();
builder.Services.AddScoped(typeof(DynamoDbService<>));
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddServices();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new()
    {
        Title = "Swagger Gestão de Promoções WEB API",
        Description = "Aplicação para gestão de promoções do restaurante"
    });
    var filePath = Path.Combine(System.AppContext.BaseDirectory, "GestaoPedidos.xml");
    c.IncludeXmlComments(filePath);
});

WebApplication app = builder.Build();

app.MapHealthChecks("/healthz");
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    _ = endpoints.MapControllers();
});

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

    options.RoutePrefix = "/swagger";
});

app.Run();
