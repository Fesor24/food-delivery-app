using API.Extensions;
using API.Middleware;
using Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext(builder.Configuration)
    .ConfigureIdentityDbContext()
    .ConfigureRedis(builder.Configuration)
    .AddTokenService()
    .AddGenericRepository()
    .ConfigureAuthentication(builder.Configuration)
    .ConfigureAutoMapper()
    .ConfigureCors()
    .AddShoppingCartRepository()
    .AddOrderService()
    .AddPaystackService(builder.Configuration["Paystack:Secret"])
    .AddApplicationServices();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

await app.ApplyMigrations();

app.UseMiddleware<ExceptionMiddleware>();

app.UseSwagger();

app.UseSwaggerUI();

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
