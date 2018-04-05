using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data;
using Microsoft.AspNetCore.Session;
using System.Web;

namespace Kebabvognen.Pages
{
    public class SearchModel : PageModel
    {
        public void OnGet()
        {
            System.Diagnostics.Debug.WriteLine("ON GET!");
            
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("[PAGE-LOAD] Sender: " + sender);
        }
    }
}
