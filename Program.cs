
using NhaSachDaiThang_BE_API.Configurations;
using NhaSachDaiThang_BE_API.Services;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Repositories;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddJwtConfiguration(builder.Configuration);

// Add DbContext configuration
builder.Services.AddDbContextConfiguration(builder.Configuration);

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddScoped<EmailService>();
// Add services to the container.

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
