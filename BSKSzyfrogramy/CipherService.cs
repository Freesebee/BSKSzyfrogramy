﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSKSzyfrogramy
{
    public static class CipherService
    {
        public static string CipherRailFence(string ciphertext, int rowCount)
        {
            char[,] array = new char[ciphertext.Length, rowCount];
            int increment = 1;
            int row = 0;

            for (int col = 0; col < ciphertext.Length; col++)
            {
                array[col, row] = ciphertext[col];

                if (row == rowCount - 1) increment = -1;
                else if (row == 0) increment = 1;
                
                row += increment;
            }

            string result = "";

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < ciphertext.Length; j++)
                {
                    char c = array[j, i];
                    if(c != '\0')
                    {
                        result += c;
                    }
                }
            }

            return result;
        }

        public static string DecipherRailFence(string cipher, int rowCount)
        {
            char[,] array = new char[cipher.Length, rowCount];

            int increment = 1;
            int row = 0;

            for (int col = 0; col < cipher.Length; col++)
            {
                array[col, row] = 'x';

                if (row == rowCount - 1) increment = -1;
                else if (row == 0) increment = 1;

                row += increment;
            }

            int cipherIndex = 0;

            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < cipher.Length; j++)
                {
                    if (array[j, i] == 'x')
                    {
                        array[j, i] = cipher[cipherIndex++];
                    }
                }
            }

            string result = "";

            for (int col = 0; col < cipher.Length; col++)
            {
                for (row = 0; row < rowCount; row++)
                {
                    char c = array[col, row];
                    if (c != '\0') result += c;
                }
            }

            return result;
        }

        public static string MatrixTransp(string ciphertext)
        {
            throw new NotImplementedException();
        }

        public static string MatrixTransp_B(string ciphertext)
        {
            throw new NotImplementedException();  //test
        }
    }
}
