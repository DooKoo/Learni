using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfLearnie
{
    public class TestQuestion
    {
        public string Text { get; set; }
        public string[] Answers { get; set; }
        public int Correct { get; set; }
    }
}