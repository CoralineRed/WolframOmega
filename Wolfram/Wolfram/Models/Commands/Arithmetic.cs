using System;
using System.Collections.Generic;
using System.Text;

namespace WolframOmega
{
    class Arithmetic : IBotCommand, INeedResponse, ICalculation
    {
        public string Command => "/arithmetic";

        public string Reference => "считает арифметическое выражение, состоящее из чисел и знаков +, -, /, *, (, )";

        public string AskResponse => "Cчитает арифметическое выражение, состоящее из чисел и знаков +, -, /, *, (, ). Например: (2 + 2) * 2.";

        public string Response { get; set; }

        private Dictionary<char, int> priority = new Dictionary<char, int>
        { ['*'] = 0, ['/'] = 0, ['+'] = 1, ['-'] = 1, ['('] = 2, [')'] = 2 };

        private Dictionary<char, Func<double, double, double>> operations = new Dictionary<char, Func<double, double, double>>
        {
            ['*'] = (x, y) => x * y,
            ['/'] = (x, y) => x / y,
            ['+'] = (x, y) => x + y,
            ['-'] = (x, y) => x - y,
        };

        public string Responce()
        {
            var operators = new Stack<char>();
            var numbers = new Stack<double>();
            Response = Response.Replace(" ", "");
            if (Response.Length == 0) throw new ArgumentException("Пустая строка");
            for (int i = 0; i < Response.Length; i++)
            {
                if (char.IsDigit(Response[i]))
                {
                    var snumber = new StringBuilder();
                    while (i < Response.Length && (char.IsDigit(Response[i]) || Response[i] == '.' || Response[i] == ','))
                    {
                        snumber.Append(Response[i]);
                        i++;
                    }
                    i--;
                    if (!double.TryParse(snumber.ToString(), out double number))
                        throw new ArgumentException("Неправильно введено число " + snumber);
                    numbers.Push(number);
                }
                else if (priority.ContainsKey(Response[i]))
                {
                    if (Response[i] == '(') operators.Push(Response[i]);
                    else if (Response[i] == ')')
                    {
                        while (operators.Count != 0 && operators.Peek() != '(')
                            CountLastOperation(operators, numbers);
                        if (operators.Count == 0)
                            throw new ArgumentException("Неправильно расставлены скобки");
                        else operators.Pop();
                    }
                    else
                    {
                        while (operators.Count != 0 && priority[operators.Peek()] <= priority[Response[i]])
                            CountLastOperation(operators, numbers);
                        operators.Push(Response[i]);
                    }
                }
                else throw new ArgumentException("Нераспознанный символ: " + Response[i]);
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
