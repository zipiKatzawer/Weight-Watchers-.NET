using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Project_Net.core.config;
using Serilog;
using System.Text;
using WeightWatchers.Configurations;
using WeightWatchers.core;
using WeightWatchers.data;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
var setting = configuration.GetSection("setting").Get<Settings>();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton(setting);
builder.Services.ConfigurationService();
builder.Host.UseSerilog((context, configuration) =>
{

    ///קריאה של ההגדות מהקונפיd 
    configuration.ReadFrom.Configuration(context.Configuration);

});
builder.Services.AddDbContext<WeightWatchersContext>(option =>
{
    option.UseSqlServer(configuration.GetConnectionString("WeightWatchersConnectionString"));
}
       );
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
     options =>
     {
         options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         {
             Scheme = "Bearer",
             BearerFormat = "JWT",
             In = ParameterLocation.Header,
             Name = "Authorization",
             Description = "Bearer Authentication with JWT Token",
             Type = SecuritySchemeType.Http
         });
         options.AddSecurityRequirement(new OpenApiSecurityRequirement
                   {
                  {
                  new OpenApiSecurityScheme
               {


                       Reference = new OpenApiReference
                                 {
               Id = "Bearer",
                     Type = ReferenceType.SecurityScheme
              }
                               },
                           new List<string>()


}
});
     });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           // ValidateIssuer = true,
           // ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = setting.Jwt.Issuer,
           ValidAudience = setting.Jwt.Audience, // Configuration["Jwt:Issuer"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(setting.Jwt.Key))
       };
   });
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//כתיבה ללוג של כל קריאה אוטמטית לא חובה 
//app.UseSerilogRequestLogging();

//הוספת דיבור בין פרויקטים 
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});


app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

///מגדיר את הפעלה הממידלוור
app.UseMiddleware(typeof(MiddleWare));

app.Run();
