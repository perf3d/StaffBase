using Microsoft.EntityFrameworkCore;
using StaffBase.Application.Services;
using StaffBase.Application.Utils;
using StaffBase.DataAccess;
using StaffBase.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StaffBaseDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString(nameof(StaffBaseDbContext)));
    });
builder.Services.AddScoped<ICsvProvider, CsvProvider>();
builder.Services.AddScoped<IStaffBaseRepository, StaffBaseRepository>();
builder.Services.AddScoped<IStaffBaseService, StaffBaseService>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
