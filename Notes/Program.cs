using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Notes.Domain.Models;
using Notes.Infrastructure.ApplicationContext;
using Notes.Infrastucture.Interfaces;
using Notes.Infrastucture.Repositories;
using Notes.Interfaces;
using Notes.Services.Account;
using Notes.Services.Notes;
using Notes.Services.Users;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddLogging();
builder.Services.AddCors();
builder.Services.AddDbContext<EFContext>(options => options.UseSqlServer(connection));
builder.Services.AddAuthorization();

builder.Services.AddScoped<IAsyncRepository<Note>, NotesRepository>();
builder.Services.AddScoped<IAsyncRepository<User>, UsersRepository>();

builder.Services.AddScoped<INoteService, NotesService>();
builder.Services.AddScoped<IUserService, UsersService>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration.GetSection("Authorization:Issuer").Value,
            ValidAudience = builder.Configuration.GetSection("Authorization:Audience").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Authorization:SecretKey").Value)),
            LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters) =>
                 (expires != null) ? DateTime.UtcNow < expires : false
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.UseHttpsRedirection();

app.MapControllers();
app.Run();
