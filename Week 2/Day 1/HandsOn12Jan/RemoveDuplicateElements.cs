// using System;

// class RemoveDuplicateElements
// {
//     static void Main()
//     {
//         int[] arr = { 10, 20, 10, 30, 20 };
//         int size = arr.Length;

//         int[] result = new int[size];
//         int index = 0;

//         for (int i = 0; i < size; i++)
//         {
//             bool duplicate = false;
//             for (int j = 0; j < index; j++)
//             {
//                 if (arr[i] == result[j])
//                 {
//                     duplicate = true;
//                     break;
//                 }
//             }
//             if (!duplicate)
//                 result[index++] = arr[i];
//         }

//         Console.Write("Output: ");
//         for (int i = 0; i < index; i++)
//             Console.Write(result[i] + " ");
//     }
// }
