using EmployeeManagementSystem.Common;
using EmployeeManagementSystem.CosmosDb;
using EmployeeManagementSystem.Interface;
using EmployeeManagementSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmployeeService, EmployeeBasicDetailsService>();
builder.Services.AddScoped<IEmployeebasicDetailsCosmosDbService, EmployeeBasicDetailsCosmosDbService>();

builder.Services.AddScoped<IAdditionInfo_Serivice, AdditionalInfo_Service>();
builder.Services.AddScoped<IAddiitonalInfo_CosmosDb_Service, Additonalnfo_CosmosDb_Service>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
//builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


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
