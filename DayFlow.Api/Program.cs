using DayFlow.Api.Configuration;
using DayFlow.Api.Diagnostics;
using DayFlow.Modules.Identity;
using MediatR;

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

//builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();


// Register Identity module services
var dayflowDbConn = builder.Configuration.GetConnectionString("DayflowDb");

builder.Services.AddSwaggerConfiguration();

//builder.Services.AddAuthenticationConfiguration(builder.Configuration);

//builder.Services.AddAuthorizationConfiguration();

//builder.Services.AddCorsConfiguration(builder.Configuration);

builder.Services.AddProblemDetails();

builder.Services.AddModuleConfiguration(builder.Configuration, dayflowDbConn);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//}

app.UseHttpsRedirection();

app.UseSwaggerConfiguration(app.Environment.IsDevelopment());

//app.UseAuthentication();

//app.UseAuthorization();

//app.MapControllers();
app.MapIdentityEndpoints();

//app.LogEndpoints();

app.Run();
