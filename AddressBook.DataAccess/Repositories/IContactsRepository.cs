using AddressBook.Domain;

namespace AddressBook.DataAccess.Repositories;

public interface IContactsRepository
{
    IQueryable<Contact> GetAllContacts();
    Task<Contact?> GetContactById(int id);
    Task<int> GetCount();
}
