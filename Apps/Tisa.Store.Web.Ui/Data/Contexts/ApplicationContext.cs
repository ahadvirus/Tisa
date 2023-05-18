using Microsoft.EntityFrameworkCore;
using Tisa.Store.Web.Ui.Models.Entities;

namespace Tisa.Store.Web.Ui.Data.Contexts;

public sealed class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {
        Roles = Set<Role>();
        RoleUsers = Set<RoleUser>();
        Users = Set<User>();
        Types = Set<Type>();
        Attributes = Set<Attribute>();
    }

    public DbSet<Role> Roles { get; }
    public DbSet<RoleUser> RoleUsers { get; }
    public DbSet<User> Users { get; }
    public DbSet<Type> Types { get;}
    public DbSet<Attribute> Attributes { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}