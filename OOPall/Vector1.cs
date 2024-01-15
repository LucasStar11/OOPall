using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace OOPall
{
    public class Vector1<T> where T : INumberField<T>
    {
        private T[] components;

        public Vector1(params T[] components)
        {
            this.components = components;
        }

        public T this[int index]
        {
            get { return components[index]; }
            set { components[index] = value; }
        }

        public int Dimension => components.Length;
        //Парсинг строк
        public static Vector1<T> Parse(string input)
        {
            input = input.Trim('(', ')', '{', '}').Replace(" ", "");


            string[] parts = input.Split(',');

            T[] parsedComponents = new T[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                parsedComponents[i] = T.Parse(parts[i]);
            }

            return new Vector1<T>(parsedComponents);
        }
        //Сложение
        public static Vector1<T> operator +(Vector1<T> a, Vector1<T> b)
        {
            if (a.Dimension != b.Dimension)
                throw new ArgumentException("Векторы должны иметь одинаковую размерность!");

            T[] resultComponents = new T[a.Dimension];
            for (int i = 0; i < a.Dimension; i++)
            {
                resultComponents[i] = a[i] + b[i];
            }

            return new Vector1<T>(resultComponents);
        }
        //Вычитание
        public static Vector1<T> operator -(Vector1<T> a, Vector1<T> b)
        {
            if (a.Dimension != b.Dimension)
                throw new ArgumentException("Векторы должны иметь одинаковую размерность!");

            T[] resultComponents = new T[a.Dimension];
            for (int i = 0; i < a.Dimension; i++)
            {
                resultComponents[i] = a[i] - b[i];
            }

            return new Vector1<T>(resultComponents);
        }
        //Векторное (3D) и перекрёстное (2D) умножение (доп.)
        public static Vector1<T> operator *(Vector1<T> a, Vector1<T> b)
        {
            if (a.Dimension == 2 && b.Dimension == 2)
            {
                // Перекрёстное (2D) умножение (доп.)
                T result = (a[0] * b[1]) - (a[1] * b[0]);
                return new Vector1<T>(result);
            }
            else if (a.Dimension == 3 && b.Dimension == 3)
            {
                //Векторное (3D) умножение
                T[] resultComponents = new T[3];
                resultComponents[0] = a[1] * b[2] - a[2] * b[1];
                resultComponents[1] = a[2] * b[0] - a[0] * b[2];
                resultComponents[2] = a[0] * b[1] - a[1] * b[0];
                return new Vector1<T>(resultComponents);
            }
            else
            {
                throw new ArgumentException("Векторное/перекрёстное умножение определено только для двумерных и трёхмерных векторов");
            }
        }

        // todo() - done 
        // СКАЛЯР ДЛЯ КОМПЛЕКСНЫХ
        //Скалярное умножение
        public static T ScalarProduct(Vector1<T> a, Vector1<T> b)
        {    
            if (a.Dimension != b.Dimension )
                throw new ArgumentException("Скалярное произведение определено только для трёхмерных векторов");
            T sum = T.Scalar(a[0],b[0]);
            for(int i = 1;i < a.Dimension; i++)
            {
                sum += T.Scalar(a[i], b[i]);
            }
            return sum;

        }
        //Умножение на константу/скаляр
        public static Vector1<T> MultiplyByScalar(Vector1<T> vector, T scalar)
        {
            T[] resultComponents = new T[vector.Dimension];
            for (int i = 0; i < vector.Dimension; i++)
            {
                resultComponents[i] = vector[i] * scalar;
            }

            return new Vector1<T>(resultComponents);
        }
        //Проверка равенства
        public static bool operator ==(Vector1<T> a, Vector1<T> b)
        {
            if (ReferenceEquals(a, b)) return true; // Если оба объекта null, то они равны

            if (a is null || b is null) return false; // Если один из объектов null, а другой нет, то они не равны

            if (a.Dimension != b.Dimension)
                return false;

            for (int i = 0; i < a.Dimension; i++)
            {
                if (!a.components[i].Equals(b.components[i]))
                {
                    return false;
                }
            }

            return true;
        }
        public static bool operator !=(Vector1<T> a, Vector1<T> b)
        {
            return !(a == b);
        }
        //Преобразование вывода
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");

            for (int i = 0; i < Dimension; i++)
            {
                sb.Append(components[i]);
                if (i < Dimension - 1)
                {
                    sb.Append(", ");
                }
            }

            sb.Append("}");
            return sb.ToString();
        }
        //Генерация случайного вектора
        public static Vector1<T> GenerateRandom(int size, int a, int b)
        {
            T[] randomComponents = new T[size];

            for (int i = 0; i < size; i++)
            {
                randomComponents[i] = T.GenerateRandom(a, b);
            }

            return new Vector1<T>(randomComponents);
        }
    }
}
