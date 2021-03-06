﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TestingSystem.Data.DTO;

namespace TestingSystem.Data.StoredProcedure.CRUD
{
    public class TestCRUD
    {
        public int Add(TestDTO test)
        {
            var connection = Connection.GetConnection();
            connection.Open();
            string sqlExpression = "Test_Add @name, @durationTime, @successScore, @questionNumber";
            return connection.Query<int>(sqlExpression, test).FirstOrDefault();            
        }

        public List<TestDTO> GetAll()
        {
            var connection = Connection.GetConnection();            
            string sqlExpression = "Test_GetAll";        
            return connection.Query<TestDTO>(sqlExpression).ToList();            
        }

        public TestDTO GetById(int id)
        {
            var connection = Connection.GetConnection();            
            string sqlExpression = "Test_GetById";            
            return connection.Query<TestDTO>(sqlExpression, new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();            
        }

        public int Update(TestDTO test)
        {
            var connection = Connection.GetConnection();
            string sqlExpression = "Test_Update";
            connection.Execute(sqlExpression, test, commandType: CommandType.StoredProcedure);
            return test.ID;
        }

        public int Delete(int id)
        {
            var connection = Connection.GetConnection();
            string sqlExpression = "Test_Delete";
            connection.Execute(sqlExpression, new { id }, commandType: CommandType.StoredProcedure);
            return id;
        }
    }
}
