using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AddressBook.Domain;

public class Address
{
    public int Id { get; set; }
    [Required]
    public string Street { get; set; }
    [Required]
    public string City { get; set; }
    [Required]
    public string State { get; set; }
    public string ZipCode { get; set; }
    public AddressTypes AddressType { get; set; }
    public int ContactId { get; set; }

    public virtual Contact Contact { get; set; }
}