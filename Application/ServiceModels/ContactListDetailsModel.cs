using AddressBook.Domain;

namespace Application.ServiceModels
{
    public class ContactDetailsModel
    {
        public ContactDetailsModel()
        {
            Addresses= new List<AddressDetailsModel>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public IEnumerable<AddressDetailsModel> Addresses {get; set;}
    }
}
