using System;

class GreatestNumber
{
    public static void FindGreatestOfThree()
    {
        Console.Write("Enter first no: ");
        int a = int.Parse(Console.ReadLine());

        Console.Write("Enter second no: ");
        int b = int.Parse(Console.ReadLine());

        Console.Write("Enter third no: ");
        int c = int.Parse(Console.ReadLine());

        int greatest;

        if (a >= b && a >= c)
            greatest = a;
        else if (b >= a && b >= c)
            greatest = b;
        else
            greatest = c;

        Console.WriteLine("Greatest no. is: " + greatest);
    }
}
