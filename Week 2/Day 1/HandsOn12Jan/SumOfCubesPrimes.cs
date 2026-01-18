// using System;

// class SumOfCubesOfPrimes
// {
//     public static void Main()
//     {
//         int n = 10;

//         if (n < 0)
//         {
//             Console.WriteLine("-1");
//             return;
//         }

//         if (n > 32676)
//         {
//             Console.WriteLine("-2");
//             return;
//         }

//         int sum = 0;

//         for (int i = 2; i <= n; i++)
//         {
//             int flag = 0;
//             for (int j = 2; j <= i / 2; j++)
//             {
//                 if (i % j == 0)
//                 {
//                     flag = 1;
//                     break;
//                 }
//             }
//             if (flag == 0)
//                 sum += i * i * i;
//         }

//         Console.WriteLine("Output: " + sum);
//     }
// }
