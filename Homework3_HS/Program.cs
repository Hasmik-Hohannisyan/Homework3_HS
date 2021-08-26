using System;

namespace Homework3_HS
{
    class Program
    {
        struct Matrix
        {
            internal int Row;
            internal int Column;

            internal void Fill(double[,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        try
                        {
                            Console.Write($"arr[{i} {j}] = ");
                            arr[i, j] = double.Parse(Console.ReadLine());
                        }
                        catch (FormatException FEx)
                        {
                            Console.WriteLine(FEx.Message);
                            j--;
                        }
                    }
                }
                Console.WriteLine();
            }

            internal void Print(double[,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        Console.Write($"{arr[i, j]} ");
                    }
                    Console.WriteLine();
                }
            }

            internal bool isOrthogonal()
            {
                if (Row == Column)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

        }

        static void Main(string[] args)
        {
            Matrix matrix = new Matrix();
            double[,] arr = new double[matrix.Row, matrix.Column];
            CreatingAndFillMatrix(ref matrix, ref arr);

            Addition(matrix, arr);
            ScalarMultiplication(matrix, arr);
            OrthogonalOrNot(matrix, arr);
            MinMaxElements(matrix, arr);
            Multiplication(matrix, arr);
            Inverse(matrix, arr);

            Console.ReadKey();
        }

        static int Column(ref Matrix matrix)
        {
            bool isFalse;
            do
            {
                isFalse = false;
                try
                {
                    Console.Write("Enter Column: ");
                    matrix.Column = int.Parse(Console.ReadLine());

                }
                catch (FormatException FEx)
                {
                    isFalse = true;
                    Console.WriteLine(FEx.Message);
                }
            }
            while (matrix.Column <= 0 || isFalse);
            return matrix.Column;
        }

        static int Row(ref Matrix matrix)
        {
            bool isFalse;

            do
            {
                isFalse = false;
                try
                {
                    Console.Write("Enter Row: ");
                    matrix.Row = int.Parse(Console.ReadLine());
                }
                catch (FormatException FEx)
                {
                    isFalse = true;
                    Console.WriteLine(FEx.Message);
                }
            }
            while (matrix.Row <= 0 || isFalse);
            return matrix.Row;
        }

        static void CreatingAndFillMatrix(ref Matrix matrix, ref double[,] arr)
        {
            arr = new double[Row(ref matrix), Column(ref matrix)];
            matrix.Fill(arr);

        }



        static void Addition(Matrix matrix, double[,] arr1)
        {
            Console.WriteLine("Addition of matrices․");
            double[,] arr2 = new double[matrix.Row, matrix.Column];
            CreatingAndFillMatrix(ref matrix, ref arr2);

            double[,] addArr1Arr2 = new double[arr1.GetLength(0), arr1.GetLength(1)];

            if (arr1.GetLength(0) == arr2.GetLength(0) && arr1.GetLength(1) == arr2.GetLength(1))
            {
                for (int i = 0; i < arr1.GetLength(0); i++)
                {
                    for (int j = 0; j < arr1.GetLength(1); j++)
                    {
                        addArr1Arr2[i, j] = arr1[i, j] + arr2[i, j];
                    }
                }
                matrix.Print(addArr1Arr2);
            }
            else
            {
                Console.WriteLine("Index Out Of Range Exception");
            }
            Console.WriteLine();
        }

        static void ScalarMultiplication(Matrix matrix, double[,] arr)
        {
            Console.WriteLine("Scalar Multiplication.");

            double digit;
            bool isNotDigit = true;

            while (isNotDigit)
            {
                isNotDigit = false;
                Console.Write("Enter number: ");
                if (double.TryParse(Console.ReadLine(), out digit))
                {
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            arr[i, j] *= digit;
                        }
                    }
                    matrix.Print(arr);
                }
                else
                {
                    isNotDigit = true;
                    Console.WriteLine("input number was not in correct format.");
                }
            }

            Console.WriteLine();
        }

        static void Multiplication(Matrix matrix, double[,] arr1)
        {
            Console.WriteLine("Multiplication matrices ");

            double[,] arr2 = new double[matrix.Row, matrix.Column];
            CreatingAndFillMatrix(ref matrix, ref arr2);

            double[,] multArr1Arr2 = new double[arr1.GetLength(0), arr2.GetLength(1)];

            if (arr1.GetLength(1) == arr2.GetLength(0))
            {
                for (int i = 0; i < arr1.GetLength(0); i++)
                {
                    for (int k = 0; k < arr2.GetLength(1); k++)
                    {
                        for (int j = 0; j < arr1.GetLength(1); j++)
                        {
                            multArr1Arr2[i, k] += arr1[i, j] * arr2[j, k];
                        }
                    }
                }
                matrix.Print(multArr1Arr2);
            }
            else
            {
                Console.WriteLine("enter correct matrix ");
            }

            Console.WriteLine();
        }



        static void Transpose(double[,] arr, double[,] fac, int length, Matrix matrix)
        {
            double[,] arrTranspose = new double[length, length];
            double[,] inverse = new double[length, length];
            double det;

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    arrTranspose[j, i] = fac[i, j];
                }
            }

            det = Determinant(arr, length);

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    inverse[i, j] = arrTranspose[i, j] / det;
                }
            }
            matrix.Print(inverse);
        }

        static double Determinant(double[,] arr, int length)
        {
            double[,] b = new double[length, length];
            double det = 0, s = 1;
            int m, n;
            if (length == 1)
            {
                return arr[0, 0];
            }
            else
            {
                det = 0;
                for (int k = 0; k < length; k++)
                {
                    m = 0;
                    n = 0;
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            b[i, j] = 0;
                            if (i != 0 && j != k)
                            {
                                b[m, n] = arr[i, j];
                                if (n < (length - 2))
                                {
                                    n++;
                                }
                                else
                                {
                                    n = 0;
                                    m = 0;
                                }
                            }
                        }
                    }

                    det = det + s * (arr[0, k] * Determinant(b, (length - 1)));
                    s = -1 * s;
                }
            }
            return det;
        }

        static void ModuleArr(double[,] arr, int length, Matrix matrix)
        {
            double[,] b = new double[length, length];
            double[,] fac = new double[length, length];
            int m, n;
            for (int p = 0; p < length; p++)
            {
                for (int q = 0; q < length; q++)
                {
                    m = 0;
                    n = 0;
                    for (int i = 0; i < length; i++)
                    {
                        for (int j = 0; j < length; j++)
                        {
                            if (i != p && j != q)
                            {
                                b[m, n] = arr[i, j];
                                if (n < (length - 2))
                                {
                                    n++;
                                }
                                else
                                {
                                    n = 0;
                                    m++;
                                }
                            }
                        }

                        fac[p, q] = Math.Pow(-1, p + q) * Determinant(b, length - 1);

                    }

                }
            }
            Transpose(arr, fac, length, matrix);
        }

        static void Inverse(Matrix matrix, double[,] arr)
        {
            Console.WriteLine("Inverse.");
            if (arr.GetLength(0) == arr.GetLength(1))
            {
                if (arr.GetLength(0) == 2)
                {
                    int length = matrix.Row;
                    if (Determinant(arr, length) == 0)
                    {
                        Console.WriteLine("The inverse of matrix is not posible.");
                    }
                    else
                    {
                        Determinant(arr, length);

                        ModuleArr(arr, length, matrix);
                    }
                }
            }
            Console.WriteLine();
        }



        static void OrthogonalOrNot(Matrix matrix, double[,] arr)
        {
            if (matrix.isOrthogonal())
            {
                Console.WriteLine("The matrix is orthogonal.");
            }
            else
            {
                Console.WriteLine("The matrix is'n orthogonal.");
            }
            Console.WriteLine();
        }



        static double Min(double[,] arr)
        {
            double min = double.MaxValue;
            double[] minRows = new double[arr.GetLength(0)];
            minRows[0] = double.MaxValue;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] < minRows[i])
                    {
                        minRows[i] = arr[i, j];
                    }
                }
            }

            for (int i = 0; i < minRows.Length; i++)
            {
                if (minRows[i] < min)
                {
                    min = minRows[i];
                }
            }

            return min;
        }

        static double Max(double[,] arr)
        {
            double max = double.MinValue;
            double[] maxRows = new double[arr.GetLength(0)];
            maxRows[0] = double.MinValue;
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    if (arr[i, j] > maxRows[i])
                    {
                        maxRows[i] = arr[i, j];
                    }
                }
            }

            for (int i = 0; i < maxRows.Length; i++)
            {
                if (maxRows[i] > max)
                {
                    max = maxRows[i];
                }
            }
            return max;
        }

        static void MinMaxElements(Matrix matrix, double[,] arr)
        {
            Console.WriteLine($"Min elemen is {Min(arr)}");
            Console.WriteLine($"Max elemen is {Max(arr)}");
        }
    }
}
