using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class Program
    {
        static Dictionary<string, IOperation> dict = new Dictionary<string, IOperation>()
            {
                {"+",new Plus()},
                {"-",new Minus()},
                {"*",new Multiplication()},
                {"/",new Division()}
            };
        string[] operation = new string[] { "*", "/", "+", "-" };
        static void Main(string[] args)
        {
            var input = Console.ReadLine();
            var Calc = new Program();
            Console.WriteLine(Calc.Calculator(input));
        }
        public double Calculator(string input)
        {
            List<string> list = new List<string>();
            string pattern = @"(\d+|\+|\-|\*|\/)";
            var matches = Regex.Matches(input, pattern);

            foreach (Match match in matches)
            {
                if (!string.IsNullOrWhiteSpace(match.Value))
                {
                    list.Add(match.Value);
                }
            }

            int removedCount = list.RemoveAll(item => string.IsNullOrWhiteSpace(item));

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == "-" && i == 0)
                {
                    list[i + 1] = "-" + list[i + 1];
                    list.RemoveAt(i);
                }
                else if (i != 0) 
                {
                    if (list[i] == "-" && !operation.Contains(list[i - 1]))
                        {
                            list[i + 1] = (-double.Parse(list[i + 1])).ToString();
                            list[i] = "+";
                        }
                    else if (list[i] == "-" && operation.Contains(list[i - 1]))
                    {
                        list[i + 1] = (-double.Parse(list[i + 1])).ToString();
                        list.RemoveAt(i);
                    }
                }
            }

            while (list.Contains(operation[0]) || list.Contains(operation[1]) || list.Contains(operation[2]))
            for (int i = 0; i < operation.Length; i++)
            {
                var index = 1;
                while (list.Contains(operation[i]) && index<list.Count)
                {
                    if (operation.Contains(list[index]) && list[index] == operation[i])
                    {
                        var work = new Work(dict[list[index]], double.Parse(list[index - 1]), double.Parse(list[index + 1]));
                        var result = work.IWork();
                        list.RemoveRange(index - 1, 3);
                        list.Insert(index - 1, result.ToString());
                    }
                    index++;
                }
            }
            return double.Parse(list[0]);
        }
    }

    interface IOperation
    {
        public double Algortithm(double a, double b);
    }
    public class Plus : IOperation
    {
        public double Algortithm(double a, double b) => a + b;
    }
    public class Minus : IOperation
    {
        public double Algortithm(double a, double b) => a - b;
    }
    public class Multiplication : IOperation
    {
        public double Algortithm(double a, double b) => a * b;
    }
    public class Division : IOperation
    {
        public double Algortithm(double a, double b) => a / b;
    }

    class Work
    {
        private IOperation _oper;
        private double _firstValue;
        private double _secondValue;

        public Work(IOperation oper, double firstValue, double secondValue)
        {
            _oper = oper;
            _firstValue = firstValue;
            _secondValue = secondValue;
        }

        public double IWork()
        {
            return _oper.Algortithm(_firstValue, _secondValue);
        }
    }
}