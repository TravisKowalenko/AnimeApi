using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AnimeApi.Models;

public class AnimeStoreContext : DbContext
{
    public AnimeStoreContext(DbContextOptions<AnimeStoreContext> options)
    : base(options)
    {
    }
    public DbSet<AnimeShirt> AnimeShirts { get; set; } = null!;
    public DbSet<AnimeShorts> AnimeShorts { get; set; } = null!;
    public DbSet<Collectible> Collectibles { get; set; }
    public DbSet<User> Users { get; set; }
}