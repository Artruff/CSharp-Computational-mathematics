using System;

namespace CSharp_Computational_mathematics
{
    class Program
    {
        //Оригинальная функция
        static double Function1(double x) => -Math.Pow(x, 4) + 15d * Math.Pow(x, 2) + 12d * x - 10d;

        //Функция из другого варианта (4) для проверки
        static double Function2(double x) => 2d * Math.Pow(x, 4) - 24d * Math.Pow(x, 2) - x + 8d;

        //Функция из другого варианта (2) для проверки
        static double Function3(double x) => Math.Pow(2, x) - 5d * Math.Pow(x, 2) + 10d;

        //Функция из другого варианта (5) для проверки
        static double Function4(double x) => Math.Pow(Math.E, x) - 4d * Math.Pow(x, 2) - 3d*x;

        static double AdditionalFunction(double x) => 100d * Math.Pow(x, 2) - 10000d * x - 3d;

        static void Main(string[] args)
        {
            for (int i = 0; i < 200; i++)
            {
                IFuncCalculator calculator = new SimpleIterator(new FunctionPattern(AdditionalFunction), Math.Pow(0.1, i));
                var res = calculator.SolveEquation(0, -1, 0);
                Console.WriteLine("iter: " + i.ToString());
                Console.WriteLine("func: " + calculator.Function(res));
                Console.WriteLine(res);
            }

        }
    }
}
