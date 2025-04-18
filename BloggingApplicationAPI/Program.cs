using BloggingApplicationAPI.Data;
using BloggingApplicationAPI.Services.IServices;
using BloggingApplicationAPI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options.UseSqlServer(builder.Configuration.GetConnectionString("constr")));


// Add services to the container.
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IUserService, UserService>();



builder.Services.AddControllers();

builder.Services.AddCors(options =>

{

    options.AddPolicy("AllowFrontendOrigin", builder =>

    {

        builder.WithOrigins("https://localhost:7179")

               .AllowAnyMethod()

               .AllowAnyHeader();

    });

});

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
app.UseCors("AllowFrontendOrigin");


app.UseAuthorization();

app.MapControllers();

app.Run();
