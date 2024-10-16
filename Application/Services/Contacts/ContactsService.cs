using AddressBook.DataAccess;
using AddressBook.Domain;
using Application.ServiceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;

namespace AddressBook.Application;

public class ContactsService : IContactsService
{
    private readonly DataContext _context;

    public ContactsService(DataContext context)
    {
        _context = context;
    }


public ContactListResponseModel GetAll(int page, int size)
    {

        var contact = _context.Contacts
                .Skip(page * size)
                .Take(size)
                .Select(c => new ContactListItemModel
                {
                    Id = c.Id,
                    Name = c.Name
                });

        var total = _context.Contacts.Count();

        return new ContactListResponseModel
        {
            Total = total,
            Contacts = contact
        };
    }

    public async Task<ContactDetailsModel> GetById(int id)
    {
        var contact =  await _context.Contacts
            .Where(c => c.Id == id)
            .Include(c => c.Addresses)
            .SingleOrDefaultAsync(c => c.Id == id);

        if (contact == null)
        {
            return null;
        }

        return new ContactDetailsModel
        {
            Id = contact.Id,
            Name = contact.Name,
            Addresses = contact.Addresses.Select(a => new AddressDetailsModel
            {
                AddressType = a.AddressType,
                City = a.City,
                State = a.State,
                Street = a.Street,
                ZipCode = a.ZipCode
            })
        };
    }

}