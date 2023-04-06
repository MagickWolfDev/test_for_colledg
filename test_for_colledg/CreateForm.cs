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
using System.Text.Json;
using System.IO;

namespace test_for_colledg
{
    public partial class CreateForm : Form
    {
        Question question = new Question();
        
        int questionCount = 0;
        int questionIndex = 0;
        TestDataModal testDataModal = new("newTest");
        public CreateForm()
        {
            this.question.options = new String[4];
            this.question.title = "";
            InitializeComponent();
            
        }

        private void CreateForm_Load(object sender, EventArgs e)
        {
            button6.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            this.comboBox1.SelectedIndex = 0;
            this.label7.Text = (questionIndex + 1).ToString() + "/" + (questionCount + 1).ToString();
            groupBox1.Visible = false;
        }

        private void showData()
        {
            textBox5.Text = question.title;
            textBox1.Text = question.options[0];
            textBox2.Text = question.options[1];
            textBox3.Text = question.options[2];
            textBox4.Text = question.options[3];
            if(question.rightAnswer > -1)
                comboBox1.SelectedIndex = question.rightAnswer;
            else 
                comboBox1.SelectedIndex = 0;
        }

        private bool validateForm()
        {
            bool validate = (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "");
            if (!validate)
            {
                MessageBox.Show("Заполните форму!");

            }
            return validate;
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            question.title = textBox5.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            question.options[0] = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            question.options[1] = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            question.options[2] = textBox3.Text;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            question.options[3] = textBox4.Text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            question.rightAnswer = comboBox1.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validateForm())
            {
                if (this.questionIndex == questionCount)
                {
                    if(questionCount > 0)
                    {
                        this.testDataModal.newQuestion(this.question);
                        this.question = new Question();
                        this.question.options = new String[4];
                        this.question.title = "";
                        questionIndex++;
                        questionCount++;
                    }
                    else
                    {
                        this.testDataModal.updateQuestion(0, this.question);
                        this.question = new Question();
                        this.question.options = new String[4];
                        this.question.title = "";
                        questionIndex++;
                        questionCount++;
                    }
                    
                }
                else
                {
                    questionCount++;
                    this.testDataModal.updateQuestion(questionIndex, this.question);
                    questionIndex = questionCount;
                }
                this.textBox1.Text = "";
                this.textBox2.Text = "";
                this.textBox3.Text = "";
                this.textBox4.Text = "";
                this.textBox5.Text = "";
                this.comboBox1.SelectedIndex = 0;

                this.label7.Text = (questionIndex + 1).ToString() + "/" + (questionCount + 1).ToString();
                button3.Enabled = true;
                button4.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(questionIndex > 0)
            {
                if(validateForm())
                {
                    if(questionIndex < questionCount)
                        this.testDataModal.updateQuestion(this.questionIndex, this.question);
                    else
                        this.testDataModal.newQuestion(this.question);
                    questionIndex--;
                    this.question = this.testDataModal.getQuestion(questionIndex);

                    this.label7.Text = (questionIndex + 1).ToString() + "/" + (questionCount + 1).ToString();
                    button6.Enabled = true;

                    if (questionIndex == 0)
                        button3.Enabled = false;
                    showData();
                }   
            }
                        
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if( questionIndex < questionCount)
            {
                this.testDataModal.updateQuestion(this.questionIndex, this.question);
                questionIndex++;
                this.question = this.testDataModal.getQuestion(questionIndex);

                this.label7.Text = (questionIndex + 1).ToString() + "/" + (questionCount + 1).ToString();
                if(questionIndex == questionCount)
                    button6.Enabled = false;

                this.button3.Enabled = true;
                showData();
            }
            else
            {
                button6.Enabled = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(questionCount > 0)
            {
                if(questionIndex < questionCount)
                    testDataModal.removeQuestion(this.questionIndex);
                questionCount--;
                questionIndex--;
                question = testDataModal.getQuestion(questionIndex);

                this.label7.Text = (questionIndex + 1).ToString() + "/" + (questionCount + 1).ToString();
                if (questionIndex == 0)
                    button4.Enabled = false;
                showData();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(textBox6.Text != "")
            {
                button7.Visible = false; 
                label8.Visible = false;
                textBox6.Visible = false;
                groupBox1.Visible = true;

                button1.Visible = true;
                button4.Visible = true;
                button3.Visible = true;
                button6.Visible = true;
                button2.Visible = true;
                label7.Visible = true;

                testDataModal.title = textBox6.Text;
            }
            else
            {
                MessageBox.Show("Введите название теста");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = testDataModal.title;
            saveFileDialog.InitialDirectory = "./";
            saveFileDialog.Filter = "json files (*.json)|*.json";
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream createStream = File.Create(saveFileDialog.FileName);
                await JsonSerializer.SerializeAsync(createStream, testDataModal);
                await createStream.DisposeAsync();
            }

            //string fileName = "WeatherForecast.json";
            //using FileStream createStream = File.Create(fileName);
            //await JsonSerializer.SerializeAsync(createStream, weatherForecast);
            //await createStream.DisposeAsync();
        }

        private void button5_Click(object sender, EventArgs e)
        {
        DialogResult result = MessageBox.Show(
        "Вы уверены, что хотите выйти? Данные не будут сохранены!",
        "Сообщение",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Information,
        MessageBoxDefaultButton.Button1,
        MessageBoxOptions.DefaultDesktopOnly);

        if (result == DialogResult.Yes)
        {
                Form1 form = new Form1();
                form.Show();
                this.Close();
        }
                
        }
    }
}
