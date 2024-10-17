using AddressBook.DataAccess;
using AddressBook.DataAccess.Repositories;
using AddressBook.Domain;
using Application.ServiceModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;

namespace AddressBook.Application;

public class ContactsService : IContactsService
{
    //private readonly DataContext _context;
    private readonly IContactsRepository _contactsRepository;

    public ContactsService(IContactsRepository contactsRepository)
    {
//        _context = context;
          _contactsRepository= contactsRepository;
    }


    public async Task<ContactListResponseModel> GetAll(int page, int size)
    {

        var contact = await _contactsRepository.GetAllContacts()
                .Skip(page * size)
                .Take(size)
                .Select(c => new ContactListItemModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

        var total = await _contactsRepository.GetCount();

        return new ContactListResponseModel
        {
            Total = total,
            Contacts = contact
        };
    }

    public async Task<ContactDetailsModel?> GetById(int id)
    {
        //var contact =  await _context.Contacts
        //    .Where(c => c.Id == id)
        //    .Include(c => c.Addresses)
        //    .SingleOrDefaultAsync(c => c.Id == id);

        var contact = await _contactsRepository.GetContactById(id);

        if (contact == null)
        {
            return (ContactDetailsModel) null;
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