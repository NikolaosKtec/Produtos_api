
using Microsoft.AspNetCore.Identity;
using Produtos_api.DataBase;
using Produtos_api.EndPoints.Categorias;
using Produtos_api.EndPoints.Employees;
using Produtos_api.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AplicationDB_context>();

builder.Services.AddDbContext<AplicationDB_context>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<EmployeeService>();

/*AddIdentity<IdentityUser, IdentityRole>(options =>
 {
     options.Password.RequireUppercase = false;
     options.Password.RequireNonAlphanumeric = false;
 })*/
//AddEntityFrameworkStores<Context_app>();
//builder.Services.AddScoped<CategoryService>();
    //.AddScoped<Produtc_service>()
    //.AddScoped<User_service>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapMethods(CategotiaPost.Template, EmployeePost.Methods, EmployeePost.Handle);

app.MapMethods(CategoriaGet.Template, CategoriaGet.Methods, CategoriaGet.Handle);

app.MapMethods(CategoriaPut.Template, CategoriaPut.Methods, CategoriaPut.Handle);

app.MapMethods(CategoriaDelete.Template, CategoriaDelete.Methods, CategoriaDelete.Handle);

app.MapMethods(EmployeePost.Template, EmployeePost.Methods, EmployeePost.Handle);

app.MapMethods(EmployeeGetAll.Template, EmployeeGetAll.Methods, EmployeeGetAll.Handle);
app.Run();