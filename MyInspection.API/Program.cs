//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

//app.Run();

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}


//using Microsoft.EntityFrameworkCore;
//using MyInspection.Infrastructure.Data;
//using MyInspection.Core.Entities;
//using Microsoft.AspNetCore.Identity;
//using MyInspection.Application.DTOs; // Add this using
//using MyInspection.Infrastructure.Services; // Add this using

//var builder = WebApplication.CreateBuilder(args);

//// 1. Get the connection string
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//// 2. Add DbContext to the services container.
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));

//// 3. Add Identity services
//builder.Services.AddIdentityCore<User>()
//    .AddRoles<Role>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();


//// Add other services like Swagger/OpenAPI
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();


//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// This is where we will add our API endpoints later
//app.MapGet("/", () => "Hello, Inspection API!");


//app.Run();

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyInspection.Application.DTOs; // Add this using
using MyInspection.Core.Entities;
using MyInspection.Infrastructure.Data;
using MyInspection.Infrastructure.Services; // Add this using

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration; // Get configuration for easy access

// 1. Get the connection string
var connectionString = config.GetConnectionString("DefaultConnection");

// 2. Add DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// 3. Add Identity services (using AddIdentity instead of AddIdentityCore for more features)
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 4. Add Authentication with JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
    };
});

// 5. Add our custom services
builder.Services.AddScoped<TokenService>();


// Add other services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// IMPORTANT: Add these two lines for authentication/authorization
app.UseAuthentication();
app.UseAuthorization();


// ====== API ENDPOINTS ======
var authGroup = app.MapGroup("/api/auth");

authGroup.MapPost("/register", async (UserManager<User> userManager, RegisterDto registerDto) =>
{
    var user = new User
    {
        UserName = registerDto.Username,
        Email = registerDto.Email,
        FullName = registerDto.FullName
    };

    var result = await userManager.CreateAsync(user, registerDto.Password);

    if (!result.Succeeded)
    {
        return Results.BadRequest(result.Errors);
    }

    // Optionally assign a default role
    // await userManager.AddToRoleAsync(user, "Inspector");

    return Results.Ok(new { Message = "User registered successfully!" });
});

authGroup.MapPost("/login", async (UserManager<User> userManager, TokenService tokenService, LoginDto loginDto) =>
{
    var user = await userManager.FindByNameAsync(loginDto.Username);

    if (user == null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
    {
        return Results.Unauthorized();
    }

    return Results.Ok(new UserDto
    {
        Username = user.UserName,
        Token = tokenService.CreateToken(user)
    });
});


app.Run();

// Seed the database with roles (run this once on startup)
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    var roles = new[] { "Admin", "Inspector" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new Role { Name = role });
        }
    }
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during database seeding.");
}