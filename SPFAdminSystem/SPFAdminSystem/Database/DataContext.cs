using Microsoft.EntityFrameworkCore;
using SPFAdminSystem.Database.UserFiles;
using System;
using System.Collections.Generic;

public class DataContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<UserAction> UserActions => Set<UserAction>();
    public DbSet<Mapping> Mappings => Set<Mapping>();
    public DbSet<UploadMappingFileChangeProduct> UploadMappingFileChangeProducts => Set<UploadMappingFileChangeProduct>();


    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
}

  