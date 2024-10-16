using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBook.Domain;

public class Contact
{
    public Contact()
    {
        Addresses= new List<Address>();
    }
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<Address> Addresses { get; set; }
}