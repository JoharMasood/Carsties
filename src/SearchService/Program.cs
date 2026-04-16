// using AuctionService;
using Polly;
using System.Net;
using Polly.Extensions.Http;
using SearchService;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddMassTransit(x =>
{
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
{
    h.Username("guest");
    h.Password("guest");
});

        // cfg.ConfigureEndpoints(context);
        cfg.ReceiveEndpoint("search-auction-created", e =>
{
    e.ConfigureConsumer<AuctionCreatedConsumer>(context);
});

        cfg.ReceiveEndpoint("search-auction-updated", e =>
        {
            e.ConfigureConsumer<AuctionUpdatedConsumer>(context);
        });

        cfg.ReceiveEndpoint("search-auction-deleted", e =>
        {
            e.ConfigureConsumer<AuctionDeletedConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization(); ;

app.MapControllers();

app.Lifetime.
ApplicationStarted.Register(async () =>
{
    try
    {
        await DbInitializer.InitDb(app);

    }
    catch (Exception e)
    {
        Console.WriteLine(e);
    }

});


app.Run();

static IAsyncPolicy<HttpResponseMessage> GetPolicy()
   => HttpPolicyExtensions
       .HandleTransientHttpError()
       .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
       .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(3));