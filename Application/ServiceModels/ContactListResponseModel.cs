using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceModels
{
    public class ContactListResponseModel {

        public ContactListResponseModel()
        {
            Contacts = new List<ContactListItemModel>();
        }

        public int Total { get; set; }
        public IEnumerable<ContactListItemModel> Contacts { get; set; }
    }
}
