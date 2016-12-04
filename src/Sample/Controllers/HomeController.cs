using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDbContext _dbContext;
        private int newmax = -1;
        public HomeController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new ViewModel();
            try
            {
                var max = 0;

                if (_dbContext.OnlyTable.Any())
                {
                    max = _dbContext.OnlyTable.Max(x => x.SomeColumn);
                }

                _dbContext.Add(new TestTable() { SomeColumn = max + 1 });
                _dbContext.SaveChanges();

                await MakePostCallAsync("http:\\google.com", vm).ConfigureAwait(true);

                if (!string.IsNullOrEmpty(vm.TextToDisplay))
                {
                    vm.TextToDisplay = "I have inserted the value " + newmax + " into table (-1 means error)";
                }
                else
                {
                    vm.TextToDisplay = "Errored!";
                }


            }
            catch (Exception ex)
            {
                vm.TextToDisplay = "I encountered error message - \"" + ex.Message + "\"";
            }
            return View("Index", vm);
        }

        private async Task MakePostCallAsync(string url, ViewModel vm)
        {
            var httpClient = new HttpClient();

            var httpResponse = await httpClient.PostAsync("http://google.com", null).ConfigureAwait(true);

            newmax = _dbContext.OnlyTable.Max(x => x.SomeColumn);
        }
    }

    public class ViewModel
    {
        public string TextToDisplay { get; set; }
    }
}
