using Moq;
using Microsoft.AspNetCore.Mvc;
using AddressBook.Domain;
using AddressBook.Application;
using AddressBook.Controllers;
using Application.ServiceModels;

public class ContactsControllerTests
{
    private readonly Mock<IContactsService> _mockService;
    private readonly ContactsController _contactController;

    public ContactsControllerTests()
    {
        _mockService = new Mock<IContactsService>();
        _contactController = new ContactsController(_mockService.Object);
    }

    [Fact]
    public async Task GetAllContacts_ReturnsOkResult_WithListOfContacts()
    {
        var response = new ContactListResponseModel
        {
            Contacts = new List<ContactListItemModel>
                {
                    new ContactListItemModel { Id = 1, Name = "John" },
                    new ContactListItemModel { Id = 2, Name = "Jane" },
                    new ContactListItemModel { Id = 3, Name = "Jane2" },
                    new ContactListItemModel { Id = 4, Name = "Jane3" },
                    new ContactListItemModel { Id = 5, Name = "Jane4" }
                },
            Total = 10
        };

        var page = 0;
        var size = 5;
       
        _mockService.Setup(service => service.GetAll(page, size)).ReturnsAsync(response);

        var result = await _contactController.GetAllContacts();

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnContacts = Assert.IsType<ContactListResponseModel>(okResult.Value);
        Assert.Equal(5, returnContacts.Contacts.Count());
        Assert.Equal(10, returnContacts.Total);
    }

    [Fact]
    public async Task GetContactById_ReturnsNotFound_WhenContactDoesNotExist()
    {
        var id = 100;
        _mockService.Setup(service => service.GetById(id)).ReturnsAsync((ContactDetailsModel)null);

        var result = await _contactController.GetContactById(id);

        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task GetContactById_ReturnsOkResult_WithContact()
    {
        var contact = new ContactDetailsModel
        {
            Id = 1,
            Name = "John",
            Addresses = new List<AddressDetailsModel> {
                new AddressDetailsModel{ Street = "789 Home St", City =  "Los Angeles", State = "CA", ZipCode= "90001", AddressType = AddressTypes.Home },
                new AddressDetailsModel{ Street = "78 asdfa sd", City =  "New york City", State = "NY", ZipCode= "25452", AddressType = AddressTypes.Work },
            }
        };
        _mockService.Setup(service => service.GetById(1)).ReturnsAsync(contact);

        var result = await _contactController.GetContactById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnContact = Assert.IsType<ContactDetailsModel>(okResult.Value);
        Assert.Equal(1, returnContact.Id);
    }
}
