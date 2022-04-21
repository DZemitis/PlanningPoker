using ScrumPoker.Core.Services;
using ScrumPoker.Data.Data;
using ScrumPoker.DataBase.Interfaces;
using ScrumPoker.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IGameRoomService, GameRoomService>();
builder.Services.AddTransient<IPlayerService, PlayerService>();
builder.Services.AddTransient<IVotingResultService, VotingResultService>();
builder.Services.AddTransient<IGameRoomRepository, GameRoomRepository>();

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