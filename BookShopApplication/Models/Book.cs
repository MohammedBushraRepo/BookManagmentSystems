using System;

namespace BookShopApplication.Models
{
    public class Book
    {
        public int Id { get; set; }

        public String Title { get; set; }

        public String Description { get; set; }

        public string  ImagUrl { get; set; }

        public Author Author { get; set; }
    }
}
