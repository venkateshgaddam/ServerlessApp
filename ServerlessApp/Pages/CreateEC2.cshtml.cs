using Microsoft.AspNetCore.Mvc.RazorPages;
using ServerlessApp.DAL;

namespace ServerlessApp.Pages
{
    public class CreateEC2Model : PageModel
    {
        

        public void OnGet()
        {

        }

        public void OnPost() {

            clsCreateEC2 clsCreateEC2 = new clsCreateEC2();
            clsCreateEC2.CreateInstance();
        }
    }
}