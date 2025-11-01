using Microsoft.EntityFrameworkCore;
using LearnApiNetCore.Entity;
using Microsoft.EntityFrameworkCore.SqlServer;
using LearnAspNetCore.Services;
using LearnAspNetCore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection(SmtpSettings.SettingName));
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddHostedService<MyHostedService>();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

