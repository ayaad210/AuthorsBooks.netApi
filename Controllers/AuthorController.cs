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
    public class AuthorController : ControllerBase
    {
        private readonly IMapper _mapper;
     private readonly   IUnitOfWork _unitofwork;
        private readonly IAuthorRepo _authorRepo;
        public AuthorController( IMapper mapper,IUnitOfWork unitofwork)

        {
            _mapper = mapper;
            _unitofwork = unitofwork;
            _authorRepo = unitofwork.GetAuthorRepo();
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public async Task< ActionResult<IEnumerable<ApiResponse<Author>>>> GetAll()
        {
            ApiResponse<Author> res = new ApiResponse<Author>();
            res.Data= await _authorRepo.GetAuthors();

            return    Ok(res);
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<Author>>> GetById(int id)
        {
            ApiResponse<Author> res = new ApiResponse<Author>();
            res.Data = new List<Author> { await _authorRepo.GetAuthorById(id) };
            return Ok(res );
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<ActionResult<ApiResponse<Author>>> Post([FromBody] AuthorCreate Createmodel)
        {
                ApiResponse<Author> res = new ApiResponse<Author>();

            Author model= _mapper.Map<Author>(Createmodel);
            await  _authorRepo.CreateAuthor(model);
            var result= await _unitofwork.SaveChanges();
            if (result>0)
            {
                res.Mesasage = "saved successfully";
                res.MesasageId = 1;
                res.Data = new List<Author> { model };
                return Ok(res);
               
            }
            res.Mesasage = "failed";
            res.MesasageId = -1;
            return BadRequest(res);
        }

        // PUT api/<AuthorController>/5
        [HttpPut]
        public async Task<ActionResult<ApiResponse<Author>>>  Put([FromBody] Author author)
        {

            ApiResponse<Author> res = new ApiResponse<Author>();

           var obj=  await _authorRepo.UpdateAuthor(author);
            if (obj==null)
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
                res.Data = new List<Author> { author };
                return Ok(res);

            }
            res.Mesasage = "failed";
            res.MesasageId = -1;
            return BadRequest(res);
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<Author>>> Delete(int id)
        {

            ApiResponse<Author> res = new ApiResponse<Author>();

          Author obj= await  _authorRepo.DeleteAuthor(id);
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
                res.Data = new List<Author> { obj };
                return Ok(res);

            }
            res.Mesasage = "failed";
            res.MesasageId = -1;
            return BadRequest(res);


           
        }
    }
}
