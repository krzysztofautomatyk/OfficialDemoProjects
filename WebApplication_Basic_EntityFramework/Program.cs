using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
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
        FullName = "User1",
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
        FullName = "User2",
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

app.MapPost("update", async (MyBoardsContext db) =>
{
    Epic epic = await db.Epic.FirstAsync(epic => epic.Id == 1);

    epic.Area ="Update date area";
    epic.Priority = 1;
    epic.StartDate = DateTime.Now;

    await db.SaveChangesAsync();

    return epic;

});


app.MapGet("dataAync", async (MyBoardsContext db) =>
{
    var newComments = await db
        .Comments
        .Where(x => x.CreatedDate > new DateTime(2022,05,05))
        .ToListAsync();

    var top5NewestComments = await db
    .Comments
    .OrderByDescending(x => x.CreatedDate)
    .Take(5)
    .ToListAsync();

    var satesCount = await db.WorkItems
        .GroupBy(x => x.StateId)
        .Select(x => new { stateId = x.Key, count = x.Count() })
        .ToListAsync();

    var epicListOnHold = await db.Epic
        .Where(x => x.StateId == 4)
        .OrderBy(x => x.Priority)
        .Select(c => new { c.StartDate, c.EndDate,c.Id, c.State,c.StateId,c.Area,c.IterationPath,c.Priority })
        .ToListAsync();

    var authorsCommentCounts =await db.Comments
        .GroupBy(x => x.AuthorId)
        .Select(x => new { x.Key, count = x.Count() })
        .ToListAsync();
    var topAuthor = authorsCommentCounts
        .First(x => x.count == authorsCommentCounts.Max(x => x.count));
    var userDetails = db.Users
    .Select(x => new { x.Id, x.FullName, x.Email })
    .First(x => x.Id == topAuthor.Key);
        

    return new { commentCount = topAuthor.count, userDetails };

});


app.MapGet("data", (MyBoardsContext db) =>
{
    var tags = db.Tags.ToList();

    var epic = db.Epic.First();

    var user = db.Users.FirstOrDefault(x => x.FullName == "User One");

    var toDoWorkItem = db.WorkItems.Where(x=> x.StateId == 1).ToList();

    return new { toDoWorkItem };

    //return new { epic, user };
});



app.Run();

 