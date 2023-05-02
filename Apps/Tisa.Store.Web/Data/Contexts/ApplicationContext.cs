using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Data.Contexts;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
    }

    public DbSet<Type> Types { get; set; }
    public DbSet<Attribute> Attributes { get; set; }
    public DbSet<AttributeEntity> AttributeEntities { get; set; }
    public DbSet<Entity> Entities { get; set; }
    public DbSet<TypeValidator> TypeValidators { get; set; }
    public DbSet<Validator> Validators { get; set; }
    
    public DbSet<TypeValidatorClaim> TypeValidatorClaims { get; set; }
    
    public DbSet<ValidatorClaim> ValidatorClaims { get; set; }

    public DbSet<AttributeEntityValidator> AttributeEntityValidators { get; set; }
    public DbSet<AttributeEntityValidatorClaim> AttributeEntityValidatorClaims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}