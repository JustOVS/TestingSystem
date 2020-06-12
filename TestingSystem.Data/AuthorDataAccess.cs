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
        //Запросы на основной странице "Тест" (список тестов/список фидбэков/список тэгов)

        public List<TestDTO> GetAllTest()       //вывод списка всех тестов
        {
            TestCRUD tests = new TestCRUD();
            return tests.GetAll();
        }

        public List<SearchTestByTagDTO> GetTestVSTagSearchOr(params string[] tag)    //ищем тесты по тэгу(или)
        {
            TestManager tests = new TestManager();
            return tests.GetTestVSTagSearchOr(tag);
        }

        public List<SearchTestByTagDTO> GetTestVSTagSearchAnd(params string[] tag)     //ищем тесты по тэгу(и)
        {
            TestManager tests = new TestManager();
            return tests.GetTestVSTagSearchAnd(tag);
        }

        public List<FeedbackDTO> GetProcessedFeedbacks()       //список обработанных фидбэков (сортировка по дате убыв.)
        {
            FeedbackManager feedbacks = new FeedbackManager();
            return feedbacks.GetProcessedFeedback();
        }

        public List<FeedbackDTO> GetNotProcessedFeedbacks()    //список необработанных фидбэков (сортировка по дате)
        {
            FeedbackManager feedbacks = new FeedbackManager();
            return feedbacks.GetNotProcessedFeedback();
        }

        public List<FeedbackDTO> GetAllFeedbacks()       //список всех фидбэков (сортировка по дате убыв.)
        {
            FeedbackCRUD feedbacks = new FeedbackCRUD();
            return feedbacks.FeedbackGetAll();
        }

        public List<FeedbackDTO> GetFeedbackByTest(int testId)    //список фидбэков конкретного теста (сортировка по дате убыв.)
        {
            FeedbackManager feedbacks = new FeedbackManager();
            return feedbacks.GetFeedbackByTest(testId);
        }

        public List<FeedbackDTO> GetFeedbackByDate(DateTime dateTime1, DateTime dateTime2)  //список фидбэков конкретных дат (сортировка по дате)
        {
            FeedbackManager feedbacks = new FeedbackManager();
            return feedbacks.GetFeedbackByDate(dateTime1, dateTime2);
        }

        public List<TagDTO> GetAllTag()     //список всех тэгов
        {
            TagCRUD tags = new TagCRUD();
            return tags.GetAll();
        }

        public int AddTag(TagDTO tg)       //создать тэг
        {
            TagCRUD tag = new TagCRUD();
            return tag.Add(tg);
        }

        public void UpdateTag(TagDTO tag)   //редактировать тэг
        {
            TagCRUD tg = new TagCRUD();
            tg.Update(tag);
        }

        public void DeleteTag(int id)       //удалить тэг
        {
            TagCRUD tag = new TagCRUD();
            tag.Delete(id);
        }











        //AnswerCRUD 

        public int AddAnswer(AnswerDTO answer)
        {
            AnswerCRUD answr = new AnswerCRUD();
            return answr.AnswerAdd(answer);
        }
       
        public List<AnswerDTO> GetAnswerByQuestionId(int questonId)
        {
            AnswerCRUD answr = new AnswerCRUD();
            return answr.AnswerGetByQuestionId(questonId);
        }

        public void UpdateAnswer(AnswerDTO answer)
        {
            AnswerCRUD answr = new AnswerCRUD();
            answr.Update(answer);
        }

        public void DeleteAnswer(int id)
        {
            AnswerCRUD answr = new AnswerCRUD();
            answr.Delete(id);
        }


        //Test_TegCRUD

        public int TestTagCreate(TestTagDTO testtag)
        {
            TestTagCRUD tt = new TestTagCRUD();
            return tt.Add(testtag);
        }

        public void Update(TestTagDTO testtag)
        {
            TestTagCRUD tt = new TestTagCRUD();
            tt.Update(testtag);
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


        //FeedbackCRUD        

        


        //QuestionCRUD

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

        public QuestionDTO GetQuestionById(int id)
        {
            QuestionCRUD q = new QuestionCRUD();
            return q.GetById(id);
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

        public void UpdateQuestion(QuestionDTO question)
        {
            QuestionCRUD q = new QuestionCRUD();
            q.Update(question);
        }        


        //TypeCRUD 

        //public int AddType(TypeDTO type)
        //{
        //    TypeCRUD typeCRUD = new TypeCRUD();
        //    return typeCRUD.Add(type);
        //}

        //public List<TypeDTO> GetAllTypes()
        //{
        //    TypeCRUD typeCRUD = new TypeCRUD();
        //    return typeCRUD.GetAll();
        //}        

        //public void UpdateType(TypeDTO type)
        //{
        //    TypeCRUD typeCRUD = new TypeCRUD();
        //    typeCRUD.Update(type);
        //}

        //public void DeleteType(TypeDTO type)
        //{
        //    TypeCRUD typeCRUD = new TypeCRUD();
        //    typeCRUD.Delete(type.ID);
        //}
        

        //TestCRUD

        public int AddTest(TestDTO test)
        {
            TestCRUD ts = new TestCRUD();
            return ts.Add(test);
        }

        

        public TestDTO GetByIdTest(int id)
        {
            TestCRUD ts = new TestCRUD();
            return ts.GetById(id);
        }

        public int UpdateTest(TestDTO test)
        {
            TestCRUD ts = new TestCRUD();
            return ts.Update(test);
        }

        public int DeleteTest(int id) 
        {
            TestCRUD dt = new TestCRUD();
            return dt.Delete(id);
        }


        //TagCRUD
        public int AddTag(TagDTO tag)
        {
            TagCRUD tg = new TagCRUD();
            return tg.Add(tag);
        }

       
        public void UpdateTag(TagDTO tag)
        {
            TagCRUD tg = new TagCRUD();
            tg.Update(tag);
        }

        public void DeleteTag(int id)
        {
            TagCRUD tg = new TagCRUD();
            tg.Delete(id);
        }


        //From FeedbackManager

        public int UpdateProcessedInFeedback(int id)
        {
            FeedbackManager feedback = new FeedbackManager();
            return feedback.UpdateProcessedInFeedback(id);
        }

        


        //From TestManager

        

        public List<TagDTO> GetTagsInTest(TestDTO tests)
        {
            TestManager test = new TestManager();
            return test.GetTestTags(tests);
        }

        public void DeleteTest(TestDTO test)
        {
            TestManager tm = new TestManager();
            tm.DeleteTest(test.ID);
        }

        public List<TagDTO> GetTagsWhichAreNotInTest(int testId)
        {
            TestManager tags = new TestManager();
            return tags.GetTagsWhichAreNotInTest(testId);
        }


        //From QuestionManager

        public int DeleteQuestionFromTest(int questionId)
        {
            QuestionManager question = new QuestionManager();
            question.DeleteQuestionFromTest(questionId);
            return questionId;
        }


        //From AttemptManager


    }
}
