using FluentValidation.AspNetCore;
using ScrumPoker.Infrastructure;
using ScrumPoker.Infrastructure.AutoMapper;
using ScrumPoker.Web.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Register();

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services
    .AddMvc()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateGameRoomApiRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<UpdateGameRoomApiRequestValidator>();
    });

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