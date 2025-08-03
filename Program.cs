using CreditDecision.Data;
using CreditDecision.Repositories;
using CreditDecision.Services;
using LoanManager.Api.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var allowedOrigins = builder.Configuration
                            .GetValue<string>("AllowedOrigins")?
                            .Split(';', StringSplitOptions.RemoveEmptyEntries);

builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowFrontEnd",
        policy =>
        {
            policy.WithOrigins(allowedOrigins ?? Array.Empty<string>())
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CreditDecisionDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<CustomerService>();

builder.Services.AddSingleton<KeyVaultService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowFrontEnd");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
