using System.Diagnostics;

namespace Calculator
{
    public partial class Calculator : Form
    {
        public int result = 0;
        public int val_num = 0; // the number of inputed value
        public string input = ""; // inputed number
        public string ope = ""; // to store operator 
        public int[] val = new int[128]; // to store value 

        public bool start_flag = true; // indicating the start of input
        public int error_num = 0;

        public Calculator()
        {
            InitializeComponent();
        }

        private void Result_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Input_1_Click(object sender, EventArgs e)
        {
            Input_Value("1");
        }

        private void Input_2_Click(object sender, EventArgs e)
        {
            Input_Value("2");
        }


        private void Input_3_Click(object sender, EventArgs e)
        {
            Input_Value("3");
        }

        private void Input_4_Click(object sender, EventArgs e)
        {
            Input_Value("4");
        }

        private void Input_5_Click(object sender, EventArgs e)
        {
            Input_Value("5");
        }

        private void Input_6_Click(object sender, EventArgs e)
        {
            Input_Value("6");
        }

        private void Input_7_Click(object sender, EventArgs e)
        {
            Input_Value("7");
        }

        private void Input_8_Click(object sender, EventArgs e)
        {
            Input_Value("8");
        }

        private void Input_9_Click(object sender, EventArgs e)
        {
            Input_Value("9");
        }

        private void Input_0_Click(object sender, EventArgs e)
        {
            Input_Value("0");
        }
        private void Operator_plus_Click(object sender, EventArgs e)
        {
            Input_Operator("+");
        }

        private void operator_minus_Click(object sender, EventArgs e)
        {
            if ((start_flag || (val_num > 0) && input == ""))
            {
                Input_Value("-");
                return;
            }
            Input_Operator("-");
        }
        private void operator_multi_Click(object sender, EventArgs e)
        {
            Input_Operator("*");
        }


        private void operator_div_Click(object sender, EventArgs e)
        {
            Input_Operator("/");
        }

        private void Operator_equal_Click(object sender, EventArgs e)
        {
            val[val_num++] = int.Parse(input);
            input = "";
            Calculate_Input();
            Result_exp.Text = Result_box.Text;
            if (error_num != 0)
            {
                Error_Show(error_num);
                error_num = 0;
                val[0] = 0;
            }
            ope = "";
            val_num = 0;
            Result_box.Text = val[0].ToString();
            input = Result_box.Text;
            start_flag = true;
        }

        public void Input_Value(string _value)
        {
            /* 0 -> input 4 -> 4 */
            if (start_flag)
            {
                Result_box.Text = "";
                input = "";
                start_flag = false;
            }
            /* 0 -> input 0 -> 0  */
            if (input == "0")
            {
                return;
            }
            input += _value;

            /* cheak overflow */
            if (input.Length >= 10)
            {
                int a = 0;
                if (input[0] == '-')
                    a = 1;
                if (input[0 + a] > 0x0032 || int.Parse(input.Substring(1 + a)) > 147483647 + a)
                {
                    input = input.Substring(0, 9 + a);
                    return;
                }
                else if (input.Length > 10 + a)
                {
                    input = input.Substring(0, 10 + a);
                    return;
                }
            }
            Result_box.Text += _value;
        }

        public void Input_Operator(string _operator)
        {
            /* cheak locate of the operator  */
            /* e.g "+"4A3*"+" */
            if (start_flag && (input == "") || input == "-")
                return;
            ope += _operator;
            val[val_num++] = int.Parse(input);
            input = "";
            Result_box.Text += _operator;
            start_flag = false;
        }

        public void Calculate_Input()
        {
            int INT_MAX = 2147483647;
            int INT_MIN = -2147483648;

            double cheaker;

            for (int i = ope.Length - 1; i >= 0; i--)
            {
                if (ope[i] == '*')
                {
                    if (val[i] < 0 && val[i + 1] < 0)
                    {
                        cheaker = INT_MAX / val[i + 1];
                        if (val[i] < cheaker)
                        {
                            error_num = 1;
                            return;
                        }

                    }
                    else if (val[i] > 0 && val[i + 1] < 0)
                    {
                        cheaker = INT_MIN / val[i];
                        if (val[i + 1] < cheaker)
                        {
                            error_num = 1;
                            return;
                        }
                    }
                    else if (val[i] < 0)
                    {
                        cheaker = INT_MIN / val[i + 1];
                        if (val[i] < cheaker)
                        {
                            error_num = 1;
                            return;
                        }
                    }
                    else
                    {
                        cheaker = INT_MAX / val[i + 1];
                        if (val[i] > cheaker)
                        {
                            error_num = 1;
                            return;
                        }
                    }

                    val[i] = val[i] * val[i + 1];
                }
                else if (ope[i] == '/')
                {
                    if (val[i + 1] == 0)
                    {
                        error_num = 2;
                        return;
                    }
                    val[i] = val[i] / val[i + 1];
                }
            }
            for (int i = 0; i < ope.Length; i++)
            {
                if (ope[i] == '+')
                {
                    if (val[i + 1] > 0)
                    {
                        if (INT_MAX - val[i + 1] <= val[0])
                        {
                            error_num = 1;
                            return;
                        }
                    }
                    val[0] = val[0] + val[i + 1];
                }
                else if (ope[i] == '-')
                {
                    if (val[i + 1] < 0)
                    {
                        if (val[0] < INT_MIN - val[i + 1])
                        {
                            error_num = 1;
                            return;
                        }
                    }
                    else
                    {
                        if (val[0] < INT_MIN + val[i + 1])
                        {
                            error_num = 1;
                            return;
                        }
                    }
                    val[0] = val[0] - val[i + 1];
                }
            }
        }

        public void Error_Show(int i)
        {
            switch (i)
            {
                case 1:
                    MessageBox.Show("Error: overflow");
                    break;
                case 2:
                    MessageBox.Show("Error: cannot divide by zero");
                    break;
                default:
                    break;
            }
        }

        private void Resetbtn_Click(object sender, EventArgs e)
        {
            Result_box.Text = "0";
            val_num = 0;
            input = "";
            ope = "";
            start_flag = true;
        }

        private void Result_exp_TextChanged(object sender, EventArgs e)
        {

        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBtn.Change_Btncolor(Color.Red);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBtn.Change_Btncolor(Color.Green);
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBtn.Change_Btncolor(Color.Yellow);
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InputBtn.Change_Btncolor(Color.Black);
        }
    }
}
