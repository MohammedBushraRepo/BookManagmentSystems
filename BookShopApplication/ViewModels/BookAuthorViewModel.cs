using BookShopApplication.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShopApplication.ViewModels
{
    public class BookAuthorViewModel
    {
        public int BookId { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Title { get; set; }
        [Required]
        [StringLength(150 , MinimumLength =20)]
        public string Description { get; set; }

        public int AuthorId { get; set; }

        public List<Author> Authors { get; set; }

        public IFormFile  File { get; set; }

        public string ImageUrl { get; set; }

    }
}
