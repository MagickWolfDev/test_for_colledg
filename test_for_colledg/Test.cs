using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using test_for_colledg.models;

namespace test_for_colledg
{
    public partial class Test : Form
    {
        TestDataModal testDataModal;
        private int currentIndex = 0;
        private int questionCount = 0;
        private int[] answers;
        public Test(String testDataString)
        {
            InitializeComponent();
            try
            {
                this.testDataModal = JsonSerializer.Deserialize<TestDataModal>(testDataString);
                questionCount = testDataModal.questions.Count() - 1;
                answers = new int[questionCount+1];
                for (int i = 0; i <= questionCount; i++)
                    answers[i] = -1;

            } catch {
                MessageBox.Show("Ошибка в чтении данных");
            }
            //throw new Exception("gay!");
        }

        private void showData()
        {
            label1.Text = this.testDataModal.getQuestion(currentIndex).title;
            this.label7.Text = (currentIndex + 1).ToString() + "/" + (questionCount + 1).ToString();
            radioButton1.Text = this.testDataModal.getQuestion(currentIndex).options[0];
            radioButton2.Text = this.testDataModal.getQuestion(currentIndex).options[1];
            radioButton3.Text = this.testDataModal.getQuestion(currentIndex).options[2];
            radioButton4.Text = this.testDataModal.getQuestion(currentIndex).options[3];
        }

        private void Test_Load(object sender, EventArgs e)
        {
            this.label3.Text = this.testDataModal.title;
            this.label4.Text = "Вопросов в тесте: " + (questionCount + 1);
            this.label2.Visible = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
                answers[currentIndex] = 0;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
                answers[currentIndex] = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
                answers[currentIndex] = 2;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
                answers[currentIndex] = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(currentIndex < questionCount)
            {
                currentIndex++;
                this.label7.Text = (currentIndex + 1).ToString() + "/" + (questionCount + 1).ToString();
                button2.Enabled = true;

                if (currentIndex == questionCount)
                {
                    button1.Text = "Завершить тест";
                }
                else
                {
                    button1.Text = "Следующий вопрос";
                    
                }
                if (answers[currentIndex] >= 0)
                    for (int i = 0; i < groupBox1.Controls.Count; i++)
                    {
                        if (i == answers[currentIndex])
                            ((RadioButton)this.groupBox1.Controls[i]).Checked = true;
                        //Debug.WriteLine(this.groupBox1.Controls[i]);
                    }
                else
                    for (int i = 0; i < groupBox1.Controls.Count; i++)
                    {
                        if (((RadioButton)this.groupBox1.Controls[i]).Checked == true)
                            ((RadioButton)this.groupBox1.Controls[i]).Checked = false;
                        //Debug.WriteLine(this.groupBox1.Controls[i]);
                    }

                showData();
            }
            else
            {
                double rightAnswersCount = 0;
                for(int i = 0; i <= questionCount; i++)
                {
                    //Debug.WriteLine(this.testDataModal.getQuestion(i).rightAnswer + " / " + this.answers[i]);
                    if (this.testDataModal.getQuestion(i).rightAnswer == this.answers[i])
                        rightAnswersCount++;
                }
                
                double procent = ((rightAnswersCount / (questionCount +1)) * 100);

                label4.Text = "Вы ответили на " + procent + "% вопросов правильно!";
                label4.Visible = true;
                button4.Visible = true;

                this.groupBox1.Visible = false;
                this.button1.Visible = false;
                this.button2.Visible = false;
                this.label1.Visible = false;
                this.label7.Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(currentIndex > 0)
            {
                currentIndex--;

                this.label7.Text = (currentIndex + 1).ToString() + "/" + (questionCount + 1).ToString();
                button1.Text = "Следующий вопрос";
                

                if (currentIndex == 0)
                {
                    button2.Enabled = false;
                }

                for (int i = 0; i < groupBox1.Controls.Count; i++)
                {
                    if (i == answers[currentIndex])
                        ((RadioButton)this.groupBox1.Controls[i]).Checked = true;
                    else
                        ((RadioButton)this.groupBox1.Controls[i]).Checked = false;
                    //Debug.WriteLine(this.groupBox1.Controls[i]);
                }
                showData();
            }
            else
            {

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.label2.Visible = false;
            this.label3.Visible = false;
            this.button3.Visible = false;

            showData();

            this.groupBox1.Visible = true;
            this.button1.Visible = true;
            this.button2.Visible = true;
            this.label1.Visible = true;
            this.label7.Visible = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Close();
        }
    }
}
