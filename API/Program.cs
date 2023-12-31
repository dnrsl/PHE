using API.Contracts;
using API.Data;
using API.Repositories;
using API.Services;
using API.Utilities.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection;
using System.Text;
using TokenHandler = API.Utilities.Handlers.TokenHandler;


var builder = WebApplication.CreateBuilder(args);

// Add DbContext to the container
var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<PheDbContext>(options => options.UseSqlServer(connection));

// Add repositories to the container
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<ICharacteristicRepository, CharacteristicRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserProjectRepository, UserProjectRepository>();

// Add services to the container.
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<AccountService>();
builder.Services.AddScoped<AccountRoleService>();
builder.Services.AddScoped<CharacteristicService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<UserProjectService>();

//Register FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//Register TokenHandler
builder.Services.AddScoped<ITokenHandler, TokenHandler>();

builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = _context =>
                    {
                        var errors = _context.ModelState.Values
                                 .SelectMany(v => v.Errors)
                                 .Select(v => v.ErrorMessage);

                        return new BadRequestObjectResult(new ResponseValidationHandler
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Status = HttpStatusCode.BadRequest.ToString(),
                            Message = "Validation Error",
                            Errors = errors.ToArray()
                        });
                    };
                });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => {
    x.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Metrodata Coding Camp",
        Description = "ASP.NET Core API 6.0"
    });
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

//CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

// Jwt Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer(options =>
       {
           options.RequireHttpsMetadata = false;
           options.SaveToken = true;
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWTConfig:SecretKey"])),
               ValidateIssuer = false,
               //Usually, this is your application base URL
               ValidIssuer = builder.Configuration["JWTConfig:Issuer"],
               ValidateAudience = false,
               //If the JWT is created using a web service, then this would be the consumer URL.
               ValidAudience = builder.Configuration["JWTConfig:Audience"],
               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero
           };
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
