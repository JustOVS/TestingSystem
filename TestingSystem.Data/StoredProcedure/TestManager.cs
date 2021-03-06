﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using TestingSystem.Data.DTO;
using TestingSystem.Data;


namespace TestingSystem.Data.StoredProcedure
{
    public class TestManager
    {
        public List<TestDTO> GetLateAttempt(int userId)//просроченные тесты студента
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "Test_Attempt_GetLate @UserID";
                return connection.Query<TestDTO>(sqlExpression,new { userId }).ToList();
            }
        }
        
        public List<QuestionAnswerDTO> GetCorrectAnswerByTestID(int testId)//нахождение правильных ответов теста
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "Answers_GetCorrectByTestId @testId";
                return connection.Query<QuestionAnswerDTO>(sqlExpression,new { testId }).ToList();
            }
        }
        
        public List<QuestionAnswerDTO> GetQuestionAndAnswerFromAttempt(int attemptId)//все вопросы и ответы попытки
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "Attempt_GetQuestionAndAnswer @AttemptID";
                return connection.Query<QuestionAnswerDTO>(sqlExpression,new{ attemptId }).ToList();
            }
        }

        public int DeleteConcreteAttempt(AttemptDTO attempt)//удаление попытки 
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "Attempt_DeleteConcrete @UserID,@TestID,@Number";
                return connection.Execute(sqlExpression, attempt);
            }
        }

        public List<TagDTO> GetTestTags(int testId)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "GetTestTags";
                return connection.Query<TagDTO>(sqlExpression, new { testId }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<TagDTO> GetTagsWhichAreNotInTest(int testId)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "Tags_GetWhichAreNotInTest";
                return connection.Query<TagDTO>(sqlExpression, new {testId}, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<TestDTO> GetTestByTagpAndGroup(TagGroupDTO dto)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "Test_GetByTagNameGroupId @Tag_Name @Group_ID";
                return connection.Query<TestDTO>(sqlExpression, dto, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<TestDTO> GetTestByGroupId(int groupId)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "GetTestsByGroupId";
                return connection.Query<TestDTO>(sqlExpression, new { groupId }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<TestDTO> GetTestVSTagSearchOr(params string[] tag)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "SearchTestByTagOr";
                return connection.Query<TestDTO>(sqlExpression, new { tag }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<TestDTO> GetTestVSTagSearchAnd(params string[] tag)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "SearchTestByTagAnd";
                return connection.Query<TestDTO>(sqlExpression, new { tag }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public List<TestAttemptDTO> GetCompletedTestsByUserID(int userId)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "GetCompletedTestsByUserID";
                return connection.Query<TestAttemptDTO>(sqlExpression, new { userId }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void DeleteTest(int Id)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "DeleteTest";
                connection.Execute(sqlExpression, Id, commandType: CommandType.StoredProcedure);
            }
        }

        public List<StudentsVSTestsDTO> GetBestResultOfTestByGroupID(int groupId)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "Test_GetÍBestGroupResult @GroupID";
                return connection.Query<StudentsVSTestsDTO>(sqlExpression, new { groupId }).ToList();
            }
        }

        public List<TestQuestionsDTO> GetTestWithAllQuestionsById (int testId)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "GetAllQuestionsByTestID";
                return connection
                    .Query<TestQuestionsDTO>(sqlExpression, new {testId}, commandType: CommandType.StoredProcedure)
                    .ToList();
            }
        }

        public List<AnswerDTO> GetAllAnswersInTest(int testId)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "AnswersAll_GetByTestId";
                return connection
                    .Query<AnswerDTO>(sqlExpression, new {testId}, commandType: CommandType.StoredProcedure).ToList();
            }
        }



        public TestDTO GetDurationAndQuestionNumber(int id)
        {
            using (IDbConnection connection = Connection.GetConnection())
            {
                string sqlExpression = "Test_GetDurationAndQuestionNumber";
                return connection.Query<TestDTO>(sqlExpression, new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public List<QuestionWithListAnswersDTO> GetQuestionsAndAnswers(int testID)
        {
            IDbConnection connection = Connection.GetConnection();
            List<QuestionWithListAnswersDTO> questions;
            using (connection)
            {
                var questionDictionary = new Dictionary<int, QuestionWithListAnswersDTO>();
                connection.Query<QuestionWithListAnswersDTO, AnswerWithoutCorrectnessDTO, QuestionWithListAnswersDTO>(
                    "GetAllQuestionsAndAnswersByTestId1",
                    (question, answers) =>
                    {
                        QuestionWithListAnswersDTO questionEntry;

                        if (!questionDictionary.TryGetValue(question.Id, out questionEntry))
                        {
                            questionEntry = question;
                            questionEntry.Answers = new List<AnswerWithoutCorrectnessDTO>();
                            questionDictionary.Add(questionEntry.Id, questionEntry);
                        }

                        questionEntry.Answers.Add(answers);
                        return questionEntry;
                    },
                    new { testID },
                    splitOn: "QuestionId",
                    commandType: CommandType.StoredProcedure)
                .ToList();
                questions = new List<QuestionWithListAnswersDTO>(questionDictionary.Values);
            }
            return questions;
        }
        
        public TestQuestionTagDTO GetTestWithQuestionsAndTagsByID(int id)
        {
            using (var connection = Connection.GetConnection())
            {
                TestQuestionTagDTO result = null; 
                string sqlExpression = "GetTestsByIdOneToMany";
                connection.Query<TestQuestionTagDTO, QuestionForOneToManyDTO, TagWithTestIDDTO, TestQuestionTagDTO>(sqlExpression, (test, question, tag) =>
                {
                    if (result == null)
                    {
                        result = test;
                        result.Questions = new List<QuestionForOneToManyDTO>();
                        result.Questions.Add(question);
                        result.Tags = new List<TagWithTestIDDTO>();
                        result.Tags.Add(tag);
                        
                    }
                    else
                    {
                        if (!result.Questions.Any(x=>x.Value==question.Value))
                        { result.Questions.Add(question); }
                        if (!result.Tags.Any(x=>x.Name==tag.Name))
                        {result.Tags.Add(tag);}
                    }
                    
                    return result;
                }
                ,new { id }
            , splitOn: "TestID,IDtest",commandType:CommandType.StoredProcedure);
                return result;
            }
        }

        public UserTestWithQuestionsAndAnswersDTO GetUserResultByUserIDAndTestID(int userID,int testID)
        {
            UserTestWithQuestionsAndAnswersDTO result = null;
            using (var connection = Connection.GetConnection())
            {
                string sqlExpression = "GetUserResultByUserIDTestID";
                connection.Query<UserTestWithQuestionsAndAnswersDTO, AnswerDTO, QuestionWithAnswersDTO, UserTestWithQuestionsAndAnswersDTO>(sqlExpression, (res,answer, question) =>
                {
                    if(result == null)
                    {
                        result = res;
                        result.Questions = new List<QuestionWithAnswersDTO>();
                        question.Answers = new List<AnswerDTO>();
                        question.Answers.Add(answer);
                        result.Questions.Add(question);
                    }
                    if(!result.Questions.Any(x=>x.IDQuestion == question.IDQuestion))
                    {
                        question.Answers = new List<AnswerDTO>();
                        result.Questions.Add(question);
                    }
                    if(!result.Questions.Any(x=>x.Answers.Any(y=>y.ID==answer.ID)))
                    {
                        int id = result.Questions.FindIndex(x => x.IDQuestion == answer.QuestionID);
                        if(id>-1)
                        result.Questions[id].Answers.Add(answer);
                    }
                    return result;
                }
                , new { userID, testID }, splitOn: "Id,IDQuestion", commandType: CommandType.StoredProcedure);
            }
            return result;
        }

        public TestWithStudentsDTO GetLateStudentsByTestID(int id)
        {
            TestWithStudentsDTO result = null;
            using(var connection = Connection.GetConnection())
            {
                string sqlExpression = "Attepmt_GetLateStudentByTestID";
                connection.Query<TestDTO, UserDTO, TestWithStudentsDTO>(sqlExpression, (test, student) =>
                  {
                      if (result == null)
                      {
                          result = new TestWithStudentsDTO();
                          //result.Test = new TestDTO();
                          result.Test = test;
                          result.Students = new List<UserDTO>();
                          result.Students.Add(student);
                      }
                      else
                      {
                          result.Students.Add(student);
                      }
                      return result;
                  }
                , new { id }, splitOn: "ID", commandType: CommandType.StoredProcedure);
            }
            return result;
        }
    }
}
