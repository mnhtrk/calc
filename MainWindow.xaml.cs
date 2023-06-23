using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement i in grid.Children)
            {
                if (i is Button)
                {
                    ((Button) i).Click += (s, e) => ButtonClick(s, e, (Button) i);
                }
            }

            foreach (UIElement i in listBox.Items)
            {
                if (i is StackPanel)
                {
                    foreach(UIElement j in ((StackPanel) i).Children)
                    {
                        if (j is Button)
                        {
                            ((Button) j).Click += (s, e) => ButtonClick(s, e, (Button) j);
                        }
                    }
                }
            }
        }

        string x = "0";
        string y = "";
        string op = "";
        bool isCommaX = false;
        bool isCommaY = false;
        bool isY = false;
        bool isLastEq = false;
        bool isOper = false;

        private void ButtonClick(object sender, RoutedEventArgs e, Button button)
        {
            Calc c = new Calc();
            string cont = button.Content.ToString();
            if (cont[0] <= '9' && cont[0] >= '0' || cont == ",")
            {
                if (isLastEq == true)
                {
                    isLastEq = false;
                    x = "0";
                    isCommaX = false;
                    y = "";
                    op = "";
                    textUpper.Text = "";
                }

                if (op == "" && x.Length <= 16 || isOper == true)
                {
                    if (isOper == true)
                    {
                        x = "0";
                        isCommaX = false;
                        isOper = false;
                        textUpper.Text = "";
                    }

                    if (x == "0" && cont != "," && isY == false)
                    {
                        x = "";
                    }

                    if (cont == ",")
                    {
                        if (isCommaX == false)
                        {
                            x += cont;
                            isCommaX = true;
                        }
                    }
                    else
                    {
                        x += cont;
                    }

                    text.Text = x;
                }
                else if (op != "" && y.Length <= 16 || isOper == true)
                {
                    if (isY == false)
                    {
                        isY = true;
                    }

                    if (isOper == true)
                    {
                        y = "0";
                        isCommaY = false;
                        isOper = false;
                        textUpper.Text = x + op;
                    }

                    if (y == "0" && cont != ",")
                    {
                        y = "";
                    }

                    if (cont == ",")
                    {
                        if (isCommaY == false)
                        {
                            y += cont;
                            isCommaY = true;
                        }
                    }
                    else
                    {
                        y += cont;
                    }

                    text.Text = y;
                }
            }
            else
            {
                if (Grid.GetRow(button) > 2)
                {
                    if (cont != "+" && cont != "-" && cont != "÷" && cont != "×" && cont != "=" && isLastEq == false)
                    {
                        if (isY == false && op != "")
                        {
                            isY = true;
                            if (y == "")
                            {
                                y = String.Copy(x);
                            }
                        }
                    }

                    if (cont == "Back")
                    {
                        if (isY == true)
                        {
                            if (y.Length != 1)
                            {
                                if (y[y.Length - 1] == ',')
                                {
                                    isCommaY = false;
                                }
                                y = y.Substring(0, y.Length - 1);
                                if (y == "-")
                                {
                                    y = "0";
                                }
                            }
                            else
                            {
                                y = "0";
                            }
                            text.Text = y;
                        }
                        else
                        {
                            if (x.Length != 1)
                            {
                                if (x[x.Length - 1] == ',')
                                {
                                    isCommaX = false;
                                }
                                x = x.Substring(0, x.Length - 1);
                                if (x == "-")
                                {
                                    x = "0";
                                }
                            }
                            else
                            {
                                x = "0";
                            }
                            text.Text = x;
                        }
                    }
                    else if (cont == "C")
                    {
                        textUpper.Text = "";
                        text.Text = "0";
                        x = "0";
                        y = "";
                        isCommaX = false;
                        isCommaY = false;
                        isY = false;
                        op = "";
                    }
                    else if (cont == "CE")
                    {
                        if (isY == true)
                        {
                            y = "0";
                            isCommaY = false;
                            text.Text = y;
                        }
                        else
                        {
                            textUpper.Text = "";
                            x = "0";
                            isCommaX = false;
                            text.Text = x;
                            op = "";
                        }
                    }
                    else if (cont == "%")
                    {
                        if (isY == true)
                        {
                            if (op == "+" || op == "-")
                            {
                                y = c.PercentSumDiff(x, y);
                            }
                            else
                            {
                                y = c.PercentMultDiv(y);
                            }
                            textUpper.Text = x + op + y;
                            text.Text = y;
                        }
                        else
                        {
                            x = "0";
                            text.Text = x;
                            textUpper.Text = x;
                        }
                        isOper = true;
                    }
                    else if (cont == "²√x")
                    {
                        if (isY == true)
                        {
                            if (Convert.ToDouble(y) >= 0)
                            {
                                textUpper.Text = x + op + $"√({y})";
                                y = c.Root(y);
                                text.Text = y;
                                isOper = true;
                            }
                            else
                            {
                                textUpper.Text = "";
                                text.Text = "Неверный ввод";
                                x = "0";
                                y = "";
                                op = "";
                                isOper = false;
                                isY = false;
                                isCommaX = false;
                                isCommaY = false;
                                isLastEq = false;
                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(x) >= 0)
                            {
                                textUpper.Text = $"√({x})";
                                x = c.Root(x);
                                text.Text = x;
                                isOper = true;
                            }
                            else
                            {
                                textUpper.Text = "";
                                text.Text = "Неверный ввод";
                                x = "0";
                                y = "";
                                op = "";
                                isOper = false;
                                isY = false;
                                isCommaX = false;
                                isCommaY = false;
                                isLastEq = false;
                            }
                        }
                    }
                    else if (cont == "x²")
                    {
                        if (isY == true)
                        {
                            textUpper.Text = x + op + $"sqr({y})";
                            y = c.Square(y);
                            text.Text = y;
                        }
                        else
                        {
                            textUpper.Text = $"sqr({x})";
                            x = c.Square(x);
                            text.Text = x;
                        }
                        isOper = true;
                    }
                    else if (cont == "⅟ₓ")
                    {
                        if (isY == true)
                        {
                            if (y == "0")
                            {
                                textUpper.Text = "";
                                text.Text = "Деление на ноль невозможно";
                                x = "0";
                                y = "";
                                op = "";
                                isOper = false;
                                isY = false;
                                isCommaX = false;
                                isCommaY = false;
                                isLastEq = false;
                            }
                            else
                            {
                                textUpper.Text = x + op + $"1/({y})";
                                y = c.Reverse(y);
                                text.Text = y;
                                isOper = true;
                            }
                        }
                        else if (isY == false)
                        {
                            if (x == "0")
                            {
                                textUpper.Text = "";
                                text.Text = "Деление на ноль невозможно";
                                x = "0";
                                y = "";
                                op = "";
                                isOper = false;
                                isY = false;
                                isCommaX = false;
                                isCommaY = false;
                                isLastEq = false;
                            }
                            else
                            {
                                textUpper.Text = $"1/({x})";
                                x = c.Reverse(x);
                                text.Text = x;
                                isOper = true;
                            }
                        }
                    }
                    else if (cont == "±")
                    {
                        if (isY == true && y != "0")
                        {
                            y = c.PosNeg(y);
                            text.Text = y;
                        }
                        else if (isY == false && x != "0")
                        {
                            x = c.PosNeg(x);
                            text.Text = x;
                        }
                    }
                    else // операции
                    {
                        isOper = false;
                        if (isY == false)
                        {
                            if (x[x.Length - 1] == ',')
                            {
                                x = x.Substring(0, x.Length - 1);
                                text.Text = x;
                            }
                            if (cont == "=")
                            {
                                if (op != "")
                                {
                                    if (isY == false)
                                    {
                                        isY = true;
                                        if (y == "")
                                        {
                                            y = String.Copy(x);
                                        }
                                    }

                                    if (isLastEq == false)
                                    {
                                        isLastEq = true;
                                    }
                                }
                                else
                                {
                                    textUpper.Text = x + "=";
                                }
                            }
                            else
                            {
                                if (isLastEq == true)
                                {
                                    isLastEq = false;
                                    y = "";
                                }
                                op = cont;
                                textUpper.Text = x + op;
                            }
                        }

                        if (isY == true)
                        {
                            if (y[y.Length - 1] == ',')
                            {
                                y = y.Substring(0, y.Length - 1);
                            }
                            string temp = String.Copy(x);

                            if (op == "÷")
                            {
                                x = c.Div(x, y);
                            }
                            else if (op == "×")
                            {
                                x = c.Mult(x, y);
                            }
                            else if (op == "+")
                            {
                                x = c.Sum(x, y);
                            }
                            else if (op == "-")
                            {
                                x = c.Diff(x, y);
                            }

                            Button journal = new Button();
                            journal.Content = temp + " " + op + " " + y + " = " + x;
                            journal.FontSize = 16;
                            journal.Background = Brushes.White;
                            journal.Click += (s, e) => ButtonClickJournal(s, e, journal);
                            Journal.Items.Add(journal);

                            if (cont != "=")
                            {
                                isLastEq = false;
                                op = cont;
                                textUpper.Text = x + op;
                                text.Text = x;
                                y = "";
                            }
                            else
                            {
                                textUpper.Text = temp + op + y + cont;
                                text.Text = x;
                                isLastEq = true;
                            }

                            isY = false;
                            isCommaY = false;
                            isOper = false;
                        }
                    }
                }
                else
                {
                    if (cont == "MS")
                    {
                        GroupBox groupBox = new GroupBox();
                        StackPanel stackPanel = new StackPanel();
                        stackPanel.Orientation = Orientation.Horizontal;

                        Button num = new Button();
                        if (isY == true)
                        {
                            num.Content = y;
                        }
                        else
                        {
                            num.Content = x;
                        }
                        num.FontSize = 16;
                        num.Background = Brushes.White;
                        num.Click += (s, e) => ButtonClickMem(s, e, num, num, groupBox);

                        Button del = new Button();
                        del.FontSize = 16;
                        del.Background = Brushes.LightGray;
                        del.Content = "MC";
                        del.Click += (s, e) => ButtonClickMem(s, e, del, num, groupBox);
                        stackPanel.Children.Add(del);

                        Button plus = new Button();
                        plus.FontSize = 16;
                        plus.Background = Brushes.LightGray;
                        plus.Content = "M+";
                        plus.Click += (s, e) => ButtonClickMem(s, e, plus, num, groupBox);
                        stackPanel.Children.Add(plus);

                        Button minus = new Button();
                        minus.Name = "minus";
                        minus.FontSize = 16;
                        minus.Background = Brushes.LightGray;
                        minus.Content = "M-";
                        minus.Click += (s, e) => ButtonClickMem(s, e, minus, num, groupBox);
                        stackPanel.Children.Add(minus);

                        stackPanel.Children.Add(num);

                        groupBox.Content = stackPanel;
                        groupBox.Background = Brushes.White;
                        listBox.Items.Add(groupBox);
                    }
                    else if (cont == "MC")
                    {
                        listBox.Items.Clear();
                    }
                    else if (cont == "MR" && listBox.Items.Count != 0)
                    {
                        isOper = true;

                        if (op != "" && isLastEq == false)
                        {
                            isY = true;
                            y = ((StackPanel)((GroupBox)listBox.Items[listBox.Items.Count - 1]).Content).Children[3].ToString().Split(' ')[1];
                            text.Text = y;
                        }
                        else
                        {
                            x = ((StackPanel)((GroupBox)listBox.Items[listBox.Items.Count - 1]).Content).Children[3].ToString().Split(' ')[1];
                            text.Text = x;
                            textUpper.Text = "";
                            y = "";
                            op = "";
                            isOper = false;
                            isY = false;
                            isCommaY = false;
                            isLastEq = false;
                        }
                    }
                    else if (cont == "M+" && listBox.Items.Count != 0)
                    {
                        if (isY == true)
                        {
                            ((Button)((StackPanel)((GroupBox)listBox.Items[listBox.Items.Count - 1]).Content).Children[3]).Content = 
                                c.Sum(((StackPanel)((GroupBox)listBox.Items[listBox.Items.Count - 1]).Content).Children[3].ToString().Split(' ')[1], y);
                        }
                        else
                        {
                            ((Button)((StackPanel)((GroupBox)listBox.Items[listBox.Items.Count - 1]).Content).Children[3]).Content =
                                c.Sum(((StackPanel)((GroupBox)listBox.Items[listBox.Items.Count - 1]).Content).Children[3].ToString().Split(' ')[1], x);
                        }
                    }
                    else if (cont == "M-" && listBox.Items.Count != 0)
                    {
                        if (isY == true)
                        {
                            ((Button)((StackPanel)((GroupBox)listBox.Items[listBox.Items.Count - 1]).Content).Children[3]).Content =
                                c.Diff(((StackPanel)((GroupBox)listBox.Items[listBox.Items.Count - 1]).Content).Children[3].ToString().Split(' ')[1], y);
                        }
                        else
                        {
                            ((Button)((StackPanel)((GroupBox)listBox.Items[listBox.Items.Count - 1]).Content).Children[3]).Content =
                                c.Diff(((StackPanel)((GroupBox)listBox.Items[listBox.Items.Count - 1]).Content).Children[3].ToString().Split(' ')[1], x);
                        }
                    }
                }
            }

            if (x == "∞" || y == "∞")
            {
                textUpper.Text = "";
                text.Text = "Переполнение";
                x = "0";
                y = "";
                op = "";
                isOper = false;
                isY = false;
                isCommaX = false;
                isCommaY = false;
                isLastEq = false;
            }

            if (y == "0" && op == "÷" && cont == "=")
            {
                textUpper.Text = "";
                if (x == "0")
                {
                    text.Text = "Результат не определен";
                }
                else
                {
                    text.Text = "Деление на ноль невозможно";
                }
                x = "0";
                y = "";
                op = "";
                isOper = false;
                isY = false;
                isCommaX = false;
                isCommaY = false;
                isLastEq = false;
            }
        }

        private void ButtonClickJournal(object s, RoutedEventArgs e, Button journal)
        {
            x = journal.Content.ToString().Split(' ')[4];
            string temp = journal.Content.ToString().Split(' ')[0];
            y = journal.Content.ToString().Split(' ')[2];
            op = journal.Content.ToString().Split(' ')[1];
            text.Text = x;
            textUpper.Text = temp + op + y + "=";

            isY = false;
            isCommaY = false;
            isOper = false;
        }

        private void ButtonClickMem(object s, RoutedEventArgs e, Button button, Button num, GroupBox box)
        {
            Calc c = new Calc();
            string cont = button.Content.ToString();
            if (cont == "MC")
            {
                listBox.Items.Remove(box);
            }
            else if (cont == "M+")
            {
                if (isY == true)
                {
                    num.Content = c.Sum(num.Content.ToString(), y);
                }
                else
                {
                    num.Content = c.Sum(num.Content.ToString(), x);
                }
            }
            else if (cont == "M-")
            {
                if (isY == true)
                {
                    num.Content = c.Diff(num.Content.ToString(), y);
                }
                else
                {
                    num.Content = c.Diff(num.Content.ToString(), x);
                }
            }
            else
            {
                isOper = true;
                if (op != "" && isLastEq == false)
                {
                    isY = true;
                    y = num.Content.ToString();
                    text.Text = y;
                }
                else
                {
                    x = num.Content.ToString();
                    text.Text = x;
                    textUpper.Text = "";
                    y = "";
                    op = "";
                    isOper = false;
                    isY = false;
                    isCommaY = false;
                    isLastEq = false;
                }
            }
        }

        private void JournalClear(object s, RoutedEventArgs e)
        {
            Journal.Items.Clear();
        }
    }
}
