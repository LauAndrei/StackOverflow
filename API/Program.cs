using API.Extensions.AppExtensions;
using Core.Entities;
using Core.Interfaces.ServiceInterfaces;
using Infrastructure.Repositories;
using Infrastructure.Repositories.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddApplicationServicesAndRepositories(builder.Configuration);

builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
    });
});

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
app.UseCors("CorsPolicy");
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = services.GetRequiredService<DatabaseContext>();
var logger = services.GetRequiredService<ILogger<Program>>();
var userManager = services.GetRequiredService<UserManager<User>>();
var tagManager = services.GetRequiredService<ITagService>();
var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

try
{
    await context.Database.MigrateAsync();
    await UserSeed.SeedUsersAsync(userManager);
    await TagSeed.SeedTagsAsync(tagManager);
    await RoleSeed.SeedRolesAsync(roleManager);

}

catch (Exception ex)
{
    logger.LogError(ex, "Error occured during migration");
}

app.Run();
