﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestingSystem.Data;

namespace TestingSystem.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DeletedController : Controller
    {
        private readonly ILogger<DeletedController> _logger;

        public DeletedController(ILogger<DeletedController> logger)
        {
            _logger = logger;
        }
        [HttpGet("Users")]
        public IActionResult GetDeletedUsers()
        {
            AdminDataAccess admin = new AdminDataAccess();
            UserMapper mapper = new UserMapper();
            return Ok(mapper.ConvertListUserDTOToListUserOutputModel(admin.GetDeletedUsers()));
        }
        [HttpGet("Tests")]
        public IActionResult GetDeletedTests()
        {
            AdminDataAccess admin = new AdminDataAccess();
            Mapper mapper = new Mapper();
            return Ok(mapper.ConvertTestQuestionTagDTOToTestOutputListModel(admin.GetDeletedTests()));
        }
        [HttpGet("Questions")]
        public IActionResult GetDeletedQuestions()
        {
            AdminDataAccess admin = new AdminDataAccess();
            Mapper mapper = new Mapper();
            return Ok(mapper.ConvertQuestionDTOToQuestionModelList(admin.GetDeletedQuestions()));
        }
        [HttpGet("Groups")]
        public IActionResult GetDeletedGroups()
        {
            AdminDataAccess admin = new AdminDataAccess();
            Mapper mapper = new Mapper();
            //return Json(mapper.ConvertGroupDTOToListGroupOutputModel(admin.GetDeletedGroups()));
            return Ok(mapper.ConvertGroupWithStudentsAndTeachersDTOToGroupOutputModel(admin.GetDeletedGroups()));
        }
        [HttpPut("User/{id}")]
        public IActionResult RestoreUser(int id)
        {
            AdminDataAccess admin = new AdminDataAccess();
            return Ok(admin.RestoreUser(id));
        }
        [HttpPut("Test/{id}")]
        public IActionResult RestoreTest(int id)
        {
            AdminDataAccess admin = new AdminDataAccess();
            return Ok(admin.RestoreTest(id));
        }
        [HttpPut("Question/{id}")]
        public IActionResult RestoreQuestion(int id)
        {
            AdminDataAccess admin = new AdminDataAccess();
            return Ok(admin.RestoreQuestion(id));
        }
        [HttpPut("Group/{id}")]
        public IActionResult RestoreGroup(int id)
        {
            AdminDataAccess admin = new AdminDataAccess();
            return Ok(admin.RestoreGroup(id));
        }
    }
}
