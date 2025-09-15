double x = 0;
double y = 0;
double result = 0;
double M = 0;

while (true)
{
    Console.Write("Возможные операции (+ - * / % 1/x x^2 sqrt M+ M-): ");
    string? operation = Console.ReadLine();

    switch (operation)
    {
        case "+":
            inputX();
            inputY();
            result = x + y;
            Console.WriteLine("{0} + {1} = {2}", [x, y, result]);
            break;
        case "-":
            inputX();
            inputY();
            result = x - y;
            Console.WriteLine("{0} - {1} = {2}", [x, y, result]);
            break;
        case "*":
            inputX();
            inputY();
            result = x * y;
            Console.WriteLine("{0} * {1} = {2}", [x, y, result]);
            break;
        case "/":
            inputX();
            inputY();
            if (y == 0) Console.WriteLine("Нельзя делить на 0");
            else
            {
                result = x / y;
                Console.WriteLine("{0} / {1} = {2}", [x, y, result]);
            }
            break;
        case "1/x":
            inputX();
            if (x == 0) Console.WriteLine("Нельзя делить на 0");
            else
            {
                result = 1 / x;
                Console.WriteLine("1/{0} = {1}", [x, result]);
            }
            break;
        case "x^2":
            inputX();
            result = Math.Pow(x, 2);
            Console.WriteLine("{0}^2 = {1}", [x, result]);
            break;
        case "%":
            inputX();
            inputY();
            if (y == 0) Console.WriteLine("Нельзя делить на 0");
            else
            {
                result = x * (y / 100);
                Console.WriteLine("{0} % {1} = {2}", [x, y, result]);
            }
            break;
        case "sqrt":
            inputX();
            if (y == 0) Console.WriteLine("Нельзя извлечь корень из отрицательного числа");
            else
            {
                result = Math.Sqrt(x);
                Console.WriteLine("sqrt({0}) = {1}", [x, result]);
            }
            break;
        case "M+":
            inputX();
            M += x;
            break;
        case "M-":
            inputX();
            M -= x;
            break;

    }
}

void inputX()
{
    Console.Write("x = ");
    string? s = Console.ReadLine();
    if (s.Equals("MR"))
    {
        x = M;
    }
    else
    {
        x = Convert.ToInt32(s);
    }
}

void inputY()
{
    Console.Write("y = ");
    string? s = Console.ReadLine();
    if (s.Equals("MR"))
    {
        y = M;
    }
    else
    {
        y = Convert.ToInt32(s);
    }
}