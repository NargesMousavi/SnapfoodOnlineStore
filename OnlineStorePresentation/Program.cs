using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<OnlineStoreContext>(options =>
{
    options.UseSqlServer(@"Data Source=localhost;Initial Catalog=OnlineStore;Persist Security Info=True;User ID=narges;Password=1!@#345;Encrypt=False");
});
builder.Services.AddMemoryCache();

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<ProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();
//swagger address: /swagger/index.html
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
