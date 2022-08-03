using Basket.API.GrpcService;
using Basket.API.Repositories;
using Discount.Grpc.Protos;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connection=builder.Configuration["CacheSettings:ConnectionString"]; 
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = connection; 
});

builder.Services.AddControllers();
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    o =>
    {
        o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
    });
  builder.Services.AddScoped<DiscountGrpcService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
