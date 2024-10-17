using Moq;
using AddressBook.Application;
using AddressBook.Domain;
using AddressBook.DataAccess.Repositories;

public class ContactServiceTests
{
    private readonly Mock<IContactsRepository> _mockRepo;
    private readonly ContactsService _contactService;

    public ContactServiceTests()
    {
        _mockRepo = new Mock<IContactsRepository>();

        _contactService = new ContactsService(_mockRepo.Object);
    }

    //[Fact]
    //public async Task GetAllContacts_ShouldReturnListOfContacts()
    //{
    //    // Arrange: Prepare a mock list of contacts
    //    var contacts = new List<Contact>
    //    {
    //        new Contact { Id = 1, Name = "John1" },
    //        new Contact { Id = 2, Name = "John2" },
    //        new Contact { Id = 3, Name = "John3" },
    //        new Contact { Id = 4, Name = "John4" },
    //        new Contact { Id = 5, Name = "John5" },
    //        new Contact { Id = 6, Name = "John6" },
    //        new Contact { Id = 7, Name = "John7" },
    //        new Contact { Id = 8, Name = "John8" }
    //    };

    //    _mockRepo.Setup(repo => repo.GetAllContacts()).Returns(contacts.AsQueryable());
    //    _mockRepo.Setup(repo => repo.GetCount()).ReturnsAsync(8);

    //    var result = await _contactService.GetAll(0, 5);

    //    Assert.NotNull(result);
    //    Assert.Equal(5, result.Contacts?.Count());
    //    Assert.Equal(8, result.Total);
    //}

    [Fact]
    public async Task GetContactById_ShouldReturnCorrectContact()
    {
        var contact = new Contact { Id = 1, Name = "John", 
            Addresses = new List<Address> {
                new Address{ Id = 1, Street = "789 Home St", City =  "Los Angeles", State = "CA", ZipCode= "90001", AddressType = AddressTypes.Home },
                new Address{ Id = 2, Street = "78 asdfa sd", City =  "New york City", State = "NY", ZipCode= "25452", AddressType = AddressTypes.Work },
            }
        };
        _mockRepo.Setup(repo => repo.GetContactById(1)).ReturnsAsync(contact);

        var result = await _contactService.GetById(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetContactById_ShouldReturnNullForNonExistentContact()
    {
        _mockRepo.Setup(repo => repo.GetContactById(It.IsAny<int>())).ReturnsAsync((Contact)null);

        var result = await _contactService.GetById(100);

        Assert.Null(result);
    }

}
