using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server.Hubs;
using Server.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Configures Services
// Add services to the container.
builder.Services.AddControllersWithViews();

//Configure Entity Framework with PostgreSql
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

//Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(options =>
	{
		options.RequireHttpsMetadata = false; //Disable https metadata validation
		options.SaveToken = true; //Saves token in AuthenticationProperties
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
		{
			ValidateIssuer = true, // Validates if token has a valid issuer
            ValidateAudience = true,//Validates if token is intended for the correct audience
            ValidateLifetime = true, //Ensure that token hasn't expired
			ValidIssuer = builder.Configuration["JwtSettings:Issuer"], //Load issuer from configuration
            ValidAudience = builder.Configuration["JwtSettings:Audience"],//Load audience from configuration
            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
				Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))//Load signing key from configuration
        };
	});

//Configure SignalR for real-time communication
builder.Services.AddSignalR();

//Configure CORS to allow cross-origin requests
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAll", policy =>
	{
		policy.AllowAnyOrigin() //Allow requests from any origin
              .AllowAnyMethod() //Allow all HTTP methods
              .AllowAnyHeader();//Allow all headers
    });
});


var app = builder.Build();

//Map SignalR hub for real-time updating
app.MapHub<QuantityUpdateHub>("/quantityupdate");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//Enables JWT authentication and authorization
app.UseAuthentication();
app.UseAuthorization();

//Map routes for controllers
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


//Apply CORS policy globally to all request
app.UseCors("AllowAll");

//Start application on the specified URL 
app.Run("https://localhost:5001");
