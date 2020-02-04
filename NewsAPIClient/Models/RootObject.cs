using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsAPIClient.Models
{
    public class Source
    {
        public string? id { get; set; }
        public string name { get; set; }
    }
    public class Article
    {
        public Source source { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public string content { get; set; }
    }
    public class NewsInfo
    {
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
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<Article> articles { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string Language { get; set; }

    }
   
}
