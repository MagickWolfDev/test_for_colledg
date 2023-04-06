using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test_for_colledg.models
{
    internal class TestDataModal
    {
        public String title { get; set; }
        public Question[] questions { get; set; }

        public TestDataModal(String title)
        {
            this.title = title;
            this.questions = new Question[1];
        }

        public void newQuestion(Question question)
        {
            Question[] temp = new Question[questions.Length+1];
            for(int i = 0; i < questions.Length; i++)
                temp[i] = questions[i];
            temp[questions.Length] = question;
            this.questions = temp;
        }

        public Question getQuestion(int id)
        {
            if (id >= 0 && id < questions.Length)
            {
               return questions[id];
            }
            else
            {
                throw new Exception("Неверное значение элемента массива");
            }
        }

        public void updateQuestion(int id, Question question)
        {
            if (id >= 0 && id < questions.Length)
            {
                this.questions[id] = question;
            }
            else
            {
                throw new Exception("Неверное значение элемента массива");
            }
        }
        public void removeQuestion(int id)
        {
            if (id >= 0 && id < questions.Length)
            {
                Question[] temp = new Question[questions.Length];
                for (int i = 0; i < questions.Length; i++)
                    if (i != id)
                        temp.Append(questions[i]);
                this.questions = temp;
            }
            else
            {
                throw new Exception("Неверное значение элемента массива");
            }
        }
    }
}
