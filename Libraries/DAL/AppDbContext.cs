using DataAccessLayerAbstraction.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL;
public class AppDbContext : DbContext
{
  public AppDbContext ( DbContextOptions<AppDbContext> options ) :
    base (options)
  {

  }

  protected override void OnModelCreating ( ModelBuilder modelBuilder )
  {
    modelBuilder.Entity<AppUser> (x =>
    {
      x.Property (x => x.UserName)
      .IsRequired ()
      .HasMaxLength (250);

      x.HasIndex (x => x.UserName)
      .IsUnique ();

      x.Property (x => x.Password)
      .IsRequired ()
      .HasMaxLength (250);

      x.HasMany (x => x.ReceivedMessages)
      .WithOne (x => x.From)
      .HasForeignKey (x => x.FromId);

      x.HasMany (x => x.SentMessages)
      .WithOne (x => x.To).HasForeignKey (x => x.ToId);
    });
    modelBuilder.Entity<Message> (x =>
    {
      x.Property (x => x.MessageContent)
      .IsRequired ();

      x.HasOne (x => x.From)
      .WithMany (x => x.ReceivedMessages)
      .HasForeignKey (x => x.FromId);

      x.HasOne (x => x.To)
      .WithMany (x => x.SentMessages)
      .HasForeignKey (x => x.ToId);
    });

    base.OnModelCreating (modelBuilder);
  }

  public DbSet<AppUser> Users { get; set; }
  public DbSet<Message> Messages
  {
    get; set;
  }
}
