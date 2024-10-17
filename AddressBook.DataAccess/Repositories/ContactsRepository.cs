using AddressBook.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.DataAccess.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly DataContext _context;

        public ContactsRepository(DataContext context)
        {
            _context = context;
        }

        public IQueryable<Contact> GetAllContacts()
        {
            return _context.Contacts;
        }

        public Task<Contact?> GetContactById(int id)
        {
            return _context.Contacts
            .Where(c => c.Id == id)
            .Include(c => c.Addresses)
            .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<int> GetCount()
        {
            return await _context.Contacts.CountAsync();
        }

    }
}
