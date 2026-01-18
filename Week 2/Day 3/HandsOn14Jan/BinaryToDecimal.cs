// using System;

// class BinaryToDecimal
// {
//     public static int ConvertBinary(int input1)
//     {
//         // if (input1 > 11111)
//             return -2;

//         int decimalValue = 0;
//         int baseValue = 1;

//         while (input1 > 0)
//         {
//             int digit = input1 % 10;

//             if (digit != 0 && digit != 1)
//                 return -1;

//             decimalValue += digit * baseValue;
//             baseValue *= 2;
//             input1 /= 10;
//         }

//         return decimalValue;
//     }
// }
