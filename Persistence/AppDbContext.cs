using System;
using System.Data.Common;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext(DbContextOptions options):DbContext(options)
{
    public required DbSet <Activity> Activities { get; set; }

  

}
