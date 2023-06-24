using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calculator
{
    public partial class Main : Form
    {
        // переменная ответа
        double result = 0;
        // переменная для хранения операции
        string operation = "";
        // переменные для хранения вводимых чисел
        string firstNumber = "";
        string secondNumber = "";
        // можно ли вводить вторую переменную
        bool enterValue = false;
        // память
        double memoryValue = 0;


        public Main()
        {
            InitializeComponent();
            textBox2.Text = "0";
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            historyPanel.Visible = (historyPanel.Visible) ? false : true;
            historyPanel.TabIndex = 9999;
            historyPanel.Location = new Point(0, 108);
        }

        private void inputNumToField(object sender, EventArgs e)
        {
            // переменная для считывания кнопки текщей
            Button btn = (Button)sender;

            // чтобы 0 стирался при вводе(если в поле 0 первый или пустая строка)
            if (textBox2.Text == "0" || enterValue)
            {
                if (btn.Text == ",")
                {
                    textBox2.Text = "0";
                }
                else
                {
                    textBox2.Text = "";
                }
            }

            // вводить вторую переменную нельзя
            enterValue = false;

            // Ограничения на ввод знаков
            int indexOfComma = textBox2.Text.IndexOf(",");

            if (textBox2.Text.Contains(",") && textBox2.Text.Substring(indexOfComma).Length > 15)
            {
                return;
            }

            if (textBox2.Text.Length >= 18)
            {
                return;
            }

            // проверка, чтобы была одна ,
            if (btn.Text == ",")
            {
                if (!textBox2.Text.Contains(","))
                {
                    textBox2.Text += btn.Text;
                }
            }
            else
            {
                if (textBox2.Text.Length <= 17)
                {
                    textBox2.Text += btn.Text;
                }
            }
        }

        private void baseMathOperation(object sender, EventArgs e)
        {
            if (result != 0)
            {
                button26.PerformClick();
            }
            else
            {
                result = Double.Parse(textBox2.Text);
            }

            Button currenntBtn = (Button)sender;
            operation = currenntBtn.Text;
            enterValue = true;

            button32.Enabled = false;
            button17.Enabled = false;
            button25.Enabled = false;
            button21.Enabled = false;
            button13.Enabled = false;
            button30.Enabled = false;
            button31.Enabled = false;
            button10.Enabled = false;
            button29.Enabled = false;


            if (textBox2.Text != "")
            {
                textBox1.Text = firstNumber = $"{result} {operation}";
                textBox2.Text = ""; 
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            secondNumber = textBox2.Text;
            textBox1.Text = $"{textBox1.Text} {textBox2.Text} =";

            if (textBox2.Text != "")
            {
                switch (operation)
                {
                    case "+":
                        textBox2.Text = (Math.Round((result + Double.Parse(textBox2.Text)), 15)).ToString();
                        historyText.AppendText($"{firstNumber} {secondNumber} = {textBox2.Text}\n");
                        break;
                    case "-":
                        textBox2.Text = (Math.Round((result - Double.Parse(textBox2.Text)), 15)).ToString();
                        historyText.AppendText($"{firstNumber} {secondNumber} = {textBox2.Text}\n");
                        break;
                    case "×":
                        textBox2.Text = (Math.Round((result * Double.Parse(textBox2.Text)), 15)).ToString();
                        historyText.AppendText($"{firstNumber} {secondNumber} = {textBox2.Text}\n");
                        break;
                    case "÷":
                        textBox2.Text = (Math.Round((result / Double.Parse(textBox2.Text)), 15)).ToString();
                        historyText.AppendText($"{firstNumber} {secondNumber} = {textBox2.Text}\n");
                        break;
                    
                }
            }

            result = Double.Parse(textBox2.Text);
            operation = "";

            button32.Enabled = true;
            button17.Enabled = true;
            button25.Enabled = true;
            button21.Enabled = true;
            button13.Enabled = true;
            button30.Enabled = true;
            button31.Enabled = true;
            button10.Enabled = true;
            button29.Enabled = true;
        }

        // кнопки очистки
        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                textBox2.Text = textBox2.Text.Remove(textBox2.Text.Length - 1, 1);
            }

            if (textBox2.Text == "")
            {
                textBox2.Text = "0";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox2.Text = "0";
            textBox1.Text = "";
            result = 0;

            button32.Enabled = true;
            button17.Enabled = true;
            button25.Enabled = true;
            button21.Enabled = true;
            button13.Enabled = true;
            button30.Enabled = true;
            button31.Enabled = true;
            button10.Enabled = true;
            button29.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox2.Text = "0";
        }

        // кнопки остальных операций
        private void otherOperations(object sender, EventArgs e)
        {
            Button currentBtn = (Button)sender;
            operation = currentBtn.Text;

            switch (operation)
            {
                case "√x":
                    textBox1.Text = $"√({textBox2.Text})";
                    textBox2.Text = Math.Round(Math.Sqrt(Double.Parse(textBox2.Text)), 15).ToString();
                    break;
                case "x²":
                    textBox1.Text = $"({textBox2.Text})²";
                    textBox2.Text = Math.Round(Math.Pow(Double.Parse(textBox2.Text), 2), 15).ToString();
                    break;
                case "1/x":
                    textBox1.Text = $"1/({textBox2.Text})";
                    textBox2.Text = Double.Parse(textBox2.Text) == 0 ? "∞" : Math.Round((1.0 / Double.Parse(textBox2.Text)), 15).ToString();
                    break;
                case "%":
                    textBox1.Text = $"%({textBox2.Text})";
                    textBox2.Text = Math.Round((Double.Parse(textBox2.Text) / 100.0), 15).ToString();
                    break;
                case "+/-":
                    textBox2.Text = Math.Round(((-1) * Double.Parse(textBox2.Text)), 15).ToString();
                    break;
            }

            historyText.AppendText($"{textBox1.Text} = {textBox2.Text}\n");

        }

        // кнопки для работы с памятью
        private void button9_Click(object sender, EventArgs e)
        {
            memoryValue = 0;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"В памяти хранится число:\n{memoryValue}");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = memoryValue.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            memoryValue += Double.Parse(textBox2.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            memoryValue -= Double.Parse(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ClearHistory(object sender, EventArgs e)
        {
            historyText.Clear();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
