using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coelsa.Challenge.Api.Aplication.Dto
{
    public class ContactDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
