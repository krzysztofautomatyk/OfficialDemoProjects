using WebApplication_Basic_DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<IOperationTransient, Operation>();
builder.Services.AddScoped<IOperationScoped, Operation>();
builder.Services.AddSingleton<IOperationSingleton, Operation>();
builder.Services.AddTransient<OperationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();

