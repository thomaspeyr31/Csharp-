using Microsoft.EntityFrameworkCore;
using TaskBoard.Models;

namespace TaskBoard.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Workspace> Workspaces { get; set; }

    public DbSet<Board> Boards { get; set; }

    public DbSet<BoardList> Lists { get; set; }

    public DbSet<Card> Cards { get; set; }
}
