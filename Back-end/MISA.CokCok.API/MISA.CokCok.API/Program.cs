﻿using MISA.CokCok.Core;
using MISA.CokCok.Core.Interfaces.IRepositories;
using MISA.CokCok.Core.Interfaces.IServices;
using MISA.CokCok.Core.Services;
using MISA.CokCok.Infrastructure.Interfaces;
using MISA.CokCok.Infrastructure.MisaDatabaseContext;
using MISA.CokCok.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://127.0.0.1:5500") // Thay đổi thành URL của bạn
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Add services to the container.
Common.ConnectionString = builder.Configuration.GetConnectionString("Database1");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// config DI
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IMisaDBContext, MySqlDBContext>();

builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Sử dụng middleware CORS
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
