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

//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models;
//using MyInspection.Application.DTOs; // Add this using
//using MyInspection.Core.Entities;
//using MyInspection.Infrastructure.Data;
//using MyInspection.Infrastructure.Services; // Add this using
//using System.Text;

//var builder = WebApplication.CreateBuilder(args);
//var config = builder.Configuration; // Get configuration for easy access

//// 1. Get the connection string
//var connectionString = config.GetConnectionString("DefaultConnection");

//// 2. Add DbContext
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));

//// 3. Add Identity services (using AddIdentity instead of AddIdentityCore for more features)
//builder.Services.AddIdentity<User, Role>(options =>
//{
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//})
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

//// 4. Add Authentication with JWT Bearer
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = config["Jwt:Issuer"],
//        ValidAudience = config["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]))
//    };
//});

//// 5. Add our custom services
//builder.Services.AddScoped<TokenService>();


//// Add other services
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

//// IMPORTANT: Add these two lines for authentication/authorization
//app.UseAuthentication();
//app.UseAuthorization();


//// ====== API ENDPOINTS ======
//var authGroup = app.MapGroup("/api/auth");

//authGroup.MapPost("/register", async (UserManager<User> userManager, RegisterDto registerDto) =>
//{
//    var user = new User
//    {
//        UserName = registerDto.Username,
//        Email = registerDto.Email,
//        FullName = registerDto.FullName
//    };

//    var result = await userManager.CreateAsync(user, registerDto.Password);

//    if (!result.Succeeded)
//    {
//        return Results.BadRequest(result.Errors);
//    }

//    // Optionally assign a default role
//    // await userManager.AddToRoleAsync(user, "Inspector");

//    return Results.Ok(new { Message = "User registered successfully!" });
//});

//authGroup.MapPost("/login", async (UserManager<User> userManager, TokenService tokenService, LoginDto loginDto) =>
//{
//    var user = await userManager.FindByNameAsync(loginDto.Username);

//    if (user == null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
//    {
//        return Results.Unauthorized();
//    }

//    return Results.Ok(new UserDto
//    {
//        Username = user.UserName,
//        Token = tokenService.CreateToken(user)
//    });
//});


//app.Run();

//// Seed the database with roles (run this once on startup)
//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;
//try
//{
//    var roleManager = services.GetRequiredService<RoleManager<Role>>();
//    var roles = new[] { "Admin", "Inspector" };
//    foreach (var role in roles)
//    {
//        if (!await roleManager.RoleExistsAsync(role))
//        {
//            await roleManager.CreateAsync(new Role { Name = role });
//        }
//    }
//}
//catch (Exception ex)
//{
//    var logger = services.GetRequiredService<ILogger<Program>>();
//    logger.LogError(ex, "An error occurred during database seeding.");
//}




//using System.Text;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.IdentityModel.Tokens;
//using Microsoft.OpenApi.Models; // Add this for Swagger security definitions
//using MyInspection.Application.DTOs;
//using MyInspection.Core.Entities;
//using MyInspection.Infrastructure.Data;
//using MyInspection.Infrastructure.Services;
//using MyInspection.Application.Interfaces;
//using System.Security.Claims;
//using Microsoft.AspNetCore.Authorization;

//var builder = WebApplication.CreateBuilder(args);
//var config = builder.Configuration; // Get configuration for easy access

//// -----------------------------------------------------------------------------
//// 1. CONFIGURE SERVICES in the DEPENDENCY INJECTION CONTAINER
//// -----------------------------------------------------------------------------

//// Get the connection string from appsettings.json
//var connectionString = config.GetConnectionString("DefaultConnection");

//// Add DbContext service for Entity Framework Core
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString));

////// Add Identity services for user and role management
////builder.Services.AddIdentity<User, Role>(options =>
////{
////    // Configure password requirements for security
////    options.Password.RequireDigit = true;
////    options.Password.RequireLowercase = true;
////    options.Password.RequireUppercase = true;
////    options.Password.RequiredLength = 6;
////})
////    .AddEntityFrameworkStores<ApplicationDbContext>()
////    .AddDefaultTokenProviders();

//// Add Authentication services and configure JWT Bearer as the default scheme
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = config["Jwt:Issuer"],
//        ValidAudience = config["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured.")))
//    };
//});

//// Add Identity services for user and role management
//builder.Services.AddIdentity<User, Role>(options =>
//{
//    // Configure password requirements for security
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//})
//    .AddEntityFrameworkStores<ApplicationDbContext>()
//    .AddDefaultTokenProviders();

//// Add Authorization services (THIS WAS THE FIX)
//builder.Services.AddAuthorization();

//// Add our custom services
////builder.Services.AddScoped<TokenService>();

//// 5. Add our custom services
//builder.Services.AddScoped<TokenService>();
//builder.Services.AddScoped<IOracleService, MockOracleService>(); // Register our mock service


//// Add services for API documentation (Swagger/OpenAPI)
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    // Add a security definition for JWT Bearer tokens
//    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//    {
//        In = ParameterLocation.Header,
//        Description = "Please enter a valid token",
//        Name = "Authorization",
//        Type = SecuritySchemeType.Http,
//        BearerFormat = "JWT",
//        Scheme = "Bearer"
//    });

//    // Add a security requirement to enforce authorization on endpoints
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type=ReferenceType.SecurityScheme,
//                    Id="Bearer"
//                }
//            },
//            new string[]{}
//        }
//    });
//});


//// -----------------------------------------------------------------------------
//// 2. BUILD THE APPLICATION
//// -----------------------------------------------------------------------------
//var app = builder.Build();

//// -----------------------------------------------------------------------------
//// 3. CONFIGURE THE HTTP REQUEST PIPELINE (MIDDLEWARE)
//// -----------------------------------------------------------------------------

//// Use developer-friendly features in the development environment
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();

//// The order of these is important
//app.UseAuthentication(); // First, determine who the user is
//app.UseAuthorization();  // Then, determine what they are allowed to do

//// -----------------------------------------------------------------------------
//// 4. DEFINE API ENDPOINTS
//// -----------------------------------------------------------------------------

//// Group endpoints under "/api/auth" for better organization
//var authGroup = app.MapGroup("/api/auth");

//authGroup.MapPost("/register", async (UserManager<User> userManager, RegisterDto registerDto) =>
//{
//    var user = new User
//    {
//        UserName = registerDto.Username,
//        Email = registerDto.Email,
//        FullName = registerDto.FullName
//    };

//    var result = await userManager.CreateAsync(user, registerDto.Password);

//    if (!result.Succeeded)
//    {
//        return Results.BadRequest(result.Errors);
//    }

//    // Assign the new user to the "Inspector" role by default
//    await userManager.AddToRoleAsync(user, "Inspector");

//    return Results.Ok(new { Message = "User registered successfully!" });
//})
//.WithTags("Authentication") // Group endpoints in Swagger UI
//.Produces(StatusCodes.Status200OK)
//.Produces(StatusCodes.Status400BadRequest);

//authGroup.MapPost("/login", async (UserManager<User> userManager, TokenService tokenService, LoginDto loginDto) =>
//{
//    var user = await userManager.FindByNameAsync(loginDto.Username);

//    if (user == null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
//    {
//        return Results.Unauthorized();
//    }

//    return Results.Ok(new UserDto
//    {
//        Username = user.UserName,
//        Token = tokenService.CreateToken(user)
//    });
//})
//.WithTags("Authentication")
//.Produces<UserDto>(StatusCodes.Status200OK)
//.Produces(StatusCodes.Status401Unauthorized);

//// Create a new group for reports that requires authorization
//var reportGroup = app.MapGroup("/api/reports").RequireAuthorization();

//reportGroup.MapPost("/start", async (
//    StartInspectionDto startDto,
//    IOracleService oracleService,
//    ApplicationDbContext dbContext,
//    HttpContext httpContext) =>
//{
//    // 1. Get the current user's ID from the token
//    var inspectorIdClaim = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
//    if (!int.TryParse(inspectorIdClaim, out var inspectorId))
//    {
//        return Results.Unauthorized();
//    }

//    // 2. Fetch data from the "Oracle" system
//    var prefilledData = await oracleService.GetPackingListDetailsAsync(startDto.PurchaseOrderNumber);
//    if (string.IsNullOrEmpty(prefilledData.ProductDescription))
//    {
//        return Results.NotFound(new { Message = "Purchase Order not found." });
//    }

//    // 3. Create the new Inspection Report entity
//    var newReport = new InspectionReport
//    {
//        ReportID = Guid.NewGuid(),
//        PurchaseOrderNumber = startDto.PurchaseOrderNumber,
//        CustomerID = startDto.CustomerId,
//        InspectorUserID = inspectorId,
//        SBLOrderNumber = prefilledData.SblOrderNumber,
//        OverallStatus = "Draft",
//        CreatedAt = DateTime.UtcNow,
//        ReportDetails = new ReportDetails
//        {
//            ProductDescription = prefilledData.ProductDescription,
//            StyleNumber = prefilledData.StyleNumber,
//            TotalQuantity = prefilledData.TotalQuantity
//        }
//    };

//    // 4. Save the new report to our database
//    await dbContext.InspectionReports.AddAsync(newReport);
//    await dbContext.SaveChangesAsync();

//    // 5. Return the response
//    var response = new StartInspectionResponseDto
//    {
//        ReportId = newReport.ReportID,
//        PrefilledData = prefilledData
//    };

//    return Results.Created($"/api/reports/{response.ReportId}", response);
//});
//// -----------------------------------------------------------------------------
//// 5. RUN THE APPLICATION and SEED THE DATABASE
//// -----------------------------------------------------------------------------

//// Seed the database with initial roles when the application starts
//using (var scope = app.Services.CreateScope())
//{
//    var services = scope.ServiceProvider;
//    try
//    {
//        var roleManager = services.GetRequiredService<RoleManager<Role>>();
//        var roles = new[] { "Admin", "Inspector" };

//        foreach (var role in roles)
//        {
//            if (!await roleManager.RoleExistsAsync(role))
//            {
//                await roleManager.CreateAsync(new Role { Name = role });
//            }
//        }
//    }
//    catch (Exception ex)
//    {
//        var logger = services.GetRequiredService<ILogger<Program>>();
//        logger.LogError(ex, "An error occurred during database seeding of roles.");
//    }
//}

//app.Run();


using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyInspection.Application.DTOs;
using MyInspection.Application.Interfaces;
using MyInspection.Core.Entities;
using MyInspection.Infrastructure.Data;
using MyInspection.Infrastructure.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// -----------------------------------------------------------------------------
// 1. CONFIGURE SERVICES in the DEPENDENCY INJECTION CONTAINER
// -----------------------------------------------------------------------------

// Add DbContext and SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

// Add Identity services once, with a clear configuration
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add Authentication services and configure JWT Bearer
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not configured.")))
    };
});

// Add Authorization services
builder.Services.AddAuthorization();

// Register our custom services
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IOracleService, MockOracleService>();

// Add services for API documentation (Swagger/OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            new string[]{}
        }
    });
});

// -----------------------------------------------------------------------------
// 2. BUILD THE APPLICATION
// -----------------------------------------------------------------------------
var app = builder.Build();

// -----------------------------------------------------------------------------
// 3. CONFIGURE THE HTTP REQUEST PIPELINE (MIDDLEWARE)
// -----------------------------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// -----------------------------------------------------------------------------
// 4. DEFINE API ENDPOINTS
// -----------------------------------------------------------------------------

var authGroup = app.MapGroup("/api/auth").WithTags("Authentication");

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

    await userManager.AddToRoleAsync(user, "Inspector");

    return Results.Ok(new { Message = "User registered successfully!" });
}).Produces(StatusCodes.Status200OK).Produces(StatusCodes.Status400BadRequest);

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
}).Produces<UserDto>(StatusCodes.Status200OK).Produces(StatusCodes.Status401Unauthorized);

var reportGroup = app.MapGroup("/api/reports").RequireAuthorization().WithTags("Reports");

reportGroup.MapPost("/start", async (
    StartInspectionDto startDto,
    IOracleService oracleService,
    ApplicationDbContext dbContext,
    HttpContext httpContext) =>
{
    // 1. Get and VALIDATE the current user's ID
    var inspectorIdClaim = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
    if (!int.TryParse(inspectorIdClaim, out var inspectorId))
    {
        return Results.Unauthorized();
    }
    // Check if the user actually exists in the database
    var inspector = await dbContext.Users.FindAsync(inspectorId);
    if (inspector == null)
    {
        return Results.Unauthorized();
    }

    // 2. VALIDATE that the customer exists
    var customer = await dbContext.Customers.FindAsync(startDto.CustomerId);
    if (customer == null)
    {
        // Return a 404 Not Found if the customer doesn't exist.
        return Results.NotFound(new { Message = $"Customer with ID {startDto.CustomerId} not found." });
    }

    // 3. Fetch data from the "Oracle" system
    var prefilledData = await oracleService.GetPackingListDetailsAsync(startDto.PurchaseOrderNumber);
    if (string.IsNullOrEmpty(prefilledData.ProductDescription))
    {
        return Results.NotFound(new { Message = "Purchase Order not found." });
    }

    // 4. Create the new Inspection Report entity
    var newReport = new InspectionReport
    {
        // ReportID is generated by default (Guid.NewGuid())
        PurchaseOrderNumber = startDto.PurchaseOrderNumber,
        CustomerID = customer.CustomerID, // Use the ID from the validated customer
        InspectorUserID = inspector.Id, // Use the ID from the validated user
        SBLOrderNumber = prefilledData.SblOrderNumber,
        OverallStatus = "Draft",
        ReportDetails = new ReportDetails
        {
            ProductDescription = prefilledData.ProductDescription,
            StyleNumber = prefilledData.StyleNumber,
            TotalQuantity = prefilledData.TotalQuantity
        }
        // CreatedAt is set by default
    };

    // 5. Save the new report to our database
    await dbContext.InspectionReports.AddAsync(newReport);
    await dbContext.SaveChangesAsync();

    // 6. Return the response
    var response = new StartInspectionResponseDto
    {
        ReportId = newReport.ReportID,
        PrefilledData = prefilledData
    };

    return Results.Created($"/api/reports/{response.ReportId}", response);
});

reportGroup.MapGet("/{reportId:guid}", async (Guid reportId, ApplicationDbContext dbContext) =>
{
    var report = await dbContext.InspectionReports
        .Include(r => r.ReportDetails)       // Load the related details
        .Include(r => r.QuantityItems)      // Load the related quantity items
        .Include(r => r.Defects)            // Load the related defects
        .FirstOrDefaultAsync(r => r.ReportID == reportId);

    if (report == null)
    {
        return Results.NotFound();
    }

    // Map the entity to our DTO to send back to the client
    var reportDto = new FullReportDto
    {
        ReportId = report.ReportID,
        PurchaseOrderNumber = report.PurchaseOrderNumber,
        SblOrderNumber = report.SBLOrderNumber ?? string.Empty,
        ProductDescription = report.ReportDetails?.ProductDescription ?? string.Empty,
        OverallStatus = report.OverallStatus,
        QuantityItems = report.QuantityItems.Select(qi => new ReportQuantityItemDto
        {
            Id = qi.ReportQuantityItemID,
            StyleArticle = qi.StyleArticle,
            PONumber = qi.PONumber,
            OrderQuantity = qi.OrderQuantity,
            InspectedQtyPacked = qi.InspectedQtyPacked,
            InspectedQtyNotPacked = qi.InspectedQtyNotPacked,
            CartonsPacked = qi.CartonsPacked,
            CartonsNotPacked = qi.CartonsNotPacked
        }).ToList(),
        Defects = report.Defects.Select(d => new ReportDefectDto
        {
            Id = d.DefectID,
            DefectDescription = d.DefectDescription,
            CriticalCount = d.CriticalCount,
            MajorCount = d.MajorCount,
            MinorCount = d.MinorCount
        }).ToList()
    };

    return Results.Ok(reportDto);
});


reportGroup.MapPut("/{reportId:guid}", async (Guid reportId, FullReportDto updatedReportDto, ApplicationDbContext dbContext) =>
{
    // Find the existing report and its related items
    var report = await dbContext.InspectionReports
        .Include(r => r.QuantityItems)
        .Include(r => r.Defects)
        .FirstOrDefaultAsync(r => r.ReportID == reportId);

    if (report == null)
    {
        return Results.NotFound();
    }

    // Update the main report properties
    report.OverallStatus = updatedReportDto.OverallStatus;
    report.LastModifiedAt = DateTime.UtcNow;

    // Update, Add, or Remove Quantity Items
    // This is a simple but effective way to sync the child collections
    dbContext.ReportQuantityItems.RemoveRange(report.QuantityItems); // Remove old ones
    foreach (var itemDto in updatedReportDto.QuantityItems)
    {
        report.QuantityItems.Add(new ReportQuantityItem
        {
            StyleArticle = itemDto.StyleArticle,
            PONumber = itemDto.PONumber,
            OrderQuantity = itemDto.OrderQuantity,
            InspectedQtyPacked = itemDto.InspectedQtyPacked,
            InspectedQtyNotPacked = itemDto.InspectedQtyNotPacked,
            CartonsPacked = itemDto.CartonsPacked,
            CartonsNotPacked = itemDto.CartonsNotPacked
        });
    }

    // Update, Add, or Remove Defects
    dbContext.ReportDefects.RemoveRange(report.Defects); // Remove old ones
    foreach (var defectDto in updatedReportDto.Defects)
    {
        report.Defects.Add(new ReportDefect
        {
            DefectDescription = defectDto.DefectDescription,
            CriticalCount = defectDto.CriticalCount,
            MajorCount = defectDto.MajorCount,
            MinorCount = defectDto.MinorCount
        });
    }

    await dbContext.SaveChangesAsync();

    return Results.NoContent(); // 204 NoContent is a standard response for a successful PUT
});

//reportGroup.MapPost("/start", async (
//    StartInspectionDto startDto,
//    IOracleService oracleService,
//    ApplicationDbContext dbContext,
//    HttpContext httpContext) =>
//{
//    var inspectorIdClaim = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
//    if (!int.TryParse(inspectorIdClaim, out var inspectorId))
//    {
//        return Results.Unauthorized();
//    }

//    var prefilledData = await oracleService.GetPackingListDetailsAsync(startDto.PurchaseOrderNumber);
//    if (string.IsNullOrEmpty(prefilledData.ProductDescription))
//    {
//        return Results.NotFound(new { Message = "Purchase Order not found." });
//    }

//    var newReport = new InspectionReport
//    {
//        ReportID = Guid.NewGuid(),
//        PurchaseOrderNumber = startDto.PurchaseOrderNumber,
//        CustomerID = startDto.CustomerId,
//        InspectorUserID = inspectorId,
//        SBLOrderNumber = prefilledData.SblOrderNumber,
//        OverallStatus = "Draft",
//        CreatedAt = DateTime.UtcNow,
//        ReportDetails = new ReportDetails
//        {
//            ProductDescription = prefilledData.ProductDescription,
//            StyleNumber = prefilledData.StyleNumber,
//            TotalQuantity = prefilledData.TotalQuantity
//        }
//    };

//    await dbContext.InspectionReports.AddAsync(newReport);
//    await dbContext.SaveChangesAsync();

//    var response = new StartInspectionResponseDto
//    {
//        ReportId = newReport.ReportID,
//        PrefilledData = prefilledData
//    };

//    return Results.Created($"/api/reports/{response.ReportId}", response);
//}).Produces<StartInspectionResponseDto>(StatusCodes.Status201Created).Produces(StatusCodes.Status404NotFound).Produces(StatusCodes.Status401Unauthorized);

// -----------------------------------------------------------------------------
// 5. RUN THE APPLICATION and SEED THE DATABASE
// -----------------------------------------------------------------------------

using (var scope = app.Services.CreateScope())
{
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
        logger.LogError(ex, "An error occurred during database seeding of roles.");
    }
}

app.Run();