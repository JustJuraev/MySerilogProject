using Microsoft.EntityFrameworkCore;
using TestMySerilogCheck.Models;
using MySerilogLog;
using MySerilogTestWithBot;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TestMySerilogCheck.Models.ApplicationContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("DataBase"));
});

builder.Services.AddSingleton<MySerilogWithBotClass>(provider =>
{
    var connectionString = builder.Configuration.GetConnectionString("DataBase");
    var chatId = builder.Configuration["TelegramBot:ChatId"];
    var token = builder.Configuration["TelegramBot:Token"];
    return new MySerilogWithBotClass(connectionString, chatId, token);
});

//builder.Services.AddSingleton<TelegramBotService>(provider =>
//{
//    var connectionString = builder.Configuration.GetConnectionString("DataBase");
//    return new TelegramBotService(connectionString);
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
