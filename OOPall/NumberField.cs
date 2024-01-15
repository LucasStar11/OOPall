using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPall
{
    public interface INumberField<T> where T : INumberField<T>
    {
        static abstract T Parse(string input);
        static abstract T operator +(T a, T b);
        static abstract T operator -(T a, T b);
        static abstract T operator *(T a, T b);
        static abstract T operator /(T a, T b);
        static abstract bool operator ==(T a, T b);
        static abstract bool operator !=(T a, T b);
        static abstract T GenerateRandom(int a, int b);
        static abstract T Scalar(T a, T b);
        T Abs();
        int CompareTo(T other);
    }
}
