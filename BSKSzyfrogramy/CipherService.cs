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
            List<int> key = new List<int>() { 3, 4, 1, 5, 2 };
            string result = "";
            double maxRowDouble = Math.Ceiling((double)ciphertext.Length / key.Count);
            int maxRow = (int)maxRowDouble;
            char[,] array = new char[key.Count, maxRow];

                
            for (int i = 0; i < key.Count; i++)
            {
                for (int j=0; j<maxRow ; j++)  
                {

                    if(i*maxRow + j < ciphertext.Length)
                        array[i,j] = ciphertext[i * maxRow + j];
                }
            }
            for (int i = 0; i < key.Count; i++)
            {
                int keyValue = key[i];
                for (int j=0; j<maxRow ; j++)  
                {
                    if(i * (maxRow - 1)  + j < ciphertext.Length)
                        result += array[keyValue - 1 ,j];
                }
            }

            return result;
        }

        public static string DecipherMatrixTransp(string ciphertext)
        {
            List<int> key = new List<int>() { 3, 4, 1, 5, 2 };
            string result = "";

            double maxRowDouble = Math.Ceiling((double)ciphertext.Length / key.Count);
            int maxRow = (int)maxRowDouble;

            char[,] array = new char[key.Count, maxRow];


            for (int i = 0; i < key.Count; i++)
            {
                for (int j = 0; j < maxRow; j++)
                {

                    if (i * maxRow + j < ciphertext.Length)
                        array[i, j] = '1';
                }
            }

            int counter = 0;

            for (int i = 0; i < key.Count; i++)
            {
                int keyValue = key[i];
                for (int j = 0; j < maxRow; j++)
                {
                    if (array[keyValue - 1, j] == '1')
                    {
                        array[keyValue - 1, j] = ciphertext[counter];
                        counter++;
                    }
                }
            }

            for (int i = 0; i < key.Count; i++)
            {
                int keyValue = key[i];
                for (int j = 0; j < maxRow; j++)
                {
                    if (i * (maxRow - 1) + j < ciphertext.Length)
                        result += array[i, j];
                }
            }

            return result;
        }

        public static string MatrixTransp_B(string ciphertext)
        {
            throw new NotImplementedException();
        }
    }
}
