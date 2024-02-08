using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using log4net.Config;
using Microsoft.VisualBasic.Logging;
using log4net;

namespace CalculatorAppForNichi
{
    public partial class Form1 : Form
    {
         bool predicate = false;
        int counter = 0;
        double lastInputNumber;
        string number;
        bool a = true;
        int num_of_input_operation = 0;
        double firstNum;
        double total;
        string inputvalue;
        double totalSum = 0;
        double beforeLastInputNumber;

        private static readonly ILog log = LogManager.GetLogger(typeof(Form1));
        public Form1()
        {
            InitializeComponent();

            // Initialize log4net
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));

            // Log a test message
            log.Info("Form1 initialized.");
        }
        private void Button_Click(object sender, EventArgs e)
        {
            try
            {
                a = true;
                if (ResultTextbox.Text == "0" || predicate)
                {
                    ResultTextbox.Clear();
                }

                predicate = false;

                if (((Button)sender).Text == ".")
                {
                    if (!ResultTextbox.Text.Contains("."))
                    {
                        ResultTextbox.Text += ((Button)sender).Text;
                    }
                }
                else
                {
                    if (counter != 0)
                    {
                        ResultTextbox.Text = ((Button)sender).Text;
                        counter = 0;
                    }
                    else
                    {
                        // Check if the current text already contains a decimal point
                        if (!ResultTextbox.Text.Contains("."))
                        {
                            // If not, directly append the button text to the text box
                            ResultTextbox.Text += ((Button)sender).Text;
                        }
                        else
                        {
                            // If the text already contains a decimal point, determine the position of the decimal point
                            int decimalIndex = ResultTextbox.Text.IndexOf(".");

                            // Check if there are more than 5 digits after the decimal point
                            if (ResultTextbox.Text.Length - decimalIndex <= 5)
                            {
                                // If there are 5 or fewer digits after the decimal point, append the button text to the text box
                                ResultTextbox.Text += ((Button)sender).Text;
                            }
                        }
                    }
                }

                lastInputNumber = double.Parse(ResultTextbox.Text);

                number = ((Button)sender).Text;
                ///1.
                if (num_of_input_operation == 0)
                {
                    firstNum = double.Parse(ResultTextbox.Text);
                }
                if (num_of_input_operation >= 1)
                {
                    total = Calculation(inputvalue, firstNum, lastInputNumber);
                }
            }
            catch (Exception)
            {

                //label1.Text = "Invalid Arguement";
            }
            
        }

       

        private double Calculation(string inputvalue, double firstNum, double secondNum)
        {
            double sum = 0;
            switch (inputvalue)
            {
                case "+":
                    sum = firstNum + secondNum;
                    break;

                case "-":
                    sum = firstNum - secondNum;
                    break;

                case "x":
                    sum = firstNum * secondNum;
                    break;

                case "/":
                    sum = firstNum / secondNum;
                    break;

               case "√":
                    ResultTextbox.Text = Math.Sqrt(totalSum).ToString();
                    predicate = false;
                    break;

                default:
                   //label1.Text = "Invalid argument!";
                    break;
            }
            return sum;
        }
        private void Calculation_Click(object sender, EventArgs e)
        {
            a = true;
            counter = 0;
            inputvalue = ((Button)sender).Text;//input operation
            totalSum = double.Parse(ResultTextbox.Text);// current value
            predicate = true;
            //1. check for "x + y * z must give t."
            if (num_of_input_operation >= 1)
            {
                ResultTextbox.Text = total.ToString();
                firstNum = total;
                totalSum = total;// stores the current sum for  "Btn_Equals_OnClick" method !
            }

            if (((Button)sender).Text != "√")
            {
                label1.Text = $"{ResultTextbox.Text} {inputvalue} ";
            }
            else 
            {
                label1.Text = $"{inputvalue}({totalSum.ToString()})";
            }

            num_of_input_operation++;
            beforeLastInputNumber = double.Parse(ResultTextbox.Text);// for example: x + y... ,  beforeLastInputNumber = x

            if (ResultTextbox.Text.Length >= 14)
            {
                ResultTextbox.Font = new Font("Nirmala UI", 17, FontStyle.Bold);
                label1.Font = new Font("Nirmala UI", 9, FontStyle.Bold);
            }
        }
        private void ButtonEquals_Click(object sender, EventArgs e)
        {
            if ((ResultTextbox.Text != "0" && a && (!predicate || (predicate && inputvalue == "√") || (predicate && num_of_input_operation > 0))) || (ResultTextbox.Text == "0" && a && !predicate /*&& counter != 0*/))
            {

                if (predicate)
                {
                    if (num_of_input_operation > 0)
                    {
                        label1.Text += $"{ResultTextbox.Text} =";
                        predicate = false;
                    }
                    //else Lbl_1.Text += " =";
                }
                else
                {
                    if (counter == 0)
                    {
                        label1.Text += $"{ResultTextbox.Text} =";
                    }
                    else
                    {
                        if (inputvalue == "√")
                        {
                            label1.Text = $"{inputvalue}({totalSum.ToString()}) =";
                        }
                        else
                        {
                            label1.Text = $"{ResultTextbox.Text} {inputvalue} {lastInputNumber} =";
                        }
                    }
                }

                switch (inputvalue)
                {
                    case "+":
                        if (counter == 0)
                        {
                            lastInputNumber = double.Parse(ResultTextbox.Text);//1
                            ResultTextbox.Text = (totalSum + double.Parse(ResultTextbox.Text)).ToString();
                        }
                        else
                        {
                            ResultTextbox.Text = (lastInputNumber + double.Parse(ResultTextbox.Text)).ToString();
                        }
                        a = true;
                        break;

                    case "-":
                        if (counter == 0)
                        {
                            lastInputNumber = double.Parse(ResultTextbox.Text);//1
                            ResultTextbox.Text = (totalSum - double.Parse(ResultTextbox.Text)).ToString();
                        }
                        else
                        {
                            ResultTextbox.Text = (double.Parse(ResultTextbox.Text) - lastInputNumber).ToString();
                        }
                        a = true;
                        break;

                    case "X":
                        if (counter == 0)
                        {
                            lastInputNumber = double.Parse(ResultTextbox.Text);//1
                            ResultTextbox.Text = (totalSum * double.Parse(ResultTextbox.Text)).ToString();
                        }
                        else
                        {
                            ResultTextbox.Text = (double.Parse(ResultTextbox.Text) * lastInputNumber).ToString();
                        }
                        a = true;
                        break;

                    case "/":
                        if (counter == 0)
                        {
                            if (!predicate && num_of_input_operation > 0)
                            {
                                lastInputNumber = double.Parse(ResultTextbox.Text);
                                ResultTextbox.Text = (totalSum / double.Parse(ResultTextbox.Text)).ToString();
                                Console.WriteLine("123456789Hello World");
                            }
                        }
                        else
                        {
                            ResultTextbox.Text = (double.Parse(ResultTextbox.Text) / lastInputNumber).ToString();
                        }
                        a = true;
                        break;

                    case "√":
                        ResultTextbox.Text = Math.Sqrt(totalSum).ToString();
                        predicate = false;
                        break;

                    default:
                        label1.Text = "Invalid argument!";
                        break;
                }
                counter++;
            }
            else
            {
                if (inputvalue != "√")
                {
                    if (number == null || inputvalue == null)//looks for case: 0 -> =
                    {
                        label1.Text = "0 =";
                    }
                    else
                    {
                        label1.Text = $"{totalSum.ToString()} {inputvalue} 0 =";
                    }
                }
                else 
                {
                    label1.Text = $"{inputvalue}({totalSum.ToString()}) =";
                }

                ResultTextbox.Text = "0";
                if (counter != 0) a = false;
            }

            if (ResultTextbox.Text.Length >= 14)
            {
                ResultTextbox.Font = new Font("Nirmala UI", 17, FontStyle.Bold);
                label1.Font = new Font("Nirmala UI", 9, FontStyle.Bold);
            }
            num_of_input_operation = 0;
        }
        private void Btn_Clear(object sender, EventArgs e)
        {
            ResultTextbox.Font = new Font("Nirmala UI", 24, FontStyle.Bold);
            label1.Font = new Font("Nirmala UI", 12, FontStyle.Bold);
            ResultTextbox.Text = "0";
            label1.Text = null;
            counter = 0;
            a = true;
            inputvalue = null;
            beforeLastInputNumber = 0;///
            num_of_input_operation = 0;///
            total = 0;
            //
            number = null;
            predicate = false;
            firstNum = 0;
        }

    }
}
