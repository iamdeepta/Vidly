using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models; 

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }   //code became more loosely coupled if we use IEmunerable instead of List
        public Customer Customer { get; set; }

        public string Title
        {
            get
            {
                if (Customer != null && Customer.Id != 0)
                    return "Edit Customer";

                return "New Customer";
            }
        }
    }
}