using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Homework_22_API.Models;

namespace Homework_22_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiaryController : ControllerBase
    {
        DiaryContext _db;

        public DiaryController(DiaryContext db)
        {
            _db = db;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Hello world!");
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            return Ok(id);
        }
    }
}
