using Application.ServiceModels;

namespace AddressBook.Application;

public interface IContactsService
{
    Task<ContactListResponseModel> GetAll(int page, int size);
    Task<ContactDetailsModel?> GetById(int id);
}