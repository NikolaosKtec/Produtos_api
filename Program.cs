
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Produtos_api.DataBase;
using Produtos_api.EndPoints.Authentication;
using Produtos_api.EndPoints.Categorias;
using Produtos_api.EndPoints.Employees;
using Produtos_api.Service.Category;
using Produtos_api.Service.EmployeeIdentity;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidateIssuer = true,


        ValidIssuer = builder.Configuration["JwtBearerTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBearerTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                Environment.GetEnvironmentVariable(
                    builder.Configuration["JwtBearerTokenSettings:Secret_key"]))
            
        ),

    };

});

    
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AplicationDB_context>();

builder.Services.AddDbContext<AplicationDB_context>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<EmployeeService>();




var app = builder.Build();
app.UseAuthorization();
app.UseAuthentication();


app.MapGet("/", () => "Hello World!");

app.MapMethods(CategotiaPost.Template, CategotiaPost.Methods, CategotiaPost.Handle);

app.MapMethods(CategoriaGet.Template, CategoriaGet.Methods, CategoriaGet.Handle);

app.MapMethods(CategoriaPut.Template, CategoriaPut.Methods, CategoriaPut.Handle);

app.MapMethods(CategoriaDelete.Template, CategoriaDelete.Methods, CategoriaDelete.Handle);

app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle);

app.MapMethods(EmployeeGetAll.Template, EmployeeGetAll.Methods, EmployeeGetAll.Handle);

app.MapMethods(Login.Template, Login.Methods, Login.Handle);

app.Run();