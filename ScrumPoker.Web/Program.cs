using FluentValidation.AspNetCore;
using ScrumPoker.Infrastructure;
using ScrumPoker.Infrastructure.AutoMapper;
using ScrumPoker.Infrastructure.Middlewares;
using ScrumPoker.Web.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Register();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers(options =>
    {
        options.Filters.Add<HttpResponseExceptionFilter>();
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = ValidationFilter.Process;
    })
    .AddXmlSerializerFormatters();

builder.Services
    .AddMvc()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateGameRoomApiRequestValidator>();
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