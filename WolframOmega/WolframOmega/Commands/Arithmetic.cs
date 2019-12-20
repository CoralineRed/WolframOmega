using System;
using System.Collections.Generic;
using System.Text;

namespace WolframOmega
{
    class Arithmetic : IBotCommand, ICalculation
    {
        public string Command => "/arithmetic";

        public string Reference => "считает арифметическое выражение, состоящее из чисел и знаков +, -, /, *, (, )";

        public string Message => "Cчитает арифметическое выражение, состоящее из чисел и знаков +, -, /, *, (, ). Например: (2 + 2) * 2.";

        private Dictionary<char, int> priority = new Dictionary<char, int>
            { ['*'] = 0, ['/'] = 0, ['+'] = 1, ['-'] = 1, ['('] = 2, [')'] = 2 };

        private Dictionary<char, Func<double, double, double>> operations = new Dictionary<char, Func<double, double, double>>
        {
            ['*'] = (x, y) => x * y,
            ['/'] = (x, y) => x / y,
            ['+'] = (x, y) => x + y,
            ['-'] = (x, y) => x - y,
        };

        public string Calculate(string input)
        {
            var operators = new Stack<char>();
            var numbers = new Stack<double>();
            input = input.Replace(" ", "");
            if (input.Length == 0) throw new ArgumentException("Пустая строка");
            for (int i = 0; i < input.Length; i++)
            {
                if (char.IsDigit(input[i]))
                {
                    var snumber = new StringBuilder();
                    while (i < input.Length && (char.IsDigit(input[i]) || input[i] == '.' || input[i] == ','))
                    {
                        snumber.Append(input[i]);
                        i++;
                    }
                    i--;
                    if (!double.TryParse(snumber.ToString(), out double number))
                        throw new ArgumentException("Неправильно введено число " + snumber);
                    numbers.Push(number);
                }
                else if (priority.ContainsKey(input[i]))
                {
                    if (input[i] == '(') operators.Push(input[i]);
                    else if (input[i] == ')')
                    {
                        while (operators.Count != 0 && operators.Peek() != '(')
                            CountLastOperation(operators, numbers);
                        if (operators.Count == 0)
                            throw new ArgumentException("Неправильно расставлены скобки");
                        else operators.Pop();
                    }
                    else
                    {
                        while (operators.Count != 0 && priority[operators.Peek()] <= priority[input[i]])
                            CountLastOperation(operators, numbers);
                        operators.Push(input[i]);
                    }
                }
                else throw new ArgumentException("Нераспознанный символ: " + input[i]);
            }
            while (operators.Count != 0)
                if (operators.Peek() != '(') CountLastOperation(operators, numbers);
                else throw new ArgumentException("Неправильно расставлены скобки");
            if (numbers.Count != 1) throw new ArgumentException("Неправильно составлено выражение");
            else return numbers.Peek().ToString();
        }

        private void CountLastOperation(Stack<char> operators, Stack<double> numbers)
        {
            if (numbers.Count > 1)
            {
                var x = numbers.Pop();
                var y = numbers.Pop();
                numbers.Push(operations[operators.Pop()](y, x));
            }
            else throw new ArgumentException("Неправильно составлено выражение");
        }
    }
}
