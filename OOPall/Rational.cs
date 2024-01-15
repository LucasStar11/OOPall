using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OOPall
{
    public class Rational : INumberField<Rational>
    {
        public int Numerator { get; private set; }
        public uint Denominator { get; private set; }

        public Rational(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new Exception("Знаменатель не может быть равен нулю!");
            if (denominator < 0)
            {
                numerator *= -1;
                denominator *= -1;
            }

            uint gcd = GCD((uint)Math.Abs(numerator), (uint)denominator);

            this.Numerator = numerator / (int)gcd;
            this.Denominator = (uint)(denominator / gcd);
        }
        public Rational(int numerator)
        {
            this.Numerator = numerator;
            this.Denominator = 1;
        }
        public Rational()
        {
            this.Numerator = 0;
            this.Denominator = 1;
        }

        
        public static uint GCD(uint n, uint d)
        {
            
                while (n != 0 && d != 0)
                {
                    if (n > d)
                        n %= d;
                    else
                        d %= n;
                }
                return n + d;
            
        }

        public static Rational GenerateRandom(int left, int right)
        {
            int tmpleft = left;
            left = left > right ? right : left;
            right = tmpleft < right ? right : tmpleft;

            Random rnd = new Random();
            int distanse = right - left;
            int powerOf10 = (int)Math.Log10(distanse) + 1;
            int determinator = rnd.Next(1, (int)Math.Pow(10, powerOf10));
            int numerator = rnd.Next(left * determinator, right * determinator);
            return new Rational(numerator, determinator);
        }

        public static Rational Parse(string input)
        {
            var r = new Regex(@"(^-?[0-9]+/{1}-?[0-9]+$)|(^-?[0-9]+:{1}-?[0-9]+$)|(^-?[0-9]+$)");
            //@"(^-?[0-9]+/{1}-?[0-9]+$)|(^-?[0-9]+:{1}-?[0-9]+$)|(^-?[0-9]+$)"
            // 10/10 or 10:10 or 10
            input.Replace(" ", "");
            var match = r.Match(input);
            if (match.Success)
            {
                if (match.Groups[1].Success)
                {
                    string[] subStrings = match.Groups[1].Value.Split('/');
                    return new Rational(int.Parse(subStrings[0]), int.Parse(subStrings[1]));
                }
                else if (match.Groups[2].Success)
                {
                    string[] subStrings = match.Groups[2].Value.Split(':');
                    return new Rational(int.Parse(subStrings[0]), int.Parse(subStrings[1]));
                }
                else if (match.Groups[3].Success)
                {
                    return new Rational(int.Parse(match.Groups[3].Value));
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

        public static Rational Scalar(Rational a, Rational b)
        {
            return (a * b);
        }

        public static Rational operator +(Rational r1, Rational r2)
        {
            return new Rational
                (
                    (int)(r1.Numerator * r2.Denominator + r2.Numerator * r1.Denominator),
                    (int)(r1.Denominator * r2.Denominator)
                );
        }
        public static Rational operator *(Rational r1, Rational r2)
        {
            return new Rational
                (
                     r1.Numerator * r2.Numerator,
                     (int)(r1.Denominator * r2.Denominator)
                );
        }
        public static Rational operator *(int number, Rational r1)
        {
            return new Rational
                (
                     r1.Numerator * number,
                     (int)r1.Denominator
                );
        }
        public static Rational operator *(Rational r1, int number)
        {
            return new Rational
                (
                     r1.Numerator * number, 
                     (int)r1.Denominator
                );
        }
        public static Rational operator -(Rational r1, Rational r2)
        {
            return r1 + (-1 * r2);
        }

        public static Rational operator -(Rational r1, int number)
        {
            return r1 + (-1 * new Rational(number));
        }
        public static Rational operator -(Rational r1)
        {
            return -1 * r1 ;
        }

        public static Rational operator /(Rational r1, Rational r2)
        {
            return  r1 * r2.Reverse();
        }

        public static bool operator >(Rational r1, Rational r2)
        {
            return (r1 - r2).Numerator > 0;
        }
        public static bool operator <(Rational r1, Rational r2)
        {
            return (r1 - r2).Numerator < 0;
        }
        public static bool operator ==(Rational r1, Rational r2)
        {
            return (r1 - r2).Numerator == 0;
        }
        public static bool operator <=(Rational r1, Rational r2)
        {
            return !(r1 > r2);
        }
        public static bool operator >=(Rational r1, Rational r2)
        {
            return !(r1 < r2);
        }
        public static bool operator !=(Rational r1, Rational r2)
        {
            return !(r1 == r2);
        }

        public Rational Reverse()
        {
            return new Rational((int)this.Denominator, this.Numerator);
        }
        public override string ToString() =>
            Denominator == 1 ? $"{this.Numerator}" : $"{this.Numerator}/{this.Denominator}";

        public Rational Abs()
        {
            return new Rational(int.Abs(Numerator), (int)Denominator);
        }

        public int CompareTo(Rational other)
        {
            return (int)(Numerator * other.Denominator - other.Numerator * Denominator);
        }
    }
}
