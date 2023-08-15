using Api.Services;
using Application;
using Application.Interfaces;
using Infrastructure;
using Infrastructure.Context;
using Presentation;
using Presentation.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddApplicationPart(AssemblyReference.Assembly);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddPresentation(builder.Configuration)
                .AddApplication()
                .AddInfrastructure(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILinkServices, LinkServices>();

var app = builder.Build();

await app.Services.AplicarMigracoes();

app.ConfigureEndpoints();
app.ConfigureSwaggerJson();

app.UseHttpsRedirection();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();


app.Run();
