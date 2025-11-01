using Microsoft.EntityFrameworkCore;
using LearnApiNetCore.Entity;
using Microsoft.EntityFrameworkCore.SqlServer;
using LearnApiNetCore.Services;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

//Register services
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

