using System;

namespace CSharp_Computational_mathematics
{
    class Program
    {
        //Оригинальная функция
        static double Function1(double x) => -Math.Pow(x, 4) + 15d * Math.Pow(x, 2) + 12d * x - 10d;

        //Функция из другого варианта (4) для проверки
        static double Function2(double x) => 2d * Math.Pow(x, 4) - 24d * Math.Pow(x, 2) - x + 8;

        //Функция из другого варианта (2) для проверки
        static double Function3(double x) => Math.Pow(2, x) - 5d * Math.Pow(x, 2) + 10d;

        //Функция из другого варианта (5) для проверки
        static double Function4(double x) => Math.Pow(Math.E, x) - 4d * Math.Pow(x, 2) - 3d*x;

        static void Main(string[] args)
        {
            ISimpleIteration si = new SimpleIteration(new FunctionPattern(Function1));
            var res = si.SolveEquation(0, -10, 10);
            foreach(double r in res)
            {
                Console.WriteLine(r);
            }
        }
    }
}
