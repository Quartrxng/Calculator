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
            if (input[0] == '-')
            {
                list.Add(input.Substring(0, 2));
                input = input.Substring(2);
            }
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
            for (int i = 0; i < operation.Length; i++)
            {
                while (list.Contains(operation[i]))
                {
                    var index = 0;
                    for (int j = 0; j < list.Count; j++)
                    {
                        if (list[j] == operation[i])
                        {
                            index = j;
                        }
                    }
                    var work = new Work(dict[list[index]], double.Parse(list[index - 1]), double.Parse(list[index + 1]));
                    var result = work.IWork();
                    list.RemoveRange(index - 1, 2);
                    list[index - 1] = result.ToString();
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
