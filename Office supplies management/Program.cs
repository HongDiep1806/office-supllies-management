using Office_supplies_management.DAL;
using Microsoft.EntityFrameworkCore;
using Office_supplies_management.Repositories;
using Office_supplies_management.Services;
using MediatR;
using Office_supplies_management.Mappings;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Office_supplies_management.Models;
using Microsoft.OpenApi.Models;

namespace Office_supplies_management
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var jwtSettings = builder.Configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["Secret"]!);

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidateAudience = true,
                    ValidAudience = jwtSettings["Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                // 🛠️ Add this logging code to check if `sub` is available
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        Console.WriteLine("✅ Token Validated Successfully");
                        foreach (var claim in context.Principal.Claims)
                        {
                            Console.WriteLine($"Claim: {claim.Type} => {claim.Value}");
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"❌ Authentication Failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    }
                };
            });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireFinanceEmployee", policy =>
                    policy.RequireClaim("Permission", "CanCreateProduct"));
                options.AddPolicy("AllRolesCanAccess", policy =>
                    policy.RequireClaim("Permission", "CanViewProduct"));
                options.AddPolicy("DepartmentQuery", policy =>
                    policy.RequireClaim("Permission", "ViewUsersDepartment"));
                options.AddPolicy("RequireSupLeaderRole", policy =>
                    policy.RequireClaim("Permission", "ViewAllRequests")); // Ensure this policy is correctly set
            });



            builder.Services.AddMediatR(cfg => cfg.AsScoped(), typeof(Program).Assembly);

            // Add services to the container
            builder.Services.AddControllers();

            // Set up Swagger UI and API Explorer for documentation
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(typeof(ProductProfile));
            builder.Services.AddAutoMapper(typeof(UserProfile));
            builder.Services.AddAutoMapper(typeof(RequestProfile));

            // Configure database context
            builder.Services.AddDbContext<Context>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.CommandTimeout(180))); // 180 seconds timeout

            // Register repositories and services
            builder.Services.AddScoped<IBaseRepository<Product>, BaseRepository<Product>>();
            builder.Services.AddScoped<IBaseRepository<User>, BaseRepository<User>>();
            builder.Services.AddScoped<IBaseRepository<UserType>, BaseRepository<UserType>>();
            builder.Services.AddScoped<IBaseRepository<UserType_Permission>, BaseRepository<UserType_Permission>>();
            builder.Services.AddScoped<IBaseRepository<Permission>, BaseRepository<Permission>>();
            builder.Services.AddScoped<IBaseRepository<Request>, BaseRepository<Request>>();
            builder.Services.AddScoped<IBaseRepository<Product_Request>, BaseRepository<Product_Request>>();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserType_PermissionRepository, UserType_PermissionRepository>();
            builder.Services.AddScoped<IPermissionRepository, PermissionRepsitory>();
            builder.Services.AddScoped<IUserTypeRepository, UserTypeRepository>();
            builder.Services.AddScoped<IRequestRepository, RequestRepository>();
            builder.Services.AddScoped<IProduct_RequestRepository, Product_RequestRepository>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserType_PermissionService, UserType_PermissionService>();
            builder.Services.AddScoped<IPermissionService, PermissionService>();
            builder.Services.AddScoped<IUserTypeService, UserTypeService>();
            builder.Services.AddScoped<IJwtService, JwtService>();
            builder.Services.AddScoped<IRequestService, RequestService>();
            builder.Services.AddScoped<IProduct_RequestService, Product_RequestService>();

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Office Supplies API", Version = "v1" });

                // Configure JWT in Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter token in the text input below (do not include 'Bearer ' prefix)",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Use CORS
            app.UseCors("AllowSpecificOrigin");

            // Configure the HTTP request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Office Supplies API v1");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting(); // Ensure routing comes after CORS and before authorization

            // Authentication and Authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Map controllers (API endpoints)
            app.MapControllers();

            // Global exception handling (optional)
            app.Use(async (context, next) =>
            {
                try
                {
                    await next.Invoke();
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = 500; // Internal Server Error
                    await context.Response.WriteAsync(ex.ToString());
                }
            });

            app.Run();
        }
    }
}
