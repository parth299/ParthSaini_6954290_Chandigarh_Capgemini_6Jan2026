// using System;

// class AverageDivisibleByFive
// {
//     public static int FindAverage(int input1)
//     {
//         if (input1 < 0)
//             return -1;

//         int sum = 0, count = 0;

//         for (int i = 1; i <= input1; i++)
//         {
//             if (i % 5 == 0)
//             {
//                 sum += i;
//                 count++;
//             }
//         }

//         return count == 0 ? 0 : sum / count;
//     }

//     static void Main()
//     {
//         Console.WriteLine(FindAverage(25));
//     }
// }
