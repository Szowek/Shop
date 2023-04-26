using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.Pages.Checkout
{
    public class CustomerInformationModel : PageModel
    {


        [BindProperty]
        public AddCustomerInformation.Request CustomerInformation { get; set; }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();

            if (information != null)
            {
                return RedirectToPage("/Checkout/Payment");
            }
            else
            {
                //dla u³atwienia testowania
                CustomerInformation = new AddCustomerInformation.Request
                {
                    FirstName = "A",
                    LastName = "A",
                    Email = "a@b.com",
                    PhoneNumber = "42",
                    Address1 = "A",
                    Address2 = "A",
                    City = "A",
                    PostCode = "42",
                };
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            new AddCustomerInformation(HttpContext.Session).Do(CustomerInformation);

            return RedirectToPage("/Checkout/Payment");
        }

    }
}
