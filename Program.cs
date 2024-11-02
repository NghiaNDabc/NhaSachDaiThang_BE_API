
using NhaSachDaiThang_BE_API.Configurations;
using NhaSachDaiThang_BE_API.Services;
using NhaSachDaiThang_BE_API.Helper;
using NhaSachDaiThang_BE_API.Services.IServices;
using NhaSachDaiThang_BE_API.Repositories.IRepositories;
using NhaSachDaiThang_BE_API.Repositories;
using NhaSachDaiThang_BE_API.UnitOfWork;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("NhaSachDaiThang", builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddJwtConfiguration(builder.Configuration);

// Add DbContext configuration
builder.Services.AddDbContextConfiguration(builder.Configuration);
//service
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOtpService, OtpService>();
builder.Services.AddScoped< BookService>();
//repository
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//unit of work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//mapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

//helper
builder.Services.AddScoped<JwtHelper>();
builder.Services.AddScoped<EmailHelper>();
builder.Services.AddMemoryCache();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Nha Sach Dai Thang API",
        Version = "v1",
        Description = "API cho ứng dụng quản lý nhà sách",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Nhà phát triển",
            Email = "nhasachdaithang@gmail.com"
            //, // Thay đổi với email thật
            //Url = new Uri("https://example.com") // Thay đổi với URL thật
        }
    });
    c.EnableAnnotations();
});

var app = builder.Build();

app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
    });
}

app.UseHttpsRedirection();

app.UseCors("NhaSachDaiThang");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
