using BookStore.Bot.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBotCore(builder.Configuration);

var app = builder.Build();

app.Run();
