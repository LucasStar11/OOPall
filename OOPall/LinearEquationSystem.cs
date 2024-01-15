using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPall
{
    public class LinearEquationSystem<T> where T : INumberField<T>
    {
        private Vector1<T>[] coefficientsMatrix; // Матрица коэффициентов (A)
        private Vector1<T> constantsVector;      // Столбец свободных членов (b)

        public LinearEquationSystem(Vector1<T>[] coefficientsMatrix, Vector1<T> constantsVector)
        {
            if (coefficientsMatrix == null || constantsVector == null)
            {
                throw new ArgumentNullException("Matrix and vector cannot be null.");
            }

            // Проверка совпадения размерностей матрицы и вектора
            int matrixSize = coefficientsMatrix.Length;
            int vectorSize = constantsVector.Dimension;

            if (matrixSize == 0 || coefficientsMatrix[0].Dimension != vectorSize)
            {
                throw new ArgumentException("Invalid matrix or vector dimensions.");
            }

            for (int i = 1; i < matrixSize; i++)
            {
                if (coefficientsMatrix[i].Dimension != vectorSize)
                {
                    throw new ArgumentException("Invalid matrix or vector dimensions.");
                }
            }

            this.coefficientsMatrix = coefficientsMatrix;
            this.constantsVector = constantsVector;
        }

        // Метод Гаусса для решения системы
        public Vector1<T> Solve()
        {
            // Приведение матрицы к верхнетреугольному виду
            GaussianElimination();

            // Обратный ход метода Гаусса для нахождения решения
            return BackSubstitution();
        }


        
        // Приведение матрицы к верхнетреугольному виду методом Гаусса
        private void GaussianElimination()
        {   
            int equationsCount = coefficientsMatrix.Length;
            int variablesCount = (equationsCount > 0) ? coefficientsMatrix[0].Dimension : 0;

            int minDim = Math.Min(equationsCount, variablesCount);

            for (int i = 0; i < minDim; i++)
            {
                // Поиск максимального элемента в столбце
                int maxRowIndex = i;
                T maxElement = coefficientsMatrix[i][i];

                for (int j = i + 1; j < equationsCount; j++)
                {
                    if (coefficientsMatrix[j][i].CompareTo(maxElement) > 0)
                    {
                        maxElement = coefficientsMatrix[j][i];
                        maxRowIndex = j;
                    }
                }

                // Перестановка строк
                SwapRows(i, maxRowIndex);

                // Приведение к единичному элементу
                T pivotElement = coefficientsMatrix[i][i];
                if (pivotElement != T.Parse("0"))
                {
                    for (int j = i; j < variablesCount; j++)
                    {
                        coefficientsMatrix[i][j] = coefficientsMatrix[i][j] / pivotElement;
                    }
                    constantsVector[i] = constantsVector[i] / pivotElement;
                }

                // Обнуление элементов ниже текущего
                for (int j = i + 1; j < equationsCount; j++)
                {
                    T factor = coefficientsMatrix[j][i];
                    for (int k = i; k < variablesCount; k++)
                    {
                        coefficientsMatrix[j][k] = coefficientsMatrix[j][k] - factor * coefficientsMatrix[i][k];
                    }
                    constantsVector[j] = constantsVector[j] - factor * constantsVector[i];
                }
            }
        }

        // Обратный ход метода Гаусса для нахождения решения
        private Vector1<T> BackSubstitution()
        {
            int equationsCount = coefficientsMatrix.Length;
            int variablesCount = (equationsCount > 0) ? coefficientsMatrix[0].Dimension : 0;

            Vector1<T> solution = new Vector1<T>(Enumerable.Repeat(default(T), variablesCount).ToArray());

            for (int i = equationsCount - 1; i >= 0; i--)
            {
                solution[i] = constantsVector[i];
                for (int j = i + 1; j < variablesCount; j++)
                {
                    solution[i] = solution[i] - coefficientsMatrix[i][j] * solution[j];
                }
            }

            return solution;
        }

        // Обмен местами двух строк в матрице и векторе
        private void SwapRows(int row1, int row2)
        {
            Vector1<T> temp = coefficientsMatrix[row1];
            coefficientsMatrix[row1] = coefficientsMatrix[row2];
            coefficientsMatrix[row2] = temp;

            T tempConstant = constantsVector[row1];
            constantsVector[row1] = constantsVector[row2];
            constantsVector[row2] = tempConstant;
        }


        // Вычисление ранга матрицы коэффициентов
        private int CalculateRank()
        {
            int rank = 0;
            int equationsCount = coefficientsMatrix.Length;
            int variablesCount = (equationsCount > 0) ? coefficientsMatrix[0].Dimension : 0;

            for (int i = 0; i < equationsCount; i++)
            {
                bool allZeroes = true;
                for (int j = 0; j < variablesCount; j++)
                {
                    if (coefficientsMatrix[i][j] != T.Parse("0"))
                    {
                        allZeroes = false;
                        break;
                    }
                }

                if (!allZeroes)
                {
                    rank++;
                }
            }
            return rank;
        }
        // Метод для проверки существования и единственности решения
        public bool HasUniqueSolution()
        {
            int equationsCount = coefficientsMatrix.Length;
            int variablesCount = (equationsCount > 0) ? coefficientsMatrix[0].Dimension : 0;

            // Проверка существования решения
            if (equationsCount < variablesCount)
            {
                return false; // Матрица коэффициентов недоопределена
            }

            // Проверка единственности решения
            return CalculateRank() == variablesCount;
        }
    }
}
