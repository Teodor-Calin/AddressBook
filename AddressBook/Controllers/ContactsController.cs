// Controllers/PeopleController.cs
using AddressBook.Application;
using Application.ServiceModels;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Controllers;

[ApiController]
public class ContactsController : ControllerBase
{
    private readonly IContactsService _contactsService;

    public ContactsController(IContactsService contactsService)
    {
        _contactsService = contactsService;
    }

    [Route("api/contacts")]
    [HttpGet]
    public async Task<IActionResult> GetAllContacts([FromQuery] int page = 0, [FromQuery] int size = 5)
    {
        var contacts = await _contactsService.GetAll(page, size);

        return Ok(contacts);
    }

    [Route("api/contacts/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetContactById(int id)
    {
        try
        {
            var contact = await _contactsService.GetById(id);

            if (contact == null)
            {

                return NotFound(new { Message = $"Contact with ID {id} not found." });
            }

            return Ok(contact);
        }
        catch(Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving the contact.", Details = ex.Message });
        }
        
    }

}
