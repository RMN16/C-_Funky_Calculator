using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FunkyCalculator
{
    public partial class Form1 : Form
    {
        private Calculator calculator;
        private FunkyResponceManager funkyManager;

        private string currentDisplay;
        private bool shouldClearDisplay;
        //private Timer funkyTimer;

        private Color originalBackColor;

        //private TextBox displayTextBox;
        //private Label funkyLabel;
        //private Button[] numberButtons;

        //private Button btnAdd, btnSubtract, btnMultiply, btnDivide, btnModulo, btnPower;
        //private Button btnEquals, btnClear, btnBackspace, btnDecimal, btnToggleSign;

        public Form1()
        {
            InitializeComponent();
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {
            calculator = new Calculator();
            funkyManager = FunkyResponceManager.Instance;
            currentDisplay = "0";
            shouldClearDisplay = false;
            originalBackColor = this.BackColor;

            //  funkyTimer = new Timer();
            //  funkyTimer.Interval = 3000; // 3 seconds
            //  funkyTimer.Tick += FunkyTimer_Tick;
        }



        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void updateDisplay()
        {
            displayTextBox.Text = currentDisplay;
        }

        private void NumberButton_Click(int number)
        {
            if (shouldClearDisplay)
            {
                currentDisplay = "";
                shouldClearDisplay = false;
            }

            if (currentDisplay == "0")
            {
                currentDisplay = number.ToString();
            }
            else
            {
                currentDisplay += number.ToString();
            }

            updateDisplay();

        }
        private void numberButton1_Click(object sender, EventArgs e)
        {
            NumberButton_Click(1);
        }
        private void numberButton2_Click(object sender, EventArgs e)
        {
            NumberButton_Click(2);
        }
        private void numberButton3_Click(object sender, EventArgs e)
        {
            NumberButton_Click(3);
        }
        private void numberButton4_Click(object sender, EventArgs e)
        {
            NumberButton_Click(4);
        }
        private void numberButton5_Click(object sender, EventArgs e)
        {
            NumberButton_Click(5);
        }
        private void numberButton6_Click(object sender, EventArgs e)
        {
            NumberButton_Click(6);
        }
        private void numberButton7_Click(object sender, EventArgs e)
        {
            NumberButton_Click(7);
        }
        private void numberButton8_Click(object sender, EventArgs e)
        {
            NumberButton_Click(8);
        }
        private void numberButton9_Click(object sender, EventArgs e)
        {
            NumberButton_Click(9);
        }
        private void numberButton0_Click(object sender, EventArgs e)
        {
            NumberButton_Click(0);
        }

        private void btnDecimal_Click(object sender, EventArgs e)
        {
            if (!currentDisplay.Contains("."))
            {
                if (shouldClearDisplay)
                {
                    currentDisplay = "0";
                    shouldClearDisplay = false;
                }
                else
                {
                    currentDisplay += ".";
                }

                updateDisplay();
            }
        }

        private void btnEquals_Click(object sender, EventArgs e)
        {
            PerformCalculation();
        }

        private void PerformCalculation()
        {
            if (calculator.HasOperation() && !string.IsNullOrEmpty(currentDisplay))
            {
                try
                {
                    double currentValue = double.Parse(currentDisplay);
                    double result = calculator.Calculate(calculator.CurrentOperation, calculator.PreviousValue, currentValue);

                    result = Math.Round(result, 10); // Round to avoid floating-point precision issues

                    currentDisplay = result.ToString();
                    updateDisplay();

                    CheckForFunkyNumber(result);

                    calculator.CurrentOperation = string.Empty;
                    shouldClearDisplay = true;
                }
                catch (DivideByZeroException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Clear();
                }
            }
        }

        private void CheckForFunkyNumber(double number)
        {
            if (funkyManager.isFunkyNumber(number))
            {
                FunkyResponse response = funkyManager.GetFunkyResponse(number);

                this.BackColor = response.BackgroundColor;
                funkyMessageLabel.Text = response.Message;
                funkyMessageLabel.ForeColor = response.TextColor;
                funkyMessageLabel.BackColor = response.BackgroundColor;
            }

        }

        private void Clear()
        {
            calculator.Clear();
            currentDisplay = "0";
            shouldClearDisplay = false;
            updateDisplay();

            this.BackColor = originalBackColor;
            funkyMessageLabel.Text = "";
        }
        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (currentDisplay.Length > 0 && !shouldClearDisplay)
            {
                currentDisplay = currentDisplay.Substring(0, currentDisplay.Length - 1);

                if (string.IsNullOrEmpty(currentDisplay))
                {
                    currentDisplay = "0";
                }
                updateDisplay();
            }
        }

        private void btnToggleSign_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(currentDisplay) && currentDisplay != "0")
            {
                if (currentDisplay.StartsWith("-"))
                {
                    currentDisplay = currentDisplay.Substring(1);
                }
                else
                {
                    currentDisplay = "-" + currentDisplay;
                }
                updateDisplay();
            }
        }

        private void OperationButton_Click(string operation)
        {
            if (!string.IsNullOrEmpty(currentDisplay))
            {
                if (calculator.HasOperation() && !shouldClearDisplay)
                {
                    PerformCalculation();
                }

                calculator.PreviousValue = double.Parse(currentDisplay);
                calculator.CurrentOperation = operation;
                shouldClearDisplay = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OperationButton_Click("+");
        }
        private void btnSubtract_Click(object sender, EventArgs e)
        {
            OperationButton_Click("-");
        }
        private void btnMultiply_Click(object sender, EventArgs e)
        {
            OperationButton_Click("*");
        }
        private void btnDevide_Click(object sender, EventArgs e)
        {
            OperationButton_Click("/");
        }
        private void btnPower_Click(object sender, EventArgs e)
        {
            OperationButton_Click("^");
        }
        private void btnModulo_Click(object sender, EventArgs e)
        {
            OperationButton_Click("%");
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}

