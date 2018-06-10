using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomConverterJSON
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public bool IsResident { get; set; }

        public string[] strArr { get; set; }

        public int[] intArr { get; set; }

        public Address BirthdPlace { get; set; }

        public List<Company> Companies { get; set; }
    }

    public class Address
{
        public string Country { get; set; }

        public string City { get; set; }

    }

    public class Company
    {
        public string Name { get; set; }

        public Address Location { get; set; }

    }

}
