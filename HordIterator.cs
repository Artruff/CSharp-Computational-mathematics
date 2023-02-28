using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Computational_mathematics
{
    class HordIterator : IFuncCalculator
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
        public HordIterator(FunctionPattern mainFunction, double accuracy = 0.001, double gapsWidth = 1)
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
        /// <returns>Массив корней x, при которых функция равна y</returns>
        public double FunctionDerivative(double x) => (Function(x + _stepDerivative) - Function(x)) / (_stepDerivative);

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

            double minValue = Function(minX), maxValue = Function(maxX), x;
            
            //Если прямая значений функции в границах промежутка не пересекает
            //искомое значение, то берём следующий промежуток
            //Иначе итерируем x по методу хорд, пока не приблизимся к решению
            if(minValue<variableY && maxValue> variableY)
            {
                x = minX;
                while(Math.Abs(variableY-Function(x))>accuracy)
                    x = x - ((maxX - x) * Function(x)) / (Function(maxX) - Function(x));
                return x;
            }
            else if(minValue > variableY && maxValue< variableY)
            {
                x = maxX;
                while (Math.Abs(variableY - Function(x)) > accuracy)
                    x = x-((x- minX) *Function(x))/(Function(x)-Function(minX));
                return x;
            }
            throw new ArithmeticException("На отрезке не получилось найти решение");
        }
    }
}
