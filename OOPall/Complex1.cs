using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOPall
{
    public class Complex1 : INumberField<Complex1>
    {
        private double Re;
        private double Im;
        public Complex1(double re, double im)
        {
            Re = re;
            Im = im;
        }

        public Complex1(double re)
        {
            Re = re;
            Im = 0;
        }
        public Complex1()
        {
            Re = 0;
            Im = 0;
        }
        public static Complex1 GenerateRandom(int left, int right)
        {
            int tmpleft = left;
            left = left > right ? right : left;
            right = tmpleft < right ? right : tmpleft;

            Random rnd = new Random();
            double real = rnd.Next(left, right);
            double imaginary = rnd.Next(left, right);
            return new Complex1(real, imaginary);
        }

        public static Complex1 Parse(string input)
        {
            var r = new Regex(@"(^[-+]?[0-9]+$)|(^[-+]?[0-9]+[+-][0-9]*i$)|(^[-+]?[0-9]*i$)");

            // 10 or -10+-123i or -2i
            input.Replace(" ", "");
            var match = r.Match(input);

            if (match.Success)
            {
                if (match.Groups[1].Success)
                {
                    return new Complex1(double.Parse(match.Groups[1].Value));
                }
                else if (match.Groups[2].Success)
                {
                    int signIndex = match.Groups[2].Value.LastIndexOfAny(new char[] { '+', '-' });
                    //Исключение
                    if (signIndex == -1)
                    {
                        throw new FormatException("Неверный формат строки");
                    }
                    //Получение подстроки для действительной и мнимой частей
                    string rl = match.Groups[2].Value.Substring(0, signIndex);
                    string im = match.Groups[2].Value.Substring(signIndex).Replace("i", "");
                    if ( im == "+" || im == "-" || im == "")
                        im += 1;
                    return new Complex1(double.Parse(rl), double.Parse(im));
                }
                else if (match.Groups[3].Success)
                {
                    string subString = match.Groups[3].Value.Replace("i", "");
                    if (subString == "+" || subString == "-" || subString == "")
                        subString += 1;
                    return new Complex1(0, double.Parse(subString));
                }
                else
                {
                    throw new Exception("Неверынй формат ввода!");
                }
            }
            else
            {
                throw new Exception("Неверынй формат ввода!");
            }
        }

        

        public static Complex1 operator +(Complex1 a, Complex1 b)
        {
            return new Complex1(a.Re + b.Re, a.Im + b.Im);
        }
        public static Complex1 operator +(Complex1 a, double b)
        {
            return new Complex1(a.Re + b, a.Im);
        }
        public static Complex1 operator +(double a, Complex1 b)
        {
            return new Complex1(a + b.Re, b.Im);
        }

        public static Complex1 operator -(Complex1 a, Complex1 b)
        {
            return new Complex1(a.Re - b.Re, a.Im - b.Im);
        }
        public static Complex1 operator -(Complex1 a, double b)
        {
            return new Complex1(a.Re - b, a.Im);
        }
        public static Complex1 operator -(double a, Complex1 b)
        {
            return new Complex1(a - b.Re, -b.Im);
        }

        public static Complex1 operator *(Complex1 a, Complex1 b)
        {
            return new Complex1(a.Re * b.Re - a.Im * b.Im,
                a.Im * b.Re + a.Re * b.Im);
        }
        public static Complex1 operator *(Complex1 a, double d)
        {
            return new Complex1(d * a.Re, d * a.Im);
        }
        public static Complex1 operator *(double d, Complex1 a)
        {
            return new Complex1(d * a.Re, d * a.Im);
        }

        //сопряженное знаменателю выражение
        public static Complex1 Conj(Complex1 a)
        {
            return new Complex1(a.Re, -a.Im);
        }

        public static Complex1 operator /(Complex1 a, Complex1 b)
        {
            return a * Conj(b) * (1 / (Abs(b) * Abs(b)));
        }
        public static Complex1 operator /(Complex1 a, double b)
        {
            return a * (1 / b);
        }
        public static Complex1 operator /(double a, Complex1 b)
        {
            return a * Conj(b) * (1 / (Abs(b) * Abs(b)));
        }

        public static bool operator ==(Complex1 a, Complex1 b)
        {
            return a.Re == b.Re && a.Im == b.Im;
        }
        public static bool operator ==(Complex1 a, double b)
        {
            return a == new Complex1(b);
        }
        public static bool operator ==(double a, Complex1 b)
        {
            return new Complex1(a) == b;
        }

        public static bool operator !=(Complex1 a, Complex1 b)
        {
            return !(a == b);
        }
        public static bool operator !=(Complex1 a, double b)
        {
            return !(a == b);
        }
        public static bool operator !=(double a, Complex1 b)
        {
            return !(a == b);
        }

        public static double Abs(Complex1 a)
        {
            return Math.Sqrt(a.Im * a.Im + a.Re * a.Re);
        }

        public Complex1 Reverse()
        {
            return new Complex1(
                Re / (Re * Re + Im * Im),
                -Im / (Re * Re + Im * Im) );
        }

        public override string ToString()
        {
            if (Im == 0)
            {
                return String.Format("{0}", Re);
            }
            else if (Re == 0)
            {
                return String.Format("{0}i",Im);
            }
            else if (Im > 0)
            {
                return String.Format("{0}+{1}i", Re, Im);
            }
            else
            {
                return String.Format("{0}{1}i", Re, Im);
            }
        }

        public static Complex1 Scalar(Complex1 a, Complex1 b)
        {
            return a * b.Reverse();
        }

        public Complex1 Abs()
        {
            return new Complex1(double.Abs(Re),double.Abs(Im));
        }

        public int CompareTo(Complex1 other)
        {
            int realPartComparison = this.Re.CompareTo(other.Re);
            return realPartComparison != 0 ? realPartComparison : this.Im.CompareTo(other.Im);
        }
    }
}
