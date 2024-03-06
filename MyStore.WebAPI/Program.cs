using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyStore.Database;
using MyStore.Service.Models;
using MyStore.Service.Services.Email;
using MyStore.Service.Services.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// For Entity Framework
var configuration = builder.Configuration;
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("MSSqlServer")));

// For Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Tokens.ProviderMap["Email"] = new TokenProviderDescriptor(typeof(EmailTokenProvider<ApplicationUser>));
}).AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Add config for Confirm Email
builder.Services.Configure<IdentityOptions>(
    options => options.SignIn.RequireConfirmedEmail = true);

builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(24));

// Add Email Configuration
var emailConfig = configuration.GetSection("EmailConfig").Get<EmailConfig>();
builder.Services.AddSingleton(emailConfig);


// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,

                   ValidAudience = configuration["JWT:ValidAudience"],
                   ValidIssuer = configuration["JWT:ValidIssuer"],

                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
               };
               options.Events = new JwtBearerEvents
               {
                   OnTokenValidated = context =>
                   {
                       JwtSecurityToken accessToken = context.SecurityToken as JwtSecurityToken;
                       if (accessToken != null)
                       {
                           ClaimsIdentity identity = context.Principal.Identity as ClaimsIdentity;
                           identity?.AddClaim(new Claim("access_token", accessToken.RawData));
                       }

                       return Task.CompletedTask;
                   }
               };
           });

builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IUserService, UserService>();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = new PathString("/Home/Login");
    options.LogoutPath = new PathString("/Home/Logout");
    options.AccessDeniedPath = new PathString("/Home/AccessDenied");

    options.Cookie = new()
    {
        Name = "IdentityCookie",
        HttpOnly = true,
        SameSite = SameSiteMode.Lax,
        SecurePolicy = CookieSecurePolicy.Always
    };
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(1);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();


