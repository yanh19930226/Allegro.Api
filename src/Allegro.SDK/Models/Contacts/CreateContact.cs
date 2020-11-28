using System;
using System.Collections.Generic;
using System.Text;

namespace Allegro.SDK.Models.Contacts
{
    public class CreateContactRequest : BaseRequest<CreateContactResponse>
    {
        public string name { get; set; }
        public List<Email> emails { get; set; }
        public List<Phone> phones { get; set; }

        public override string Url => "sale/offer-contacts";
    }

    public class CreateContactResponse
    {
        public string id { get; set; }
        public string name { get; set; }
        public List<Email> emails { get; set; }
        public List<Phone> phones { get; set; }
    }

    public class Email
    {
        public string address { get; set; }
    }

    public class Phone
    {
        public string number { get; set; }
    }
}
