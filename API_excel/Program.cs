using API_excel.CsvService;
using API_excel.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//getting the connection string from the configuration file
string connection = builder.Configuration.GetConnectionString("DefaultConnection");

//adding the ApplicationContext context as a service to the application
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICsvService, CsvService>();

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