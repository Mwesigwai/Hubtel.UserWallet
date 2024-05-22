using Hubtel.UserWallet.Api.Data;
using Hubtel.UserWallet.Api.ReusableMethods;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IWalletService, WalletService>();
builder.Services.AddDbContext<DataContext>(
    o => o.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemoryDb")!));
builder.Services.AddSingleton<IHelperMethods, HelperMethods>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var filePath = Path.Combine(AppContext.BaseDirectory, "Hubtel.UserWallet.Api.xml");
    c.IncludeXmlComments(filePath);
});

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
