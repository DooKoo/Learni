using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceProxy.LearnieService;


namespace ServiceProxy
{
    public class ProxyService:IService
    {
        private readonly ServiceClient _serviceClient = new ServiceClient();
        public User Authorize(string username, string password)
        {
            _serviceClient.Open();
            User result = _serviceClient.Authorize(username, password);
            _serviceClient.Close();
            return result;
        }

        public Lesson[] GetLessons()
        {
            _serviceClient.Open();
            Lesson[] result = _serviceClient.GetLessons();
            _serviceClient.Close();
            return result;
        }

        public bool AddUser(User newUser)
        {
            _serviceClient.Open();
            bool result = _serviceClient.AddUser(newUser);
            _serviceClient.Close();
            return result;
        }

        public bool BlockUser(string username)
        {
            _serviceClient.Open();
            bool result = _serviceClient.BlockUser(username);
            _serviceClient.Close();
            return result;
        }

        public bool DeleteUser(string username)
        {
            _serviceClient.Open();
            bool result = _serviceClient.BlockUser(username);
            _serviceClient.Close();
            return result;
        }

        public void AddLesson(Lesson newLesson)
        {
            _serviceClient.Open();
            _serviceClient.AddLesson(newLesson);
            _serviceClient.Close();
        }

        public bool DeleteLesson(string title)
        {
            _serviceClient.Open();
            bool result = _serviceClient.DeleteLesson(title);
            _serviceClient.Close();
            return result;
        }

        public void AddQuestion(string username, string title, string questionText)
        {
            _serviceClient.Open();
            _serviceClient.AddQuestion(username, title, questionText);
            _serviceClient.Close();
        }

        public Question[] GetQuestions()
        {
            _serviceClient.Open();
            Question[] result = _serviceClient.GetQuestions();
            _serviceClient.Close();
            return result;
        }

        public void QuestionAnswer(string title, string answer)
        {
            _serviceClient.Open();
            _serviceClient.QuestionAnswer(title, answer);
            _serviceClient.Close();
        }

        public User[] GetUsers()
        {
            _serviceClient.Open();
            User[] result = _serviceClient.GetUsers();
            _serviceClient.Close();
            return result;
        }

        #region NotImplemented Async Methods
        public Task<User[]> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public Task QuestionAnswerAsync(string title, string answer)
        {
            throw new NotImplementedException();
        }

        public Task<Question[]> GetQuestionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddQuestionAsync(string username, string title, string questionText)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteLessonAsync(string title)
        {
            throw new NotImplementedException();
        }

        public Task AddLessonAsync(Lesson newLesson)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUserAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> BlockUserAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AddUserAsync(User newUser)
        {
            throw new NotImplementedException();
        }

        public Task<Lesson[]> GetLessonsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User> AuthorizeAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
