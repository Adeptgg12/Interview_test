using System.Globalization;
using System.Reflection;
using Interview_Test.Infrastructure;
using Interview_Test.Middlewares;
using Interview_Test.Validation;
using Interview_Test.Repositories;
using Interview_Test.Repositories.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using MediatR;


ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddFilter("Microsoft", LogLevel.Information)
        .AddFilter("System", LogLevel.Information)
        .AddFilter("SampleApp.Program", LogLevel.Information)
        .AddConsole();
});

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
/*builder.Services.AddTransient<>()<IUserRepository, UserRepository>();*/
 builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddMvc(options => options.EnableEndpointRouting = false);
// Register MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
// Reguster AutoMapper
builder.Services.AddAutoMapper(typeof(MapperAssembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


//var connection = "Server=localhost\\SQLEXPRESS;Database=InterviewTestDb";
var connection = "Server=localhost\\SQLEXPRESS;Database=InterviewTestDb;User Id=ooven;Password=123;TrustServerCertificate=true;";

builder.Services.AddDbContext<InterviewTestDbContext>(options =>
    {
        options.UseSqlServer(connection,
            sqlOptions =>
            {
                sqlOptions.UseCompatibilityLevel(110);
                sqlOptions.CommandTimeout(30);
                sqlOptions.EnableRetryOnFailure(3, TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
            });
#if DEBUG
        options.EnableSensitiveDataLogging().UseLoggerFactory(loggerFactory);
#endif
    }
);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.UseMiddleware<AuthenMiddleware>();

app.UseFluentValidationHandlerExtension();
app.UseMvc();
app.Run();