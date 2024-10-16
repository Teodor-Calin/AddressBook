using Microsoft.EntityFrameworkCore;
using AddressBook.Domain;
using System.Numerics;

namespace AddressBook.DataAccess;

public interface IDataContext
{
    DbSet<Contact> Contacts { get; set; }
    DbSet<Address> Addresses { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}