// See https://aka.ms/new-console-template for more information
using OOPall;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;

/*
 * ///Примеры ///*
Complex1 a = Complex1.Parse("-10");
Console.WriteLine(a);

a = Complex1.Parse("-i");
Console.WriteLine(a);
Complex1 d = Complex1.Parse("4+3i");

Console.WriteLine(Complex1.Scalar(a,d));

Rational b = Rational.Parse("23/3");
Rational c = Rational.Parse("27:8");
Console.WriteLine(Rational.Scalar(b, c));


// Пример для 5лр комплексные
Vector1<Complex1> g = Vector1<Complex1>.Parse("1+1i, 4+3i");
Console.WriteLine(g);

var coefficientsMatrix = new List<Vector1<Complex1>>();
coefficientsMatrix.Add(Vector1<Complex1>.Parse("(            1+1i, 4+3i)"));
coefficientsMatrix.Add(Vector1<Complex1>.Parse("(4+3i, 4+2i)"));
coefficientsMatrix.ToString();

var constants = Vector1<Complex1>.Parse("(3+2i,        4+1i)");
var linearSystem = new LinearEquationSystem<Complex1>(coefficientsMatrix.ToArray(), constants);
var solution = linearSystem.Solve();
Console.WriteLine($"Solution (Complex1, size 2x2): {solution}");

*/


