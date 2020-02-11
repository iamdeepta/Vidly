using System;
using System.Collections.Generic;
using System.Data.Entity;   //for Include function
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;   //necessary for db access

        public CustomersController()       //constructor and initialization
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {

            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", viewModel);
        }
        [HttpPost]
        public ActionResult Save(Customer customer)  //can also use NewCustomerViewModel viewModel in parameter
        {
            if (!ModelState.IsValid)      //this is the second step of validation. 1st step is to put data annotations in property such as [Required]
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }


            if(customer.Id == 0)
                _context.Customers.Add(customer);  //it is just added into memory not in the database(for new insertion of customer)
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);  //customer object in db(for update purpose)
                //TryUpdateModel(customerInDb);  //the properties of customer object will be updated based on key value pairs and requested data.but we will not use this because this will update all properties.
                //Mapper.Map(customer, customerInDb);  //this can also be used to update data by using automapper
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }
        // GET: Customers
        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();   //this Include() helps to load customer name and discount rate together
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel     //
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
                
            };
            return View("CustomerForm", viewModel);

        }

       /* private IEnumerable<Customer> GetCustomers()
        {
            return new List<Customer>
            {
                 new Customer { Id = 1, Name = "John Smith" },
                new Customer { Id = 2, Name = "Mary Williams" }
            };
        }*/
    }
}