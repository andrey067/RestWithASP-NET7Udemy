using Api.Services;
using Application;
using Application.Interfaces;
using Infrastructure;
using Presentation;
using Presentation.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddApplicationPart(AssemblyReference.Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPresentation(builder.Configuration)
                .AddApplication()
                .AddInfrastructure(builder.Configuration);


builder.Services.AddScoped<ILinkServices, LinkServices>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

await app.Services.AplicarMigracoes();



app.ConfigureEndpoints();
app.ConfigureSwagger();


app.UseHttpsRedirection();
app.MapControllers();


app.Run();
