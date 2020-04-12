using System;
using System.Windows.Forms;
//*********************************************************

//****Assessment 2 Program 1 

//*********************************************************

namespace IT232_Poltera_M2_Program1
{
    public partial class Form1 : Form
    {
        Double resultValue = 0;
        String operationPerformed = "";
        bool isOperationPerformed = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void button_click(object sender, EventArgs e)
        {
            if ((txt_Result.Text == "0") || (isOperationPerformed))
                txt_Result.Clear();

            isOperationPerformed = false;
            Button button = (Button)sender;
            if (button.Text == ".")
            {
                if (!txt_Result.Text.Contains("."))
                    txt_Result.Text = txt_Result.Text + button.Text;

            }
            else
                txt_Result.Text = txt_Result.Text + button.Text;


        }

        private void operator_click(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (resultValue != 0)
            {
                btnEquals.PerformClick();
                operationPerformed = button.Text;
                labelCurrentOperation.Text = resultValue + " " + operationPerformed;
                isOperationPerformed = true;
            }
            else
            {

                operationPerformed = button.Text;
                resultValue = Double.Parse(txt_Result.Text);
                labelCurrentOperation.Text = resultValue + " " + operationPerformed;
                isOperationPerformed = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            txt_Result.Text = "0";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            txt_Result.Text = "0";
            resultValue = 0;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            switch (operationPerformed)
            {
                case "+":
                    txt_Result.Text = (resultValue + Double.Parse(txt_Result.Text)).ToString();
                    break;
                case "-":
                    txt_Result.Text = (resultValue - Double.Parse(txt_Result.Text)).ToString();
                    break;
                case "*":
                    txt_Result.Text = (resultValue * Double.Parse(txt_Result.Text)).ToString();
                    break;
                case "/":
                    txt_Result.Text = (resultValue / Double.Parse(txt_Result.Text)).ToString();
                    break;
                default:
                    break;
            }
            resultValue = Double.Parse(txt_Result.Text);
            labelCurrentOperation.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txt_Result_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
