﻿using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Produtos_api.Domain.Products;


namespace Produtos_api.DataBase;

public class AplicationDB_context : IdentityDbContext<IdentityUser>
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected readonly IConfiguration Configuration;

    public AplicationDB_context(DbContextOptions<AplicationDB_context> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    // TODO configurações especifícas
    protected override void OnModelCreating(ModelBuilder modelBuilder) {

        base.OnModelCreating(modelBuilder);

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
        // TODO note que o padrão de string de conecção é:
        // Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase
        // e não URL!
        options.UseNpgsql(
            Environment.GetEnvironmentVariable(Configuration["ConnectionStrings:PostgreSql"]) );
        options.UseNpgsql().EnableSensitiveDataLogging();



    }
}
