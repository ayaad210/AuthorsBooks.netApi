using AuthorsAngularTask.Dtos;
using AuthorsAngularTask.Interfaces;
using AuthorsAngularTask.Models;
using AutoMapper;
using CoreIdentity.API.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AuthorsAngularTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitofwork;
        private readonly IBookRepo _BookRepo;
        private readonly IAuthorRepo _AuthorRepo;

        public BookController(IMapper mapper, IUnitOfWork unitofwork)

        {
            _mapper = mapper;
            _unitofwork = unitofwork;
            _BookRepo = unitofwork.GetBookRepo();
            _AuthorRepo = unitofwork.GetAuthorRepo();

        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApiResponse<Book>>>> GetAll()
        {
            ApiResponse<Book> res = new ApiResponse<Book>();
            res.MesasageId = 1;
            res.Data = await _BookRepo.GetBooks();

            return Ok(res);
        }
        [HttpPost("Search")]
        public async Task<ActionResult<IEnumerable<ApiResponse<Book>>>> Search(Book model)
        {

            ApiResponse<Book> res = new ApiResponse<Book>();
            res.MesasageId = 1;
            res.Data = await _BookRepo.Search(model);

            return Ok(res);
        }


        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Book>>> GetById(int id)
        {
            ApiResponse<Book> res = new ApiResponse<Book>();
            res.MesasageId = 1;
            res.Data = new List<Book> { await _BookRepo.GetBookById(id) };
            return Ok(res);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Book>>> Post([FromBody] BooksCreate Createmodel)
        {
            ApiResponse<Book> res = new ApiResponse<Book>();

            if (!ModelState.IsValid) { res.Mesasage = "Not Valid Data"; res.MesasageId = 0; return BadRequest(res); }

                Book model = _mapper.Map<Book>(Createmodel);

          

                await _BookRepo.CreateBook(model);
            if (string.IsNullOrEmpty(model.CoverPath))
            {
                model.CoverPath = "default.jpg";
            }
            var result = await _unitofwork.SaveChanges();
            if (result > 0)
            {
                res.Mesasage = "saved successfully";
                res.MesasageId = 1;
                res.Data = new List<Book> { model };
                return Ok(res);

            }
            res.Mesasage = "failed";
            res.MesasageId = -1;
            return BadRequest(res);
        }

        // PUT api/<BookController>/5
        [HttpPut]
        public async Task<ActionResult<ApiResponse<Book>>> Put([FromBody] Book Book)
        {

            ApiResponse<Book> res = new ApiResponse<Book>();
            if (!ModelState.IsValid) { res.Mesasage = "Not Valid Data"; res.MesasageId = 0; return BadRequest(res); }

            var obj = await _BookRepo.UpdateBook(Book);
            if (obj == null)
            {
                res.Mesasage = "not exist";
                res.MesasageId = -1;
                return BadRequest(res);
            }
            var result = await _unitofwork.SaveChanges();
            if (result > 0)
            {
                res.Mesasage = "saved successfully";
                res.MesasageId = 1;
                res.Data = new List<Book> { Book };
                return Ok(res);

            }
            res.Mesasage = "failed";
            res.MesasageId = -1;
            return BadRequest(res);
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Book>>> Delete(int id)
        {

            ApiResponse<Book> res = new ApiResponse<Book>();

            Book obj = await _BookRepo.DeleteBook(id);
            if (obj == null)
            {
                res.Mesasage = "not exist";
                res.MesasageId = -1;
                return BadRequest(res);
            }
            var result = await _unitofwork.SaveChanges();
            if (result > 0)
            {
                res.Mesasage = "saved successfully";
                res.MesasageId = 1;
                res.Data = new List<Book> { obj };
                return Ok(res);

            }
            res.Mesasage = "failed";
            res.MesasageId = -1;
            return BadRequest(res);



        }
    }
}
