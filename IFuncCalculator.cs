using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Computational_mathematics
{
    delegate double FunctionPattern(double x);
    interface IFuncCalculator
    {
        /// <summary>
        /// Точность ответа
        /// </summary>
        double accuracy { get; set; }

        /// <summary>
        /// Функция, где ищем ответ
        /// </summary>
        FunctionPattern Function { get; set; }

        /// <summary>
        /// Поиск решений на заданном интервале. Возвращает массив значений x, при которой функция равна y
        /// </summary>
        /// <param name="variableY">Значение результата функции</param>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <returns></returns>
        double SolveEquation(double variableY, double minX, double maxX);
    }
}
