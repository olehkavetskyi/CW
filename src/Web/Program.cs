using Application;
using Web;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddWeb()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var context = services.GetRequiredService<CwContext>();
        var loggerFactory = services.GetRequiredService<ILoggerFactory>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

        await CwContextSeed.InitializeAsync(context, loggerFactory, userManager, roleManager);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while seeding the data: " + ex.Message);
    }
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.WithOrigins(builder.Configuration["Origins"]!).AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();
