using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_for_colledg.models
{
    internal class Question
    {
        private int rightAnswer1;
        private string[] options1;
        private string title1;

        public String title { get => title1; set => title1=value; }
        public String[] options { get => options1; set => options1=value; }
        public int rightAnswer { get => rightAnswer1; set => rightAnswer1=value; }

        /*public Question(String title, String[] options, int rightAnswer)
        {
            this.title = title;
            this.options = options;
            this.rightAnswer = rightAnswer;
        }*/

    }
}
