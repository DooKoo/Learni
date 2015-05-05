using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfLearnie
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        User Authorize(string username, string password);//Need a refactor, should to return a User;

        [OperationContract]
        List<Lesson> GetLessons();//void is't acceptable, change to ...;

        [OperationContract]
        bool AddUser(User newUser);

        [OperationContract]
        bool BlockUser(string username);

        [OperationContract]
        bool DeleteUser(string username);

        [OperationContract]
        void AddLesson(Lesson newLesson);

        [OperationContract]
        bool DeleteLesson(string title);

        [OperationContract]
        void AddQuestion(string username, string title, string questionText);

        [OperationContract]
        List<Question> GetQuestions();

        [OperationContract]
        void QuestionAnswer(string title, string answer);

        [OperationContract]
        List<User> GetUsers();
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        //Role: 0-student, 1-teacher, 2-administrator;
        [DataMember]
        public int Role { get; set; }

        //Status: 0-Blocked, 1-Worked;

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public int Progress { get; set; }
    }

    [DataContract]
    public class Lesson
    {
        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string HtmlContent{ get; set; }

        [DataMember]
        public TestQuestion[] Questionnaire;
    }

    [DataContract]
    public class Question
    {
        [DataMember]
        public string Title {get; set;}

        [DataMember]
        public string Text {get; set;}

        [DataMember]
        public string AuthorName {get; set;}

        [DataMember]
        public string Answer {get; set;}
    }
}
