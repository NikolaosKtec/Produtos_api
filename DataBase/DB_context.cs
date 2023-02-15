using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Produtos_api.Domain.Products;
/*using System.Security.AccessControl;
using Flunt.Notifications;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
*/

namespace Produtos_api.DataBase;

public class DB_context : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected readonly IConfiguration Configuration;

    public DB_context(DbContextOptions<DB_context> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    // TODO configurações especifícas
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Ignore<Notification>();

        var product = modelBuilder.Entity<Product>();
        var categories = modelBuilder.Entity<Category>();

        product.Property(p => p.Description)
            .HasMaxLength(255);
        product.Property(product => product.Name)
            .IsRequired();
        categories.Property(p => p.Id).UseSerialColumn();
        modelBuilder.Entity<Category>().Property(c => c.name).IsRequired();

        
    }

    // TODO configuração geral
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder){
        configurationBuilder.Properties<string>()
            .HaveMaxLength(100);

      
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(Environment
            .GetEnvironmentVariable(Configuration.GetConnectionString("PostgreSql")));
        options.UseNpgsql().EnableSensitiveDataLogging();



    }
}



/*
class Context_app : IdentityDbContext<IdentityUser>
{

    public DbSet<Produto> Produto { get; set; }
    public DbSet<Categoria> Categoria { get; set; }

    protected readonly IConfiguration Configuration;

    public Context_app(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<Notification>();
        modelBuilder.Entity<Produto>()
            .HasOne<Categoria>()
            .WithOne()
            .HasForeignKey<Produto>(p => p.CategoriaId)
            .OnDelete(deleteBehavior: DeleteBehavior.SetNull);
        //todo aparentemente deu certo 
    }
    // protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    // {
    //     base.ConfigureConventions(configurationBuilder);
    //     configurationBuilder.
    // }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {


        options.UseNpgsql(Environment
            .GetEnvironmentVariable(Configuration.GetConnectionString("PostgreSql")));
        options.UseNpgsql().EnableSensitiveDataLogging();



    }



}
*/