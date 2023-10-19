using System.Configuration;
using System.Net;
using ASPCoreWebAPICRUD.Filters;
using ASPCoreWebAPICRUD.Middlewares;
using ASPCoreWebAPICRUD.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
builder.Services.AddControllers(cfg =>
    cfg.Filters.Add( typeof(ExceptionFilter)
)) ; 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var provider = builder.Services.BuildServiceProvider();

var config = provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<MyDBContext>(
        item => item.UseNpgsql(config.GetConnectionString("dbcs")));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseMiddleware<ExceptionMiddleware>();


/*
app.UseExceptionHandler(
    options =>
    {
        options.Run(
            async context =>
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var ex = context.Features.Get<IExceptionHandlerFeature>();
                if (ex != null)
                {
                    await context.Response.WriteAsync
                    (
                        "Error - " + ex.Error.Message 
                    );
                }
            }
            );
    }
    );
*/


app.UseAuthorization();

app.MapControllers();

app.Run();

