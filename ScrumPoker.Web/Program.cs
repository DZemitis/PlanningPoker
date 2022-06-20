using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ScrumPoker.DataAccess.Models.EFContext;
using ScrumPoker.Infrastructure;
using ScrumPoker.Infrastructure.AutoMapper;
using ScrumPoker.Infrastructure.Configuration;
using ScrumPoker.Infrastructure.Middlewares;
using ScrumPoker.Web.Validators;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithThreadId()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


builder.Services.AddControllers();
builder.Services.AddDbContext<ScrumPokerContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle 
builder.Services.AddEndpointsApiExplorer();
builder.ConfigureSwagger();

builder.Services.Register();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddControllers(options => { options.Filters.Add<HttpResponseExceptionFilter>(); })
    .ConfigureApiBehaviorOptions(options => { options.InvalidModelStateResponseFactory = ValidationFilter.Process; })
    .AddXmlSerializerFormatters();

builder.Services
    .AddMvc()
    .AddFluentValidation(fv => { fv.RegisterValidatorsFromAssemblyContaining<CreateGameRoomApiRequestValidator>(); });

builder.AddJwtAuthentication();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();