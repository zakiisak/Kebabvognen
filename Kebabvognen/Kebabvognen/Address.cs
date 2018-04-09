using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kebabvognen
{
    public class Address
    {
        
        public int Id { get; private set; }
        public string City { get; private set; }
        public int ZipCode { get; private set; }
        public string BillingAddress { get; private set; }
        /// <summary>
        /// Noice!
        /// </summary>
        /// <param name="id"></param>
        /// <param name="city"></param>
        /// <param name="zipCode"></param>
        /// <param name="billingAddress"></param>
        public Address(int id, string city, int zipCode, string billingAddress)
        {
            Id = id;
            City = city;
            ZipCode = zipCode;
            BillingAddress = billingAddress;
        }

    }
}
