using System;
using System.Collections.Generic;

class HandsOnProgram
{
    public static int CountMultiplesOf3(int[] input1, int input2)
    {
        if (input2 < 0) return -2;

        int count = 0;
        for (int i = 0; i < input2; i++)
        {
            if (input1[i] < 0) return -1;
            if (input1[i] % 3 == 0) count++;
        }
        return count;
    }

    public static int ProductOfDigits(int input1)
    {
        if (input1 < 0) return -1;
        if (input1 % 3 == 0 || input1 % 5 == 0) return -2;

        int product = 1;
        while (input1 > 0)
        {
            product *= input1 % 10;
            input1 /= 10;
        }

        return (product % 3 == 0 || product % 5 == 0) ? 1 : 0;
    }

    public static int SumOfPrimes(int[] input1, int input2)
    {
        if (input2 < 0) return -2;

        int sum = 0;
        bool foundPrime = false;

        for (int i = 0; i < input2; i++)
        {
            if (input1[i] < 0) return -1;
            if (IsPrime(input1[i]))
            {
                sum += input1[i];
                foundPrime = true;
            }
        }

        return foundPrime ? sum : -3;
    }

    static bool IsPrime(int num)
    {
        if (num <= 1) return false;
        for (int i = 2; i <= Math.Sqrt(num); i++)
            if (num % i == 0) return false;
        return true;
    }

    public static int[] ReverseArray(int[] input1, int input2)
    {
        int[] output = new int[input2];

        if (input2 < 0)
        {
            output[0] = -2;
            return output;
        }

        if (input2 % 2 == 0)
        {
            output[0] = -3;
            return output;
        }

        for (int i = 0; i < input2; i++)
        {
            if (input1[i] < 0)
            {
                output[0] = -1;
                return output;
            }
        }

        int mid = input2 / 2;
        for (int i = 0; i < input2; i++)
            output[i] = input1[input2 - 1 - i];

        return output;
    }

    public static int[] AddTwoArrays(int[] input1, int[] input2, int input3)
    {
        int[] output = new int[input3];

        if (input3 < 0)
        {
            output[0] = -2;
            return output;
        }

        for (int i = 0; i < input3; i++)
        {
            if (input1[i] < 0 || input2[i] < 0)
            {
                output[0] = -1;
                return output;
            }
            output[i] = input1[i] + input2[i];
        }
        return output;
    }

    public static int[] RemoveDuplicates(int[] input1, int input2)
    {
        if (input2 < 0) return new int[] { -2 };

        HashSet<int> set = new HashSet<int>();
        foreach (int val in input1)
        {
            if (val < 0) return new int[] { -1 };
            set.Add(val);
        }

        int[] output = new int[set.Count];
        set.CopyTo(output);
        return output;
    }

    public static int FahrenheitToCelsius(int f)
    {
        if (f < 0) return -1;
        return (f - 32) * 5 / 9;
    }

    public static int IsPerfectNumber(int n)
    {
        if (n < 0) return -2;

        int sum = 0;
        for (int i = 1; i <= n / 2; i++)
            if (n % i == 0) sum += i;

        return (sum == n) ? 1 : -1;
    }

    public static int SumOfCubeOfPrimes(int n)
    {
        if (n < 0 || n > 7) return -1;

        int sum = 0;
        for (int i = 2; i <= n; i++)
            if (IsPrime(i))
                sum += i * i * i;

        return sum;
    }

    public static int CountMultiplesOf3Array(int[] arr)
    {
        int count = 0;
        foreach (int val in arr)
        {
            if (val < 0) return -1;
            if (val % 3 == 0) count++;
        }
        return count;
    }

    public static void Main()
    {
        Console.WriteLine("Multiples of Three are : " + CountMultiplesOf3(new int[] { 1, 2, 3, 4, 5, 6 }, 6)); 
        Console.WriteLine("Product of digits are : " + ProductOfDigits(56)); 
        Console.WriteLine("Sum of primes: " + SumOfPrimes(new int[] { 1, 2, 3, 4, 5 }, 5)); 
        Console.WriteLine("Celcius: " + FahrenheitToCelsius(98)); 
        Console.WriteLine("Perfect number or not: " + IsPerfectNumber(6)); 
    }
}
