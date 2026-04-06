using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        private TabControl tabControl;
        private TabPage tabBasic, tabScientific, tabConverters;

        // Basic calculator
        private TextBox txtDisplay;
        private Button[] numberButtons;
        private Button btnAdd, btnSubtract, btnMultiply, btnDivide, btnEquals, btnClear;

        private double currentValue = 0;
        private string currentOperation = "";
        private bool operationPending = false;

        // Scientific calculator
        private TextBox txtDisplaySci;
        private Button btnSin, btnCos, btnTan, btnLog, btnLn, btnSqrt, btnSquare, btnPi, btnE;

        // Converters
        private ComboBox cmbWeightFrom, cmbWeightTo;
        private TextBox txtWeightFrom, txtWeightTo;
        private Button btnConvertWeight;
        private DateTimePicker dtpBirth;
        private Button btnCalcAge;
        private Label lblAgeResult;

        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Calculator";
            this.Size = new System.Drawing.Size(500, 600);

            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;
            this.Controls.Add(tabControl);

            tabBasic = new TabPage("Basic");
            tabControl.TabPages.Add(tabBasic);

            tabScientific = new TabPage("Scientific");
            tabControl.TabPages.Add(tabScientific);

            tabConverters = new TabPage("Converters");
            tabControl.TabPages.Add(tabConverters);

            InitializeBasic();
            InitializeScientific();
            InitializeConverters();
        }

        private void InitializeConverters()
        {
            // Weight Converter
            Label lblWeight = new Label();
            lblWeight.Text = "Weight Converter";
            lblWeight.Location = new System.Drawing.Point(10, 10);
            lblWeight.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            tabConverters.Controls.Add(lblWeight);

            txtWeightFrom = new TextBox();
            txtWeightFrom.Location = new System.Drawing.Point(10, 40);
            txtWeightFrom.Size = new System.Drawing.Size(100, 30);
            tabConverters.Controls.Add(txtWeightFrom);

            cmbWeightFrom = new ComboBox();
            cmbWeightFrom.Location = new System.Drawing.Point(120, 40);
            cmbWeightFrom.Size = new System.Drawing.Size(80, 30);
            cmbWeightFrom.Items.AddRange(new string[] { "kg", "lb", "g", "oz" });
            cmbWeightFrom.SelectedIndex = 0;
            tabConverters.Controls.Add(cmbWeightFrom);

            Label lblTo = new Label();
            lblTo.Text = "to";
            lblTo.Location = new System.Drawing.Point(210, 40);
            tabConverters.Controls.Add(lblTo);

            cmbWeightTo = new ComboBox();
            cmbWeightTo.Location = new System.Drawing.Point(230, 40);
            cmbWeightTo.Size = new System.Drawing.Size(80, 30);
            cmbWeightTo.Items.AddRange(new string[] { "kg", "lb", "g", "oz" });
            cmbWeightTo.SelectedIndex = 1;
            tabConverters.Controls.Add(cmbWeightTo);

            txtWeightTo = new TextBox();
            txtWeightTo.Location = new System.Drawing.Point(320, 40);
            txtWeightTo.Size = new System.Drawing.Size(100, 30);
            txtWeightTo.ReadOnly = true;
            tabConverters.Controls.Add(txtWeightTo);

            btnConvertWeight = new Button();
            btnConvertWeight.Text = "Convert";
            btnConvertWeight.Location = new System.Drawing.Point(430, 40);
            btnConvertWeight.Size = new System.Drawing.Size(70, 30);
            btnConvertWeight.Click += ConvertWeight_Click;
            tabConverters.Controls.Add(btnConvertWeight);

            // Age Calculator
            Label lblAge = new Label();
            lblAge.Text = "Age Calculator";
            lblAge.Location = new System.Drawing.Point(10, 100);
            lblAge.Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            tabConverters.Controls.Add(lblAge);

            Label lblBirth = new Label();
            lblBirth.Text = "Birth Date:";
            lblBirth.Location = new System.Drawing.Point(10, 130);
            tabConverters.Controls.Add(lblBirth);

            dtpBirth = new DateTimePicker();
            dtpBirth.Location = new System.Drawing.Point(100, 130);
            dtpBirth.Format = DateTimePickerFormat.Short;
            tabConverters.Controls.Add(dtpBirth);

            btnCalcAge = new Button();
            btnCalcAge.Text = "Calculate Age";
            btnCalcAge.Location = new System.Drawing.Point(250, 130);
            btnCalcAge.Size = new System.Drawing.Size(100, 30);
            btnCalcAge.Click += CalcAge_Click;
            tabConverters.Controls.Add(btnCalcAge);

            lblAgeResult = new Label();
            lblAgeResult.Text = "";
            lblAgeResult.Location = new System.Drawing.Point(10, 170);
            lblAgeResult.Size = new System.Drawing.Size(400, 50);
            tabConverters.Controls.Add(lblAgeResult);
        }

        private void InitializeBasic()
        {
            txtDisplay = new TextBox();
            txtDisplay.Location = new System.Drawing.Point(10, 10);
            txtDisplay.Size = new System.Drawing.Size(260, 30);
            txtDisplay.ReadOnly = true;
            txtDisplay.TextAlign = HorizontalAlignment.Right;
            tabBasic.Controls.Add(txtDisplay);

            numberButtons = new Button[10];
            for (int i = 0; i < 10; i++)
            {
                numberButtons[i] = new Button();
                numberButtons[i].Text = i.ToString();
                numberButtons[i].Size = new System.Drawing.Size(50, 50);
                numberButtons[i].Click += NumberButton_Click;
                tabBasic.Controls.Add(numberButtons[i]);
            }

            // Position number buttons
            numberButtons[1].Location = new System.Drawing.Point(10, 50);
            numberButtons[2].Location = new System.Drawing.Point(70, 50);
            numberButtons[3].Location = new System.Drawing.Point(130, 50);
            numberButtons[4].Location = new System.Drawing.Point(10, 110);
            numberButtons[5].Location = new System.Drawing.Point(70, 110);
            numberButtons[6].Location = new System.Drawing.Point(130, 110);
            numberButtons[7].Location = new System.Drawing.Point(10, 170);
            numberButtons[8].Location = new System.Drawing.Point(70, 170);
            numberButtons[9].Location = new System.Drawing.Point(130, 170);
            numberButtons[0].Location = new System.Drawing.Point(10, 230);
            numberButtons[0].Size = new System.Drawing.Size(110, 50);

            btnAdd = new Button();
            btnAdd.Text = "+";
            btnAdd.Location = new System.Drawing.Point(190, 50);
            btnAdd.Size = new System.Drawing.Size(50, 50);
            btnAdd.Click += OperationButton_Click;
            tabBasic.Controls.Add(btnAdd);

            btnSubtract = new Button();
            btnSubtract.Text = "-";
            btnSubtract.Location = new System.Drawing.Point(190, 110);
            btnSubtract.Size = new System.Drawing.Size(50, 50);
            btnSubtract.Click += OperationButton_Click;
            tabBasic.Controls.Add(btnSubtract);

            btnMultiply = new Button();
            btnMultiply.Text = "*";
            btnMultiply.Location = new System.Drawing.Point(190, 170);
            btnMultiply.Size = new System.Drawing.Size(50, 50);
            btnMultiply.Click += OperationButton_Click;
            tabBasic.Controls.Add(btnMultiply);

            btnDivide = new Button();
            btnDivide.Text = "/";
            btnDivide.Location = new System.Drawing.Point(190, 230);
            btnDivide.Size = new System.Drawing.Size(50, 50);
            btnDivide.Click += OperationButton_Click;
            tabBasic.Controls.Add(btnDivide);

            btnEquals = new Button();
            btnEquals.Text = "=";
            btnEquals.Location = new System.Drawing.Point(130, 230);
            btnEquals.Size = new System.Drawing.Size(50, 50);
            btnEquals.Click += EqualsButton_Click;
            tabBasic.Controls.Add(btnEquals);

            btnClear = new Button();
            btnClear.Text = "C";
            btnClear.Location = new System.Drawing.Point(250, 50);
            btnClear.Size = new System.Drawing.Size(50, 50);
            btnClear.Click += ClearButton_Click;
            tabBasic.Controls.Add(btnClear);
        }

        private void InitializeScientific()
        {
            txtDisplaySci = new TextBox();
            txtDisplaySci.Location = new System.Drawing.Point(10, 10);
            txtDisplaySci.Size = new System.Drawing.Size(450, 30);
            txtDisplaySci.ReadOnly = true;
            txtDisplaySci.TextAlign = HorizontalAlignment.Right;
            tabScientific.Controls.Add(txtDisplaySci);

            // Number buttons for scientific
            for (int i = 0; i < 10; i++)
            {
                Button btn = new Button();
                btn.Text = i.ToString();
                btn.Size = new System.Drawing.Size(40, 40);
                btn.Click += SciNumberButton_Click;
                tabScientific.Controls.Add(btn);
                // Position later
            }

            // Position numbers
            int x = 10, y = 50;
            for (int i = 1; i <= 9; i++)
            {
                Button btn = (Button)tabScientific.Controls[tabScientific.Controls.Count - 10 + i];
                btn.Location = new System.Drawing.Point(x, y);
                x += 50;
                if (i % 3 == 0) { x = 10; y += 50; }
            }
            Button btn0 = (Button)tabScientific.Controls[tabScientific.Controls.Count - 1];
            btn0.Location = new System.Drawing.Point(10, y);
            btn0.Size = new System.Drawing.Size(90, 40);

            // Function buttons
            btnSin = new Button();
            btnSin.Text = "sin";
            btnSin.Location = new System.Drawing.Point(200, 50);
            btnSin.Size = new System.Drawing.Size(50, 40);
            btnSin.Click += SciFunction_Click;
            tabScientific.Controls.Add(btnSin);

            btnCos = new Button();
            btnCos.Text = "cos";
            btnCos.Location = new System.Drawing.Point(260, 50);
            btnCos.Size = new System.Drawing.Size(50, 40);
            btnCos.Click += SciFunction_Click;
            tabScientific.Controls.Add(btnCos);

            btnTan = new Button();
            btnTan.Text = "tan";
            btnTan.Location = new System.Drawing.Point(320, 50);
            btnTan.Size = new System.Drawing.Size(50, 40);
            btnTan.Click += SciFunction_Click;
            tabScientific.Controls.Add(btnTan);

            btnLog = new Button();
            btnLog.Text = "log";
            btnLog.Location = new System.Drawing.Point(200, 100);
            btnLog.Size = new System.Drawing.Size(50, 40);
            btnLog.Click += SciFunction_Click;
            tabScientific.Controls.Add(btnLog);

            btnLn = new Button();
            btnLn.Text = "ln";
            btnLn.Location = new System.Drawing.Point(260, 100);
            btnLn.Size = new System.Drawing.Size(50, 40);
            btnLn.Click += SciFunction_Click;
            tabScientific.Controls.Add(btnLn);

            btnSqrt = new Button();
            btnSqrt.Text = "√";
            btnSqrt.Location = new System.Drawing.Point(320, 100);
            btnSqrt.Size = new System.Drawing.Size(50, 40);
            btnSqrt.Click += SciFunction_Click;
            tabScientific.Controls.Add(btnSqrt);

            btnSquare = new Button();
            btnSquare.Text = "x²";
            btnSquare.Location = new System.Drawing.Point(200, 150);
            btnSquare.Size = new System.Drawing.Size(50, 40);
            btnSquare.Click += SciFunction_Click;
            tabScientific.Controls.Add(btnSquare);

            btnPi = new Button();
            btnPi.Text = "π";
            btnPi.Location = new System.Drawing.Point(320, 150);
            btnPi.Size = new System.Drawing.Size(50, 40);
            btnPi.Click += SciConstant_Click;
            tabScientific.Controls.Add(btnPi);

            btnE = new Button();
            btnE.Text = "e";
            btnE.Location = new System.Drawing.Point(380, 150);
            btnE.Size = new System.Drawing.Size(50, 40);
            btnE.Click += SciConstant_Click;
            tabScientific.Controls.Add(btnE);

            Button btnDecimal = new Button();
            btnDecimal.Text = ".";
            btnDecimal.Location = new System.Drawing.Point(110, 200);
            btnDecimal.Size = new System.Drawing.Size(40, 40);
            btnDecimal.Click += SciDecimal_Click;
            tabScientific.Controls.Add(btnDecimal);

            Button btnClearSci = new Button();
            btnClearSci.Text = "C";
            btnClearSci.Location = new System.Drawing.Point(380, 50);
            btnClearSci.Size = new System.Drawing.Size(50, 40);
            btnClearSci.Click += SciClear_Click;
            tabScientific.Controls.Add(btnClearSci);
        }

        private void NumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (operationPending)
            {
                txtDisplay.Text = button.Text;
                operationPending = false;
            }
            else
            {
                txtDisplay.Text += button.Text;
            }
        }

        private void OperationButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                currentValue = double.Parse(txtDisplay.Text);
                currentOperation = button.Text;
                operationPending = true;
            }
        }

        private void EqualsButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text) && !string.IsNullOrEmpty(currentOperation))
            {
                double secondValue = double.Parse(txtDisplay.Text);
                double result = 0;

                switch (currentOperation)
                {
                    case "+":
                        result = currentValue + secondValue;
                        break;
                    case "-":
                        result = currentValue - secondValue;
                        break;
                    case "*":
                        result = currentValue * secondValue;
                        break;
                    case "/":
                        if (secondValue != 0)
                            result = currentValue / secondValue;
                        else
                            MessageBox.Show("Cannot divide by zero!");
                        break;
                }

                txtDisplay.Text = result.ToString();
                currentOperation = "";
                operationPending = true;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            txtDisplay.Text = "";
            currentValue = 0;
            currentOperation = "";
            operationPending = false;
        }

        // Scientific calculator event handlers
        private void SciNumberButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            txtDisplaySci.Text += button.Text;
        }

        private void SciFunction_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (!string.IsNullOrEmpty(txtDisplaySci.Text))
            {
                double value = double.Parse(txtDisplaySci.Text);
                double result = 0;
                switch (button.Text)
                {
                    case "sin":
                        result = Math.Sin(value * Math.PI / 180); // assuming degrees
                        break;
                    case "cos":
                        result = Math.Cos(value * Math.PI / 180);
                        break;
                    case "tan":
                        result = Math.Tan(value * Math.PI / 180);
                        break;
                    case "log":
                        result = Math.Log10(value);
                        break;
                    case "ln":
                        result = Math.Log(value);
                        break;
                    case "√":
                        result = Math.Sqrt(value);
                        break;
                    case "x²":
                        result = value * value;
                        break;
                }
                txtDisplaySci.Text = result.ToString();
            }
        }

        private void SciConstant_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Text == "π")
                txtDisplaySci.Text = Math.PI.ToString();
            else if (button.Text == "e")
                txtDisplaySci.Text = Math.E.ToString();
        }

        private void SciClear_Click(object sender, EventArgs e)
        {
            txtDisplaySci.Text = "";
        }

        private void SciDecimal_Click(object sender, EventArgs e)
        {
            if (!txtDisplaySci.Text.Contains("."))
                txtDisplaySci.Text += ".";
        }

        // Converter event handlers
        private void ConvertWeight_Click(object sender, EventArgs e)
        {
            if (double.TryParse(txtWeightFrom.Text, out double value))
            {
                string from = cmbWeightFrom.SelectedItem.ToString();
                string to = cmbWeightTo.SelectedItem.ToString();
                double result = ConvertWeight(value, from, to);
                txtWeightTo.Text = result.ToString();
            }
        }

        private double ConvertWeight(double value, string from, string to)
        {
            // Convert to kg first
            double kg = 0;
            switch (from)
            {
                case "kg": kg = value; break;
                case "lb": kg = value / 2.20462; break;
                case "g": kg = value / 1000; break;
                case "oz": kg = value / 35.274; break;
            }
            // Convert from kg to target
            switch (to)
            {
                case "kg": return kg;
                case "lb": return kg * 2.20462;
                case "g": return kg * 1000;
                case "oz": return kg * 35.274;
                default: return 0;
            }
        }

        private void CalcAge_Click(object sender, EventArgs e)
        {
            DateTime birth = dtpBirth.Value;
            DateTime now = DateTime.Now;
            int years = now.Year - birth.Year;
            int months = now.Month - birth.Month;
            int days = now.Day - birth.Day;

            if (days < 0)
            {
                months--;
                days += DateTime.DaysInMonth(now.Year, now.Month == 1 ? 12 : now.Month - 1);
            }
            if (months < 0)
            {
                years--;
                months += 12;
            }

            lblAgeResult.Text = $"Age: {years} years, {months} months, {days} days";
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CalculatorForm());
        }
    }
}