using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Computational_mathematics
{
    class NewtonIterator : IFuncCalculator
    {
        //Функция, в которой ищем решение
        private FunctionPattern _Function = null;
        //Требуемая точность
        private double _accuracy;
        //Шаг для производной
        private const double _stepDerivative = 0.00000000000065;

        /// <summary>
        /// Конструктор объекта простых итераций
        /// </summary>
        /// <param name="mainFunction">Главная функция, в которой требуется найти ответ</param>
        /// <param name="accuracy">Точность ответа</param>
        public NewtonIterator(FunctionPattern mainFunction, double accuracy = 0.001)
        {
            if (mainFunction == null)
                throw new ArgumentException("Главная функция не должна быть пустой");
            else
                _Function = new FunctionPattern(mainFunction);
            _accuracy = accuracy;
        }

        public double accuracy
        {
            get => _accuracy;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Точность не может быть меньше и равна нулю.");
                else
                    _accuracy = value;
            }
        }
        public FunctionPattern Function
        {
            get => _Function;
            set
            {
                if (value == null)
                    throw new ArgumentException("Главная функция не должна быть пустой");
                else
                    _Function = value;
            }
        }
        /// <summary>
        /// Производная главной функции
        /// </summary>
        /// <param name="x">Значение аргумента x функции</param>
        /// <returns>Значение производной</returns>
        public double FunctionDerivative(double x) => (Function(x + _stepDerivative) - Function(x)) / (_stepDerivative);

        /// <summary>
        /// Производная второго порядка главной функции
        /// </summary>
        /// <param name="x">Значение аргумента x функции</param>
        /// <returns>Значение производной второго порядка</returns>
        public double SecondFunctionDerivative(double x) => (FunctionDerivative(x+_stepDerivative)-FunctionDerivative(x))/ _stepDerivative;

        /// <summary>
        /// Поиск решений на заданном интервале. Возвращает массив значений x, при которой функция равна y
        /// </summary>
        /// <param name="variableY">Значение результата функции</param>
        /// <param name="minX"></param>
        /// <param name="maxX"></param>
        /// <returns>Массив корней x, при которых функция равна y</returns>
        public double SolveEquation(double variableY, double minX, double maxX)
        {
            //Проверка промежутка
            if (maxX <= minX)
                throw new ArgumentException("Конец промежутка поиска не может быть меньше или равен минимума");

            double x = minX, tmpResult, derivative;
            while (true)
            {
                tmpResult = Function(x);

                derivative = FunctionDerivative(x);
                //Если касательная приблизилась к ответу, то
                if (Math.Abs(variableY - tmpResult) < accuracy)
                    return x;

                //Итерируем x по методу Ньютона. Если итерация выходит за пределы промежутка
                //То уменьшаем её длину, что бы она оказалась в промежутке
                x -= (tmpResult / derivative);

                if (x < minX || x > maxX)
                    throw new ArithmeticException("На отрезке не получилось найти решение");
            }
        }
    }
}
