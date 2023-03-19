using System.Text;
using Core.Entities;
using Core.Interfaces.ServiceInterfaces;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions.AppExtensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServicesAndRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<TagRepository,TagRepository>();
        services.AddScoped<ITagService, TagService>();

        services.AddScoped<ITokenService, TokenService>();
        
        services.AddScoped<QuestionRepository, QuestionRepository>();
        services.AddScoped<QuestionTagRepository, QuestionTagRepository>();
        services.AddScoped<IQuestionService, QuestionService>();

        services.AddScoped<AnswerRepository, AnswerRepository>();
        services.AddScoped<IAnswerService, AnswerService>();

        services.AddIdentity<User, IdentityRole<int>>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<DatabaseContext>() //this creates the necessary tables to store the users
            .AddSignInManager<SignInManager<User>>();

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // here we specify what we're gonna validate against;
                    // we need to validate against the issuer signing key because otherwise
                    // any jwt token would be accepted
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"])),
                    ValidIssuer = configuration["Token:Issuer"],
                    ValidateIssuer = true,
                    ValidateAudience = false
                };
            });
        
        
        services.AddAuthorization();
            
        
        return services;
    }
}