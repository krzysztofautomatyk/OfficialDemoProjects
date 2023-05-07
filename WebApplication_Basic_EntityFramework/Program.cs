using Microsoft.EntityFrameworkCore;
using WebApplication_Basic_EntityFramework.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<MyBoardsContext>(
    option => option.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Impletentation migration
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<MyBoardsContext>();

var pendingMigration = dbContext.Database.GetPendingMigrations();
if(pendingMigration.Any())
{
    dbContext.Database.Migrate();
}   

//seed data
var user = dbContext.Users.ToList();
if (!user.Any())
{
    var user1 = new User
    {
        Email = "user1@test.com",
        Name = "User1",
        Password = "123456",
        Role = "User",


        Address = new Address()
        {
            City = "Warszwa",
            Street = "Szeroka"
        }

    };
    var user2 = new User
    {
        Email = "user2@test.com",
        Name = "User2",
        Password = "123456",
        Role = "User",


        Address = new Address()
        {
            City = "Poznañ",
            Street = "Nowa"
        }

    };

    dbContext.Users.AddRange(user1, user2);
    dbContext.SaveChanges();               
}

app.Run();

 