using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace My
{
    internal class Matrix
    {
        public int[,] matrix;

        public int count;
        public delegate bool Function(int row, int column);
        
        public Matrix(int count = 10) => this.resetMatrix(count);

        public Matrix newMatrix()
        {
            for (int i = 0; i < this.count; ++i)
                for (int j = 0; j < this.count; ++j)
                    matrix[i, j] = new Random().Next(0, 100);
            return this;
        }

        public Matrix resetMatrix(int count = 10)
        {
            this.matrix = new int[count, count];
            this.count = count;
            for (int i = 0; i < count; ++i)
                for (int j = 0; j < count; ++j)
                    matrix[i, j] = new Random().Next(0, 100);
            return this;
        }
        public int getMatrix(int i, int j) => (i >= 0) ? ((j >= 0) ? matrix[i, j] : -1) : -1;

        private int getDataFromMatrix(Function f, string text)
        {
            Console.WriteLine($"Вычисляется {text}. Делегат f ссылается на метод {f.GetMethodInfo().Name}");
            int summa = 0;
            for (int row = 0; row <= this.count; ++row)
                for (int column = 0; column <= this.count; ++column)
                    if (f(row, column))
                        summa += this.matrix[row, column];
            return summa;
        }

        public bool sideD(int row, int column)      => (column == this.count - row - 1 && column != this.count && row != this.count);
        public bool mainD(int row, int column)      => (row == column && column != this.count && row != this.count);
        public bool topT(int row, int column)       => (row < (this.count / 2 + 1) && column >= row && column <= (this.count - row - 1));
        public bool bottomT(int row, int column)    => (row >= this.count / 2 && row < this.count && column >= this.count - row - 1 && column <= row);
        public bool leftT(int row, int column)      => (column < (this.count / 2 + 1) && row >= column && row <= (this.count - column - 1));
        public bool rightT(int row, int column)     => (column < this.count && column >= this.count / 2 && row >= (this.count - column - 1) && row <= column);

        public int getSideDiagonal()   => this.getDataFromMatrix(this.sideD, "побочная диагональ");
        public int getMainDiagonal()   => this.getDataFromMatrix(this.mainD, "главная диагональ");

        public int getTopTriangle()    => this.getDataFromMatrix(this.topT, "верхний треугольник");
        public int getBottomTriangle() => this.getDataFromMatrix(this.bottomT, "нижний треугольник");
        public int getLeftTriangle()   => this.getDataFromMatrix(this.leftT, "левый треугольник");
        public int getRightTriangle()  => this.getDataFromMatrix(this.rightT, "правый треугольник");
    }
}

namespace Задание__5
{
    internal class Program
    {        
        public static void Main(string[] args)
        {
            Console.WriteLine("1. Создание объекта matrix на основе класса My.Matrix и установка значений этой матрицы 5 на 5");
            My.Matrix matrix = new My.Matrix(5);
            for (int row = 0; row < matrix.count; ++row)
            {
                for (int column = 0; column < matrix.count; ++column)
                    Console.Write($"{matrix.getMatrix(row, column)}\t");
                Console.WriteLine();
            }
            Console.WriteLine("2. Вызов методов класса My.Matrix, которые используют метод My.Matrix.getDataFromMatrix, аргументом которой является делегат");
            Console.WriteLine("private int getDataFromMatrix(Function f, string text)\n{\n\tint summa = 0;\n\tfor (int row = 0; row <= this.count; ++row)\n\t\tfor (int column = 0; column <= this.count; ++column)\n\t\t\tif (f(row, column))\n\t\t\t\tsumma += this.matrix[row, column];\n\treturn summa;\n}");
            Console.WriteLine($"Сумма элементов главной диагонали: {matrix.getMainDiagonal()}");
            Console.WriteLine($"Сумма элементов побочной диагонали: {matrix.getSideDiagonal()}");
            Console.WriteLine($"Сумма элементов верхнего треугольника: {matrix.getTopTriangle()}");
            Console.WriteLine($"Сумма элементов нижнего треугольника: {matrix.getBottomTriangle()}");
            Console.WriteLine($"Сумма элементов левого треугольника: {matrix.getLeftTriangle()}");
            Console.WriteLine($"Сумма элементов правого треугольника: {matrix.getRightTriangle()}");
        }
    }
}
