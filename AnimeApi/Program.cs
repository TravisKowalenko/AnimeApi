using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using AnimeApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AnimeStoreContext>(opt =>
    opt.UseInMemoryDatabase("AnimeStore"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var jwtSecretKey = builder.Configuration["Jwt:SecretKey"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecretKey)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<AnimeStoreContext>();
    SeedData(context); 
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Define the SeedData method
static void SeedData(AnimeStoreContext context)
{
    // Check if the database is empty
    if (!context.AnimeShirts.Any())
    {
        context.AnimeShirts.AddRange(
            new AnimeShirt
            {
                Name = "Eren Yaegar TShirt",
                Description = "Attack on titan, Eren shirt.",
                Size = "Large",
                Price = 29.99M, 
                Color = "Black",
                ImageUrl = "Yaegarblack.jpg"
            },
            new AnimeShirt
            {
                Name = "Eren Yeager TShirt",
                Description = "Attack on titan, Eren shirt, variant.",
                Size = "Large",
                Price = 29.99M,
                Color = "White",
                ImageUrl = "YeagerWhite.jpg"
            }
        );
        context.SaveChanges();
    }
}



