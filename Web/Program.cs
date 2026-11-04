using System.Text;
using System.Text.Json.Serialization;
using Application;
using Application.BusinessLogic.Access.Services;
using Application.Contracts;
using Application.Pipelines;
using Domain.Entities;
using Infrastructure.Auth;
using Infrastructure.Authentication;
using Infrastructure.Cache;
using Infrastructure.DataBase.Options;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Web.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "issuer",
            ValidateAudience = true,
            ValidAudience = "audience",
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dksbvdsbvdisavbdosavdsbhvjlsdbvadbvlajdsbvdvbdsaivudavb"))
        };

        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                string token = context.HttpContext.Request.Cookies["accessToken"] ?? "";
                context.Token = token;
                return Task.CompletedTask;
            }
        };
    }
);

builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Database=GameTournamentsHandlerDb;Username=admin;Password=admin;", opt => opt.EnableRetryOnFailure());
});
builder.Services.AddStackExchangeRedisCache(opt => opt.Configuration = "localhost:6379");
builder.Services.AddScoped<ICacheProvider, CacheProvider>();
builder.Services.AddScoped<IAppDbContext>(serviceProvider => serviceProvider.GetRequiredService<AppDbContext>());
builder.Services.AddScoped<IAuthTokensHandler, JwtTokensHandler>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

//Access services
builder.Services.AddScoped<IAccessService<Match>, MatchAccessService>();
builder.Services.AddScoped<IAccessService<RoleUpgradeRequest>, RoleUpgradeRequestsAccessService>();
builder.Services.AddScoped<IAccessService<Team>, TeamAccessService>();
builder.Services.AddScoped<IAccessService<TeamMember>, TeamMemberAccessService>();
builder.Services.AddScoped<IAccessService<Tournament>, TournamentAccessService>();
builder.Services.AddScoped<IAccessService<TournamentMember>, TournamentMemberAccessService>();
//

builder.Services.AddMediatR(cfg => {
    cfg.RegisterServicesFromAssemblyContaining<IApplicationMarker>();
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CacheBehavior<,>));
    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(CacheInvalidateBehavior<,>));
});

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();