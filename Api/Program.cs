using Application.Configuration;
using Infrastructure.Configuration;
using Presentation.Configuration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddApplicationPart(Presentation.Configuration.AssemblyReference.Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureApiVersion();

builder.Services.AddAplication()
                .AddInfrastructure(builder.Configuration, builder.Environment);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(Log.Logger);


var app = builder.Build();

app.ConfigureEndpoints();


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
