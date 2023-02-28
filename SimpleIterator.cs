using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Computational_mathematics
{
    class SimpleIterator : IFuncCalculator
    {
        //Функция, в которой ищем решение
        private FunctionPattern _Function = null;
        //Функция графика пересечения для поиска корней
        private FunctionPattern _IntersectionFunction = null;
        //Требуемая точность
        private double _accuracy;
        //Шаг для производной
        private const double _stepDerivative = 0.00000000000065;
        //Максимум итераций для поиска максимума
        private double _countMaxIteration = 20;

        /// <summary>
        /// Конструктор объекта простых итераций
        /// </summary>
        /// <param name="mainFunction">Главная функция, в которой требуется найти ответ</param>
        /// <param name="accuracy">Точность ответа</param>
        public SimpleIterator(FunctionPattern mainFunction, double accuracy = 0.001)
        {
            if (mainFunction == null)
                throw new ArgumentException("Главная функция не должна быть пустой");
            else
                _Function = new FunctionPattern(mainFunction);
            _accuracy = accuracy;
            _IntersectionFunction = new FunctionPattern(_SimpleIntersectionFunction);
        }

        /// <summary>
        /// Стандартная функция пересечения для поиска ответа
        /// </summary>
        /// <param name="x">Значение аргумента x функции</param>
        /// <returns>Значенине функции при данном корне x</returns>
        private double _SimpleIntersectionFunction(double x) => x;
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
        public FunctionPattern IntersectionFunction
        {
            get => _IntersectionFunction;
            set
            {
                if (value == null)
                    throw new ArgumentException("Функция графика пересечения не должна быть пустой");
                else
                    _IntersectionFunction = value;
            }
        }
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
            
            //Максимум функции и переменные промежуточных расчётов
            double max = Math.Abs(FunctionDerivative(minX)), tmpX = minX, tmpResult, y,
                step = (maxX - minX) / _countMaxIteration;
            //Поиск максимума функции
            do
            {
                tmpX += step;
                max = Math.Max(max, Math.Abs(FunctionDerivative(tmpX)));
            }
            while (tmpX < maxX);

            //Поиск решений
            tmpX = minX;
            do
            {
                //Функция замены
                tmpResult = tmpX + (-1d / max) * _Function(tmpX);
                //

                //Если функция замены и функция пересечения расходятся - инвертируем шаг
                if (tmpX > tmpResult)
                    tmpResult =tmpX+ (tmpX - tmpResult);

                //Если результат на данном X с достаточной точностью приблежён к искомому решению,
                //то добавляем x к массиву результатов
                y = _Function(tmpResult);
                if (Math.Abs((variableY - y)) < _accuracy)
                    return tmpResult;
                tmpX = tmpResult;
            }
            while(tmpX<maxX); //Продолжаем пока не пройдём весь промежуток поиска функции
            throw new AggregateException("Не удалось найти решения на промежутке");
        }
    }
}
