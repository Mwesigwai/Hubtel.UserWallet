using Hubtel.UserWallet.Api.Data;
using Hubtel.UserWallet.Api.ReturnTypes;
using Hubtel.UserWallet.Api.ReusableMethods;
using Hubtel.UserWallet.Api.WalletServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory,xmlFile);
builder.Services.AddSwaggerGen(c=>
{
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddSingleton<IHelperMethods, HelperMethods>();
builder.Services.AddSingleton<IServiceResponseFactory<IWalletServiceResponse>, ServiceResponseFactory>();
builder.Services.AddDbContext<DataContext>(
    o => o.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemoryDb")!));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
