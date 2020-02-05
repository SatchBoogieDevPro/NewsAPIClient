using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsAPIClient.Service
{
    public class NewsService
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string Language { get; set; }
        public string Title { get; set; }
        private string apiResponse;

        public async Task<string> GetNews()
        {
           // ViewData["CurrentFilter"] = SearchString;
         //   List<Article> filternewsList = new List<Article>();
        //    NewsInfo newsList = new NewsInfo();
            string ServiceUrl = "https://newsapi.org/v2/everything?apiKey=61ade6b9828d455ba0f71a59800aa73a";


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

            if (Title != null)
            {
                ServiceUrl += "&q=" + Title;
            }

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync(ServiceUrl))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                   // newsList = JsonConvert.DeserializeObject<NewsInfo>(apiResponse);

//                    if (!String.IsNullOrEmpty(SearchString))
//                    {
//                        newsList.articles = newsList.articles
 //                                           .Where(s => s.title.Contains(SearchString)
  //                                          || s.author.Contains(SearchString)).ToList();
  //                  }
                }
            }
            return apiResponse;
        }
    }
}
