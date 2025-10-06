using System;

class MatrixCalculator
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("=== Матричный калькулятор ===");
            Console.WriteLine("1. Создать и заполнить матрицы");
            Console.WriteLine("2. Сложить матрицы");
            Console.WriteLine("3. Умножить матрицы");
            Console.WriteLine("4. Найти детерминант матрицы");
            Console.WriteLine("5. Найти обратную матрицу");
            Console.WriteLine("6. Транспонировать матрицу");
            Console.WriteLine("7. Решить систему уравнений");
            Console.WriteLine("8. Выход");
            Console.Write("Выберите операцию: ");

            int choice = int.Parse(Console.ReadLine());
            
            switch (choice)
            {
                case 1:
                    CreateMatrices();
                    break;
                case 2:
                    AddMatrices();
                    break;
                case 3:
                    MultiplyMatrices();
                    break;
                case 4:
                    Determin();
                    break;
                case 5:
                    FindInverse();
                    break;
                case 6:
                    TransposeMatrix();
                    break;
                case 7:
                    SolveEquations();
                    break;
                case 8:
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }
    }

    static double[,] matrixA, matrixB;

    static void CreateMatrices()
    {
        Console.Write("Введите количество строк первой матрицы: ");
        int n = int.Parse(Console.ReadLine());
        Console.Write("Введите количество столбцов первой матрицы: ");
        int m = int.Parse(Console.ReadLine());
        Console.Write("Введите количество строк второй матрицы: ");
        int z = int.Parse(Console.ReadLine());
        Console.Write("Введите количество столбцов второй матрицы: ");
        int x = int.Parse(Console.ReadLine());

        matrixA = new double[n, m];
        matrixB = new double[z, x];

        Console.WriteLine("Выберите способ заполнения:");
        Console.WriteLine("1. Вручную с клавиатуры");
        Console.WriteLine("2. Автоматически случайными числами");
        int fillChoice = int.Parse(Console.ReadLine());
        if (fillChoice == 1)
        {
            Console.WriteLine("Заполнение матрицы A:");
            FillMatrixManually(matrixA);
            Console.WriteLine("Заполнение матрицы B:");
            FillMatrixManually(matrixB);
        }
        else
        {
            Console.Write("Введите минимальное значение: ");
            int min = int.Parse(Console.ReadLine());
            Console.Write("Введите максимальное значение: ");
            int max = int.Parse(Console.ReadLine());
            
            FillMatrixRandomly(matrixA, min, max);
            FillMatrixRandomly(matrixB, min, max);
        }
        Console.WriteLine("Матрица A:");
        PrintMatrix(matrixA);
        Console.WriteLine("Матрица B:");
        PrintMatrix(matrixB);
    }
    static void FillMatrixManually(double[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write($"Элемент [{i},{j}]: ");
                matrix[i, j] = double.Parse(Console.ReadLine());
            }
        }
    }
    static void FillMatrixRandomly(double[,] matrix, int min, int max)
    {
        Random rand = new Random();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                matrix[i, j] = rand.Next(min, max + 1);
            }
        }
    }
    static void PrintMatrix(double[,] matrix)
    {
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Console.Write(matrix[i, j] + "\t");
            }
            Console.WriteLine();
        }
    }
    static void AddMatrices()
    {
        if (matrixA == null || matrixB == null)
        {
            Console.WriteLine("Матрицы не созданы!");
            return;
        }
        if (matrixA.GetLength(0) != matrixB.GetLength(0) || 
            matrixA.GetLength(1) != matrixB.GetLength(1))
        {
            Console.WriteLine("Нельзя сложить матрицы разных размеров!");
            return;
        }
        double[,] result = new double[matrixA.GetLength(0), matrixA.GetLength(1)];
        for (int i = 0; i < matrixA.GetLength(0); i++)
        {
            for (int j = 0; j < matrixA.GetLength(1); j++)
            {
                result[i, j] = matrixA[i, j] + matrixB[i, j];
            }
        }
        Console.WriteLine("Результат сложения:");
        PrintMatrix(result);
    }
    static void MultiplyMatrices()
    {
        if (matrixA == null || matrixB == null)
        {
            Console.WriteLine("Матрицы не созданы!");
            return;
        }
        if (matrixA.GetLength(1) != matrixB.GetLength(0))
        {
            Console.WriteLine("Матрицы несовместимы для умножения!");
            return;
        }
        double[,] result = new double[matrixA.GetLength(0), matrixB.GetLength(1)];
        for (int i = 0; i < matrixA.GetLength(0); i++)
        {
            for (int j = 0; j < matrixB.GetLength(1); j++)
            {
                for (int k = 0; k < matrixA.GetLength(1); k++)
                {
                    result[i, j] += matrixA[i, k] * matrixB[k, j];
                }
            }
        }
        Console.WriteLine("Результат умножения:");
        PrintMatrix(result);
    }
    static void Determin()
    {
        Console.WriteLine("Для какой матрицы найти детерминант? (A/B)");
        char choice = Console.ReadLine().ToUpper()[0];
        double[,] matrix = choice == 'A' ? matrixA : matrixB;
        if (matrix.GetLength(0) != matrix.GetLength(1))
        {
            Console.WriteLine("Детерминант можно найти только для квадратной матрицы!");
            return;
        }
        double det = CalculateDeterminant(matrix);
        Console.WriteLine($"Детерминант матрицы {(choice == 'A' ? 'A' : 'B')}: {det}");
    }
    static double CalculateDeterminant(double[,] matrix)
    {
        int n = matrix.GetLength(0);
        if (n == 1) return matrix[0, 0];
        if (n == 2) return matrix[0, 0] * matrix[1, 1] - matrix[0, 1] * matrix[1, 0];
        double det = 0;
        for (int j = 0; j < n; j++)
        {
            det += matrix[0, j] * Math.Pow(-1, j) * 
                   CalculateDeterminant(GetMinor(matrix, 0, j));
        }
        return det;
    }
    static double[,] GetMinor(double[,] matrix, int row, int col)
    {
        int n = matrix.GetLength(0);
        double[,] minor = new double[n - 1, n - 1];
        int r = 0;
        for (int i = 0; i < n; i++)
        {
            if (i == row) continue;
            int c = 0;
            for (int j = 0; j < n; j++)
            {
                if (j == col) continue;
                minor[r, c] = matrix[i, j];
                c++;
            }
            r++;
        }
        return minor;
    }
    static void FindInverse()
    {
        Console.WriteLine("Для какой матрицы найти обратную? (A/B)");
        char choice = Console.ReadLine().ToUpper()[0];
        double[,] matrix = choice == 'A' ? matrixA : matrixB;
        if (matrix.GetLength(0) != matrix.GetLength(1))
        {
            Console.WriteLine("Обратная матрица существует только для квадратных матриц!");
            return;
        }
        double det = CalculateDeterminant(matrix);
        if (det == 0)
        {
            Console.WriteLine("Обратной матрицы не существует (детерминант = 0)!");
            return;
        }
        double[,] inverse = CalculateInverse(matrix, det);
        Console.WriteLine("Обратная матрица:");
        PrintMatrix(inverse);
    }
    static double[,] CalculateInverse(double[,] matrix, double det)
    {
        int n = matrix.GetLength(0);
        double[,] inverse = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                double[,] minor = GetMinor(matrix, j, i);
                inverse[i, j] = Math.Pow(-1, i + j) * CalculateDeterminant(minor) / det;
            }
        }
        return inverse;
    }
    static void TransposeMatrix()
    {
        Console.WriteLine("Какую матрицу транспонировать? (A/B)");
        char choice = Console.ReadLine().ToUpper()[0];
        double[,] matrix = choice == 'A' ? matrixA : matrixB;
        double[,] transposed = new double[matrix.GetLength(1), matrix.GetLength(0)];
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                transposed[j, i] = matrix[i, j];
            }
        }
        Console.WriteLine("Транспонированная матрица:");
        PrintMatrix(transposed);
    }
    static void SolveEquations()
    {
        Console.WriteLine("Для решения системы используем матрицу A как коэффициенты и B как свободные члены");
        if (matrixA.GetLength(0) != matrixA.GetLength(1))
        {
            Console.WriteLine("Система должна иметь квадратную матрицу коэффициентов!");
            return;
        }
        if (matrixA.GetLength(0) != matrixB.GetLength(0))
        {
            Console.WriteLine("Количество уравнений и свободных членов не совпадает!");
            return;
        }
        int n = matrixA.GetLength(0);
        double[,] extended = new double[n, n + 1];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                extended[i, j] = matrixA[i, j];
            }
            extended[i, n] = matrixB[i, 0];
        }
        // Метод Гаусса
        for (int i = 0; i < n; i++)
        {
            int maxRow = i;
            for (int k = i + 1; k < n; k++)
            {
                if (Math.Abs(extended[k, i]) > Math.Abs(extended[maxRow, i]))
                    maxRow = k;
            }
            for (int k = i; k < n + 1; k++)
            {
                double temp = extended[i, k];
                extended[i, k] = extended[maxRow, k];
                extended[maxRow, k] = temp;
            }
            if (Math.Abs(extended[i, i]) < 1e-10)
            {
                Console.WriteLine("Система не имеет единственного решения!");
                return;
            }
            for (int k = i + 1; k < n; k++)
            {
                double factor = extended[k, i] / extended[i, i];
                for (int j = i; j < n + 1; j++)
                {
                    extended[k, j] -= factor * extended[i, j];
                }
            }
        }
        // Обратный ход
        double[] solution = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            solution[i] = extended[i, n];
            for (int j = i + 1; j < n; j++)
            {
                solution[i] -= extended[i, j] * solution[j];
            }
            solution[i] /= extended[i, i];
        }
        Console.WriteLine("Решение системы:");
        for (int i = 0; i < n; i++)
        {
            Console.WriteLine($"x{i + 1} = {solution[i]:F2}");
        }
    }
}