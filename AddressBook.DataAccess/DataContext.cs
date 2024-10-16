using Microsoft.EntityFrameworkCore;
using AddressBook.Domain;

namespace AddressBook.DataAccess;

public class DataContext : DbContext, IDataContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the Contact entity
        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
           .IsRequired()
           .HasMaxLength(200);
        });


        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(a => a.Street)
           .IsRequired()
           .HasMaxLength(200);

            entity.Property(a => a.City)
                .IsRequired()
                .HasMaxLength(200);

            entity.Property(a => a.State)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(a => a.ZipCode)
                .HasMaxLength(20)
                .IsRequired(false);

            entity.HasOne(a => a.Contact)
                .WithMany(c => c.Addresses)
                .HasForeignKey(a => a.ContactId) 
                .OnDelete(DeleteBehavior.Cascade); 
        });        

    }

    public DbSet<Contact> Contacts { get; set; } = null!;
    public DbSet<Address> Addresses { get; set; } = null!;

}