// using System;

// class HalfSortArray
// {
//     static void Main()
//     {
//         int[] arr = { 8, 3, 6, 2, 9, 1 };
//         int size = arr.Length;

//         if (size < 0)
//         {
//             Console.WriteLine("-1");
//             return;
//         }

//         int mid = size / 2;

//         for (int i = 0; i < mid - 1; i++)
//         {
//             for (int j = 0; j < mid - i - 1; j++)
//             {
//                 if (arr[j] > arr[j + 1])
//                 {
//                     int t = arr[j];
//                     arr[j] = arr[j + 1];
//                     arr[j + 1] = t;
//                 }
//             }
//         }

//         for (int i = mid; i < size - 1; i++)
//         {
//             for (int j = mid; j < size - 1; j++)
//             {
//                 if (arr[j] < arr[j + 1])
//                 {
//                     int t = arr[j];
//                     arr[j] = arr[j + 1];
//                     arr[j + 1] = t;
//                 }
//             }
//         }

//         Console.Write("Output: ");
//         for (int i = 0; i < size; i++)
//             Console.Write(arr[i] + " ");
//     }
// }
