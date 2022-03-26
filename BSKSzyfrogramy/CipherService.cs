using System;
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
            throw new NotImplementedException();  //test

        }

        public static string CipherMatrixTransp(string ciphertext)
        {
            int d = 5;
            List<int> key = new List<int>() { 3, 4, 1, 5, 2 };
            string result = "";

            for (int i = 0; i < Math.Ceiling((decimal)ciphertext.Length / key.Count); i++)
            {
                int row = i * key.Count;
                for (int j=0; j< key.Count; j++)  
                {
                    int keyValue = key[j];

                    if((row + keyValue - 1) < ciphertext.Length)
                        result += ciphertext[row + keyValue - 1];
                }
            }
            return result;
        }

        public static string DecipherMatrixTransp(string ciphertext)
        {
            int d = 5;
            List<int> key = new List<int>() { 3, 4, 1, 5, 2 };
            //string result = "";
            char[] result = new char[ciphertext.Length];


            for (int i = 0; i < Math.Ceiling((decimal)ciphertext.Length / key.Count); i++)
            {
                int row = i * key.Count;
                for (int j = 0; j < key.Count; j++)
                {
                    int keyValue = key[j];

                    if ((row + keyValue - 1) < ciphertext.Length)
                        result[row + keyValue - 1] = ciphertext[row + j];
                }
            }
            return new string(result);
        }

        public static string MatrixTransp_B(string ciphertext)
        {
            throw new NotImplementedException();
        }
    }
}
