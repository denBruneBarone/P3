using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class DataContext : DbContext, IDataContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<UserAction> UserActions => Set<UserAction>();
    public DbSet<Mapping> Mappings => Set<Mapping>();



    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
}

public interface IDataContext
{
    public DbSet<User> Users { get; }
    public DbSet<Product> Products { get;}
    public DbSet<UserAction> UserActions { get; }
    public DbSet<Mapping> Mappings { get; }


}
