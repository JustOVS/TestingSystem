﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
using System.Text;
using TestingSystem.Data.DTO;
using TestingSystem.Data.StoredProcedure.CRUD;
using TestingSystem.Data;
using TestingSystem.API.Models.Input;
using TestingSystem.API.Models.Output;
using Newtonsoft.Json.Serialization;
using Microsoft.VisualBasic;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;

namespace TestingSystem.API.Controllers
{
        [ApiController]
        [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ILogger<GroupController> _logger;

        public UserController(ILogger<GroupController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public List<UserOutputModel> Get()
        //{
        //    List<UserOutputModel> allUsers = new List<UserOutputModel>();
        //    Map mapper = new Map();
        //    AdminDataAccess adm = new AdminDataAccess();
        //    foreach(UserDTO user in adm.GetAllUsers())
        //    {
        //        allUsers.Add(mapper.ConvertUserDTOToUserOutputModel(user));
        //    }
        //    return allUsers;
        //}

        [HttpPost]
        public IActionResult PostUser([FromBody] UserInputModel user) // нужно разобраться с ошибкой
        {
            UserMapper mapper = new UserMapper();
            AdminDataAccess adm = new AdminDataAccess();
            adm.UserCreate(mapper.ConvertUserInputModelToUserDTO(user));
            return new OkObjectResult("Пользователь создан успешно");
        }
        
        [HttpPost("role")]
        public IActionResult PostUserRole([FromBody] UserRoleInputModel userRole) // добавить условие, если роли не существует, иначе если данная роль для этого пользователя уже создана
        {
            UserMapper mapper = new UserMapper();
            AdminDataAccess adm = new AdminDataAccess();
            adm.UserRoleCreate(mapper.ConvertUserRoleInputModelToUserRoleDTO(userRole));
            return new OkObjectResult("Роль пользователя создана");
        }
        
        [HttpGet("role")]
        public IActionResult GetRole()// добавить условие, если ролей нет
        {
            UserMapper mapper = new UserMapper();
            AdminDataAccess adm = new AdminDataAccess();
            List<RoleDTO> roles = adm.GetRole();
            List<RoleOutputModel> rolesOut = new List<RoleOutputModel>();
            foreach (RoleDTO r in roles)
            {
                rolesOut.Add(mapper.ConvertRoleDTOToRoleOutputModel(r));
            }

            return Json(rolesOut);
        }
        
        [HttpGet("{userId}/role")]
        public IActionResult GetRoleByUserId(int userId) // спросить про условие, если пользователя не существует
        {
            UserMapper mapper = new UserMapper();
            AdminDataAccess adm = new AdminDataAccess();
            List<RoleDTO> roles = adm.GetRoleByUserId(userId);
            List<RoleOutputModel> rolesOut = new List<RoleOutputModel>();
            foreach (RoleDTO r in roles)
                {
                    rolesOut.Add(mapper.ConvertRoleDTOToRoleOutputModel(r));
                }
                return Json(rolesOut);
        }
        
        [HttpGet]
        public IActionResult GetAllUsersWithRoles() // нужно ли условие, если таблица пуста
        {
            AdminDataAccess adm = new AdminDataAccess();
            List<UserPositionDTO> users = adm.GetAllUsersWithRoles();
            UserMapper mapper = new UserMapper();
            return Json(mapper.ConvertUserPositionDTOsToUserWithRolesOutputModels(users));
        }
        
        
        [HttpGet("role/{roleID}")]
        public IActionResult GetUsersByRoleID(int roleID) // спросить про условие, если такой роли не существует
        {
            AdminDataAccess adm = new AdminDataAccess();
            List<UserOutputModel> allUsers = new List<UserOutputModel>();
            UserMapper mapper = new UserMapper();
            
            foreach(UserDTO user in adm.GetUsersByRoleID(roleID))
            {
                allUsers.Add(mapper.ConvertUserDTOToUserOutputModel(user));
            }
            return Json(allUsers);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id) // спросить про условие, если такого пользователя не существует
        {
            UserMapper mapper = new UserMapper();
            AdminDataAccess adm = new AdminDataAccess();
            UserOutputModel user = new UserOutputModel();
            UserDTO getUser = adm.GetUserbyID(id);
            if (getUser == null) { return new BadRequestObjectResult("Пользователя с таким id не существует"); }
            else {
                user = mapper.ConvertUserDTOToUserOutputModel(getUser);
                return Json(user); 
            }
        }

        [HttpPut]
        public IActionResult Put([FromBody] UserInputModel user)
        {
            UserMapper mapper = new UserMapper();
            AdminDataAccess adm = new AdminDataAccess();
            int result = adm.UserUpdate(mapper.ConvertUserInputModelToUserDTO(user));
            if (result == 0)
            {
                return new BadRequestObjectResult("Такого пользователя не существует");
            }
            if (result == 1)
            {
                return new OkObjectResult("Пользователь обновлён");
            }
            else
            {
                return new BadRequestObjectResult("Ошибка базы данных");
            }
        }
        
        [HttpDelete("{userId}/role/{roleId}")]
        public IActionResult DeleteUserRole(int userId, int roleId) // условие, если юзера нет, то юзер не найден, иначе если роли нет, то роль не найдена
        {
            AdminDataAccess adm = new AdminDataAccess();
            adm.UserRoleDelete(userId, roleId);
            return new OkObjectResult("Роль пользователя удалена");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) // условие, если пользователя нет, то пользователь не найден
        {
            AdminDataAccess adm = new AdminDataAccess();
            adm.UserDelete(id);
            return new OkObjectResult("Пользователь удалён");
        }
        
        [HttpGet("{UserID}/test")]
        public IActionResult GetStudentTests(int UserID) // добавить условие, что если ни одного теста не пройдено, то вывести OkObjectResult("Нет назначенных или пройденных тестов")
        {
            StudentDataAccess student = new StudentDataAccess();
            Mapper mapper = new Mapper();
            List<TestAttemptDTO> tests = student.GetCompleteTest(UserID);
            tests.AddRange(student.GetIncompleteTest(UserID));
            StudentOutputModel model = mapper.ConvertUserDTOTestAttemptDTOToStudentModel(student.GetUser(UserID), mapper.ConvertTestAttemptDTOToTestAttemptModel(tests));
            return Json(model);
        }
        [HttpGet("{UserID}/test/{TestID}")] // добавить условия, если id пользователя не существует, иначе если id теста не существует
        public IActionResult GetAttemptsByUserIDTestID(int UserID, int TestID)
        {
            StudentDataAccess student = new StudentDataAccess();
            Mapper mapper = new Mapper();
            UserIdTestIdDTO dTO = new UserIdTestIdDTO(UserID, TestID);
            List<AttemptResultOutputModel> model = mapper.ConvertAttemptDTOToAttemptModel(student.GetAttemptsByUserIdTestId(dTO));
            return Json(model);
        }
        [HttpGet("Attempt/{AttemptID}")] // не понятно что делает метод и зачем он
        public IActionResult GetQuestionAndAnswerByAttemptID(int attemptID)
        {
            TeacherDataAccess teacher = new TeacherDataAccess();
            Mapper mapper = new Mapper();
            List<QuestionAnswerOutputModel> model = mapper.ConvertQuestionAnswerDTOToQuestionAnswerModel(teacher.GetQuestionAndAnswerByAttempt(attemptID));
            return Json(model);
        }
    }
}
