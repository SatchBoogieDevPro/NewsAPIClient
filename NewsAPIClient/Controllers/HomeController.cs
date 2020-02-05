using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsAPIClient.Models;
using Newtonsoft.Json;


namespace NewsAPIClient.Controllers
{

    public class HomeController : Controller
    {
        /// <summary>
        /// Private variables declaration section
        /// </summary>
        private string _serviceUrl;
        private List<Article> _filternewsList;
        private NewsInfo _newsList;
        
        /// <summary>
        /// Initializing variables in Constructor
        /// </summary>
        public HomeController()
        {    
            _filternewsList = new List<Article>();
            NewsList = new NewsInfo();
            _serviceUrl = "https://newsapi.org/v2/everything?apiKey=61ade6b9828d455ba0f71a59800aa73a";
        }

        public NewsInfo NewsList { get => _newsList; set => _newsList = value; }

        /// <summary>
        /// Main Controller Action method to retrieve News asynchronously based on search parameters
        /// </summary>
        /// <param name="SearchString"></param>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="Language"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(DateTime? From, DateTime? To, string Language, int PageNumber=1, int PageSize=5, string Title = "bitcoin")
        {
           // ViewData["CurrentFilter"] = SearchString;

            FormatUrl(From, To, Language, PageNumber, PageSize, Title);

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(_serviceUrl))
                { 
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    NewsList = JsonConvert.DeserializeObject<NewsInfo>(apiResponse);

                    
                }
            }
            NewsList.PageNumber = PageNumber;
            NewsList.PageSize = PageSize;
            NewsList.Language = Language;
       
            return View(NewsList);
        }
        /// <summary>
        /// Generate URL with search parameter.
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        /// <param name="Language"></param>
        /// <param name="PageNumber"></param>
        /// <param name="PageSize"></param>
        /// <param name="Title"></param>
        /// <returns></returns>
        private string FormatUrl(DateTime? From, DateTime? To, string Language, int PageNumber = 1, int PageSize = 5, string Title = "bitcoin")
        {
            if (PageNumber > 0)
            {
                _serviceUrl += "&page=" + PageNumber.ToString() + "&pageSize=" + PageSize.ToString();
            }

            if (From != null)
            {
                _serviceUrl += "&from=" + From;
            }

            if (To != null)
            {
                _serviceUrl += "&to=" + To;
            }

            if (Language != null)
            {
                _serviceUrl += "&language=" + Language;
            }

            if (Title != null)
            {
                _serviceUrl += "&q=" + Title;
            }

            return _serviceUrl;
        }
    }
}
