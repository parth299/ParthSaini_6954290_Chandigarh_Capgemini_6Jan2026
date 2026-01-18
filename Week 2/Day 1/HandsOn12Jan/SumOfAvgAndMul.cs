// using System;

// class SumAndAverageOfMultiplesOfFive
// {
//     static void Main()
//     {
//         int[] arr = { 5, 10, 15, 20 };
//         int size = arr.Length;

//         if (size < 0)
//         {
//             Console.WriteLine("-2");
//             return;
//         }

//         int sum = 0, count = 0;

//         for (int i = 0; i < size; i++)
//         {
//             if (arr[i] < 0)
//             {
//                 Console.WriteLine("-1");
//                 return;
//             }
//             if (arr[i] % 5 == 0)
//             {
//                 sum += arr[i];
//                 count++;
//             }
//         }

//         double avg = (double)sum / count;

//         Console.WriteLine("Sum = " + sum);
//         Console.WriteLine("Average = " + avg);
//     }
// }
