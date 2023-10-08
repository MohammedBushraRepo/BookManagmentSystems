using BookShopApplication.Models;
using BookShopApplication.ViewModels;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BookShopApplication.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookStoreRepository<Book> _bookRepository;
        private readonly IBookStoreRepository<Author> _authorRepository;
        private readonly IHostingEnvironment _hosting;

        public BookController(IBookStoreRepository<Book> bookRepository
            , IBookStoreRepository<Author> authorRepository
            , IHostingEnvironment hosting)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _hosting = hosting;
        }
        // GET: BookController
        public ActionResult Index()
        {
            var books = _bookRepository.List();
            return View(books);
        }

        // GET: BookController/Details/5
        public ActionResult Details(int id)
        {
            var books = _bookRepository.Find(id);

            return View(books);
        }

        // GET: BookController/Create
        public ActionResult Create()
        {
            var model = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };
            return View(model);
        }



        // POST: BookController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // How upload file to the project
                    string fileName = UploadFile(model.File) ?? string.Empty; //اما يرجع الملق او يكون فارغ
                                                                              //   ********************************************************************
                                                                              //if (model.File != null)               حفظ اللوجيك في الميثود اعلا
                                                                              //{
                                                                              //    string uploads = Path.Combine(_hosting.WebRootPath, "uploads");
                                                                              //    fileName = model.File.FileName;
                                                                              //    string fullPath = Path.Combine(uploads, fileName);
                                                                              //    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                                                                              //}
                                                                              //  **********************************************************************
                    if (model.AuthorId == -1)
                    {
                        ViewBag.Message = "please Enter Author from list";

                        return View(GetAllAuthors());

                    }

                    var author = _authorRepository.Find(model.AuthorId);
                    Book book = new Book
                    {
                        Id = model.BookId,
                        Title = model.Title,
                        Description = model.Description,
                        Author = author,
                        ImagUrl = fileName

                    };
                    _bookRepository.Add(book);
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }

            }
            ModelState.AddModelError("", "You have to fill all Requird fields");
            return View(GetAllAuthors());

        }

        // GET: BookController/Edit/5
        public ActionResult Edit(int id)
        {

            var book = _bookRepository.Find(id);
            var authorId = book.Author == null ? book.Author.Id = 0 : book.Author.Id; //IF null then "?" make value to 0 else ":" return it back
            var viewModel = new BookAuthorViewModel
            {
                BookId = book.Id,
                Title = book.Title,
                Description = book.Description,
                AuthorId = authorId,
                Authors = _authorRepository.List().ToList(),
                ImageUrl = book.ImagUrl
            };


            return View(viewModel);
        }

        // POST: BookController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, BookAuthorViewModel viewModel)
        {
            try
            {

                // How upload file to the project
                string fileName = UploadFile(viewModel.File, viewModel.ImageUrl);
                var author = _authorRepository.Find(viewModel.AuthorId);
                Book book = new Book
                {
                    Id = viewModel.BookId,
                    Title = viewModel.Title,
                    Description = viewModel.Description,
                    Author = author,
                    ImagUrl = fileName


                };
                _bookRepository.Update(viewModel.BookId, book);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        // GET: BookController/Delete/5
        public ActionResult Delete(int id)
        {
            var book = _bookRepository.Find(id);


            return View(book);
        }

        // POST: BookController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirm(int id)
        {
            try
            {
                _bookRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }



        List<Author> FillSelectList()
        {

            var authors = _authorRepository.List().ToList();
            authors.Insert(0, new Author { Id = -1, FullName = "----- please select an author -----" });
            return (authors);
        }



        BookAuthorViewModel GetAllAuthors()
        {
            var vmodel = new BookAuthorViewModel
            {
                Authors = FillSelectList()
            };

            return vmodel;
        }


        // in create Case
        string UploadFile(IFormFile file)
        {
            if (file != null)
            {
                string uploads = Path.Combine(_hosting.WebRootPath, "uploads");
                //   fileName = model.File.FileName;
                string fullPath = Path.Combine(uploads, file.FileName);
                file.CopyTo(new FileStream(fullPath, FileMode.Create));

                return file.FileName;
            }
            return null;
        }


        // in Edit Case
        string UploadFile(IFormFile file, string imagUrl)
        {
            if (file != null)
            {


                string uploads = Path.Combine(_hosting.WebRootPath, "uploads");

                string newPath = Path.Combine(uploads, file.FileName);

                string oldPath = Path.Combine(uploads, imagUrl);

                if (oldPath != newPath)
                {
                    System.IO.File.Delete(oldPath);//Delete the File

                    //Save New Image
                    file.CopyTo(new FileStream(newPath, FileMode.Create));
                }
                return file.FileName;

            }
            return imagUrl;


        }

        public ActionResult Search(string term)
        {
            var result = _bookRepository.Search(term);

            return View("Index", result);
        }
    }
}