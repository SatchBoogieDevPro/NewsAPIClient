using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NewsAPIClient.Models;
using Newtonsoft.Json;

namespace NewsAPIClient.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public List<SelectListItem> Languages
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem() { Text = "Arabic", Value = "ar" },
                    new SelectListItem() { Text = "German", Value = "de" },
                    new SelectListItem() { Text = "English", Value = "en" },
                    new SelectListItem() { Text = "Spanish", Value = "es" },
                    new SelectListItem() { Text = "French", Value = "fr" },
                    new SelectListItem() { Text = "Hebrew", Value = "he" },
                    new SelectListItem() { Text = "Italian", Value = "it" },
                    new SelectListItem() { Text = "Dutch", Value = "nl" },
                    new SelectListItem() { Text = "Norwegian", Value = "no" },
                    new SelectListItem() { Text = "Portuguese", Value = "pt" },
                    new SelectListItem() { Text = "Russian", Value = "ru" },
                    new SelectListItem() { Text = "Sami", Value = "se" },
                    new SelectListItem() { Text = "Chinese", Value = "zn" }

                };
            }
        }
        public async Task<IActionResult> Index(string SearchString, DateTime? From, DateTime? To, string Title, string Country, string Language, int PageNumber=1, int PageSize=5)
        {
            ViewData["CurrentFilter"] = SearchString;
            List<Article> filternewsList = new List<Article>();
            NewsInfo newsList = new NewsInfo();
            string ServiceUrl = "https://newsapi.org/v2/everything?q=bitcoin&apiKey=61ade6b9828d455ba0f71a59800aa73a";
           
                        
            if (PageNumber > 0)
            {
                ServiceUrl += "&page=" + PageNumber.ToString() + "&pageSize=" + PageSize.ToString();
            }

            if (From != null)
            {
                ServiceUrl += "&from=" + From;
            }

            if (To != null)
            {
                ServiceUrl += "&to=" + To;
            }

            if (Language != null)
            {
                ServiceUrl += "&language=" + Language;
            }

            if (Country != null)
            {
                ServiceUrl += "&country=" + Country;
            }

            if (Title != null)
            {
                ServiceUrl += "&qTitle=" + Title;
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(ServiceUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    newsList = JsonConvert.DeserializeObject<NewsInfo>(apiResponse);
                        
                    if (!String.IsNullOrEmpty(SearchString))
                    {
                        newsList.articles = newsList.articles
                                            .Where(s => s.title.Contains(SearchString)
                                            || s.author.Contains(SearchString)).ToList();
                    }
                }
            }
            newsList.PageNumber = PageNumber;
            newsList.PageSize = PageSize;
            newsList.Language = Language;
            return View(newsList);
        }

        public List<SelectListItem> GetLangiages()
        {

            return new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Arabic", Value = "ar" },
                new SelectListItem() { Text = "German", Value = "de" },
                new SelectListItem() { Text = "English", Value = "en" },
                new SelectListItem() { Text = "Spanish", Value = "es" },
                new SelectListItem() { Text = "French", Value = "fr" },
                new SelectListItem() { Text = "Hebrew", Value = "he" },
                new SelectListItem() { Text = "Italian", Value = "it" },
                new SelectListItem() { Text = "Dutch", Value = "nl" },
                new SelectListItem() { Text = "Norwegian", Value = "no" },
                new SelectListItem() { Text = "Portuguese", Value = "pt" },
                new SelectListItem() { Text = "Russian", Value = "ru" },
                new SelectListItem() { Text = "Sami", Value = "se" },
                new SelectListItem() { Text = "Chinese", Value = "zn" }
             
            };
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
