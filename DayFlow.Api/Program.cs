using MediatR;
using DayFlow.Modules.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var assemblies = new[]
//{
//    IdentityModule.Assembly,
//    OrderModule.Assembly,
//    PaymentModule.Assembly
//};

//builder.Services.AddMediatR(cfg =>
//{
//    cfg.RegisterServicesFromAssemblies(assemblies);
//});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Register Identity module services
var dayflowDbConn = builder.Configuration.GetConnectionString("DayflowDb");
builder.Services.AddIdentityModule(builder.Configuration, dayflowDbConn);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
