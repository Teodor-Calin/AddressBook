using Application.ServiceModels;

namespace AddressBook.Application;

public interface IContactsService
{
    ContactListResponseModel GetAll(int page, int size);
    Task<ContactDetailsModel> GetById(int id);
}