using System.Data;
using Microsoft.Data.SqlClient;
using NilveraTestProject.CQRS.Customers.Queries;
using NilveraTestProject.Data.Repository.Abstract;
using NilveraTestProject.Data.Repository.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllCustomersQuery>());

builder.Services.AddScoped<IDbConnection>(x =>
    new SqlConnection(builder.Configuration.GetConnectionString("SqlServer")));


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
