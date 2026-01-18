// using System;

// class RemoveNegativeAndSort
// {
//     static void Main()
//     {
//         int[] arr = { 10, -5, 4, 8, -2 };
//         int size = arr.Length;

//         if (size < 0)
//         {
//             Console.WriteLine("-1");
//             return;
//         }

//         int[] temp = new int[size];
//         int count = 0;

//         for (int i = 0; i < size; i++)
//         {
//             if (arr[i] >= 0)
//                 temp[count++] = arr[i];
//         }

//         for (int i = 0; i < count - 1; i++)
//         {
//             for (int j = 0; j < count - i - 1; j++)
//             {
//                 if (temp[j] > temp[j + 1])
//                 {
//                     int t = temp[j];
//                     temp[j] = temp[j + 1];
//                     temp[j + 1] = t;
//                 }
//             }
//         }

//         Console.Write("Output: ");
//         for (int i = 0; i < count; i++)
//             Console.Write(temp[i] + " ");
//     }
// }
