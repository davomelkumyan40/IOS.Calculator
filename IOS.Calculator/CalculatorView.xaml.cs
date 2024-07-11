using System.Linq.Expressions;

namespace IOS.Calculator
{
    public partial class CalculatorView : ContentPage
    {
        private double? a, b = null;
        private bool pointerToA = true;
        private char? sign = null, nextSign = null;
        public CalculatorView()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            input.FontSize = (input.Text.Length > 6) ? 44 : 77;
            lableGrid.RowDefinitions.First().Height = (input.Text.Length > 6) ? 70 : 107;


            if (double.TryParse(button.Text, out double inputValue))
            {
                input.Text = input.Text[0] == '0' ? $"{inputValue}" : input.Text + inputValue;
                cancellBtn.Text = input.Text.Any() ? "C" : "AC";
                if (pointerToA)
                    a = double.Parse(input.Text);
                else if (!pointerToA && b == null)
                    input.Text = $"{inputValue}";
                if (!pointerToA)
                    b = double.Parse(input.Text);

            }
            else if (a != null)
            {
                switch (button.Text)
                {
                    case "%":
                        input.Text = $"{a = a / 100}";
                        return;
                    case "AC":
                        input.Text = "0";
                        pointerToA = true;
                        a = b = sign = null;
                        return;
                    case "C":
                        input.Text = "0";
                        if (a != null && b == null)
                            a = 0;
                        else b = null;
                        cancellBtn.Text = "AC";
                        return;
                    case "-/+":
                        double? res = 0;
                        if (pointerToA && a != null)
                            res = a *= -1;
                        else if (!pointerToA && b != null)
                            res = b *= -1;
                        input.Text = $"{res}";
                    return;
                    default:
                        break;
                }

                if (b == null)
                {
                    sign = button.Text[0];
                    //button.BackgroundColor = Color.FromArgb("#fff");
                    //button.TextColor = Color.FromArgb("#000");
                }
                pointerToA = false;
                if (b != null)
                {
                    var result = sign switch
                    {
                        '+' => a + b,
                        '-' => a - b,
                        '÷' => a / b,
                        'x' => a * b,
                        '=' => a,
                        _ => null
                    };
                    input.Text = result.ToString();
                    a = result;
                    sign = button.Text[0];
                    b = null;
                }
            }
        }
    }

}
