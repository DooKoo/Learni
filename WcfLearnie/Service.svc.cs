using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using System.Xml.Serialization;
using System.Text;

namespace WcfLearnie
{
    public class Service : IService, IDisposable
    {
        [XmlArray("Users"), XmlArrayItem("User")]
        private List<User> _usersList;

        [XmlArray("Lessons"), XmlArrayItem("Lesson")]
        private List<Lesson> _lessonsList;

        [XmlArray("Questions"), XmlArrayItem("Question")]
        private List<Question> _questionsList;

        public Service()
        {
            _usersList = new List<User>();
            _lessonsList = new List<Lesson>();
            _questionsList = new List<Question>();

            XmlSerializer usersXmlSerializer = new XmlSerializer(typeof(List<User>));
            XmlSerializer lessonsXmlSerializer = new XmlSerializer(typeof(List<Lesson>));
            XmlSerializer questionsXmlSerializer = new XmlSerializer(typeof(List<Question>));

            var usersFile = File.OpenRead(@"C:\Users\Shevchuk\Learnie\users.dat");
            var lessonsFile = File.OpenRead(@"C:\Users\Shevchuk\Learnie\lessosns.dat");
            var questionsFile = File.OpenRead(@"C:\Users\Shevchuk\Learnie\questions.dat");

            _usersList = (List<User>)usersXmlSerializer.Deserialize(usersFile);
            _lessonsList = (List<Lesson>)lessonsXmlSerializer.Deserialize(lessonsFile);
            _questionsList = (List<Question>)questionsXmlSerializer.Deserialize(questionsFile);

            usersFile.Close();
            lessonsFile.Close();
            questionsFile.Close();
        }

        public void Dispose()
        {
            XmlSerializer usersXmlSerializer = new XmlSerializer(typeof(List<User>));
            XmlSerializer lessonsXmlSerializer = new XmlSerializer(typeof(List<Lesson>));
            XmlSerializer questionsXmlSerializer = new XmlSerializer(typeof(List<Question>));

            var usersFile = File.Open(@"C:\Users\Shevchuk\Learnie\users.dat", FileMode.Create);
            var lessonsFile = File.Open(@"C:\Users\Shevchuk\Learnie\lessosns.dat", FileMode.Create);
            var questionsFile = File.Open(@"C:\Users\Shevchuk\Learnie\questions.dat", FileMode.Create);

            usersXmlSerializer.Serialize(usersFile, _usersList);
            lessonsXmlSerializer.Serialize(lessonsFile, _lessonsList);
            questionsXmlSerializer.Serialize(questionsFile, _questionsList);

            usersFile.Close();
            lessonsFile.Close();
            questionsFile.Close();
        }

        public User Authorize(string username, string password)
        {
            return _usersList.Find((user) => user.Username == username && user.Password == password);    
        }

        public List<Lesson> GetLessons()
        {
            return _lessonsList;
        }

        public bool AddUser(User newUser)
        {
            if (_usersList.Find((user) => user.Username == newUser.Username) == null)
            {
                _usersList.Add(newUser);
                return true;
            }
            else
                return false;
        }

        public bool BlockUser(string username)//Doesn't work. Need a fix :( i was right;
        {
            bool result = false;
            _usersList.ForEach((user) =>
                {
                    if (user.Username == username)
                    {
                        user.Status = 0;
                        result = true;
                    }
                });
            return result;
        }

        public bool DeleteUser(string username)
        {
            return _usersList.RemoveAll((user) => user.Username == username) > 0 ? true : false;
        }

        public void AddLesson(Lesson newLesson)
        {
            _lessonsList.Add(newLesson);
        }

        public bool DeleteLesson(string title)
        {
            return _lessonsList.RemoveAll((lesson) => lesson.Title == title) > 0 ? true : false;
        }

        public void AddQuestion(string username, string title, string questionText)
        {
            _questionsList.Add(new Question
                {
                    Title = title,
                    Text = questionText,
                    AuthorName = username
                });
        }

        public List<Question> GetQuestions()
        {
            return _questionsList;
        }

        public void QuestionAnswer(string title, string answer)
        {
            _questionsList.Find((question) => question.Title == title).Answer = answer;
        }

        public List<User> GetUsers() 
        {
            return _usersList;
        }
    }
}
