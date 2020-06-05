﻿using System;
using System.Collections.Generic;
using System.Text;
using TestingSystem.Data.DTO;
using TestingSystem.Data.StoredProcedure;
using TestingSystem.Data.StoredProcedure.CRUD;

namespace TestingSystem.Data
{
    public class AuthorDataAccess
    {
        public void QuestionDeleteFromTest(int questionId)
        {
            QuestionManager question = new QuestionManager();
            question.DeleteQuestionFromTest(questionId);
        }

        public List<Question_AnswerDTO> AnswerGetCorrectByTestID(TestDTO test)
        {
            TestManager managerAnswer = new TestManager();
            return managerAnswer.Answer_GetCorrectByTestID(test);
        }


        //Test_Teg
        public int TestTagCreate(TestTagDTO testtag)
        {
            TestTagCRUD tt= new TestTagCRUD();
            return tt.Add(testtag);
        }


        public List<TestTagDTO> GetAll() 
        {
            TestTagCRUD tt = new TestTagCRUD();
            return tt.GetAll();
        }

        public TestTagDTO GetById(int id) 
        {
            TestTagCRUD tt = new TestTagCRUD();
            return tt.GetById(id);
        }

        public List<TestTagDTO> GetByTagId(int tagId) 
        {
            TestTagCRUD tt = new TestTagCRUD();
            return tt.GetByTagId(tagId);
        }

        public List<TestTagDTO> GetByTestId(int testId) 
        {
            TestTagCRUD tt = new TestTagCRUD();
            return tt.GetByTestId(testId);
        }

        public TestTagDTO GetByTestIdTagId(int testId, int tagId) 
        {
            TestTagCRUD tt = new TestTagCRUD();
            return tt.GetByTestIdTagId(testId, tagId);
        }

        public void Update(TestTagDTO testtag) 
        {
            TestTagCRUD tt = new TestTagCRUD();
            tt.Update(testtag);
        }

        public void DeleteById(int id) 
        {
            TestTagCRUD tt = new TestTagCRUD();
            tt.DeleteById(id);
        }

        public void DeleteByTagId(int tagId) 
        {
            TestTagCRUD tt = new TestTagCRUD();
            tt.DeleteByTagId(tagId);
        }

        public void DeleteByTestId(int testId) 
        {
            TestTagCRUD tt = new TestTagCRUD();
            tt.DeleteByTestId(testId);
        }

        public void DeleteByTestIdTagId(int testId, int tagId) 
        {
            TestTagCRUD tt = new TestTagCRUD();
            tt.DeleteByTestIdTagId(testId, tagId);
        }


        //Feedback
        public int AddFeedback(FeedbackDTO feedback)
        {
            FeedbackCRUD fb = new FeedbackCRUD();
            return fb.FeedbackAdd(feedback);
        }

        public List<FeedbackDTO> GetAllFeedback()
        {
            FeedbackCRUD fb = new FeedbackCRUD();
            return fb.FeedbackGetAll();
        }

        public void UpdateFeedback(FeedbackDTO feedback)
        {
            FeedbackCRUD fb = new FeedbackCRUD();
            fb.FeedbackUpdate(feedback);
        }

        public void DeleteFeedback(FeedbackDTO feedback)
        {
            FeedbackCRUD fb = new FeedbackCRUD();
            fb.FeedbackDelete(feedback);
        }


        // Question CRUD methods

        public int AddQuestion(QuestionDTO question)
        {
            QuestionCRUD q = new QuestionCRUD();
            return q.Add(question);
        }

        public List<QuestionDTO> GetAllQuestions()
        {
            QuestionCRUD q = new QuestionCRUD();
            return q.GetAll();
        }

        public List<QuestionDTO> GetQuestionsByTestID(int testId)
        {
            QuestionCRUD q = new QuestionCRUD();
            return q.GetByTestID(testId);
        }

        public List<QuestionDTO> GetQuestionsByTypeID(int typeId)
        {
            QuestionCRUD q = new QuestionCRUD();
            return q.GetByTypeID(typeId);
        }

        public QuestionDTO GetQuestionsByTagID(int id)
        {
            QuestionCRUD q = new QuestionCRUD();
            return q.GetById(id);
        }

        public void UpdateQuestion(QuestionDTO question)
        {
            QuestionCRUD q = new QuestionCRUD();
            q.Update(question);
        }

        public void DeleteFeedback(QuestionDTO question)
        {
            QuestionCRUD q = new QuestionCRUD();
            q.Delete(question.ID);
        }

        // Type CRUD methods

        public int AddType(TypeDTO type)
        {
            TypeCRUD typeCRUD = new TypeCRUD();
            return typeCRUD.Add(type);
        }

        public List<TypeDTO> GetAllTypes()
        {
            TypeCRUD typeCRUD = new TypeCRUD();
            return typeCRUD.GetAll();
        }

        public TypeDTO GetTypesByTagID(int id)
        {
            TypeCRUD typeCRUD = new TypeCRUD();
            return typeCRUD.GetById(id);
        }

        public void UpdateType(TypeDTO type)
        {
            TypeCRUD typeCRUD = new TypeCRUD();
            typeCRUD.Update(type);
        }

        public void DeleteType(TypeDTO type)
        {
            TypeCRUD typeCRUD = new TypeCRUD();
            typeCRUD.Delete(type.ID);
        }

        public List<FeedbackSortByDataTimeDTO> feedbackSortByDataTime(QuestionDTO question)
        {
            FeedbackManager fb = new FeedbackManager();
            return fb.FeedbackSortByDataTime(question);
        
        }

        public List<SearchTestByTagDTO> GetTestVSTagSearchOr(string tag1, string tag2, string tag3) 
        {
            TestManager tm = new TestManager();
            return tm.GetTestVSTagSearchOr(tag1, tag2, tag3);
        
        }

        public List<SearchTestByTagDTO> GetTestVSTagSearchAnd(string tag1, string tag2, string tag3)
        {
            TestManager tm = new TestManager();
            return tm.GetTestVSTagSearchAnd(tag1, tag2, tag3);
        }


        //var connect = Connection.GetSqlConnection();
        //User user = new User();
        //user.User_Create(connect, userC);

    }
}
