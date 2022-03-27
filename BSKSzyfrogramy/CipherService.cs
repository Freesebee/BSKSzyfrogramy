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

        public static string CipherMatrixTransp(string ciphertext)
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
                        array[i, j] = ciphertext[i * maxRow + j];
                }
            }
            for (int i = 0; i < key.Count; i++)
            {
                int keyValue = key[i];
                for (int j = 0; j < maxRow; j++)
                {
                    if (i * (maxRow - 1) + j < ciphertext.Length)
                        result += array[keyValue - 1, j];
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

        public static string CipherMatrixTransp_B(string cipherText, string key)
        {
            int[] keyIndexes = new int[key.Length];
            key = key.ToLower(); //zabezpieczenie, bo iterujemy po wartościach ASCII
            int availableNumber = 1;

            for (int @char = 'a'; @char < 'z'; @char++)
            {
                for (int cellIndex = 0; cellIndex < keyIndexes.Length; cellIndex++)
                {
                    if(key[cellIndex] == @char)
                    {
                        keyIndexes[cellIndex] = availableNumber++;
                    }
                }
            }

            int rowCount = (int)Math.Ceiling((double)cipherText.Length / key.Length);
            char[,] mainMatrix = new char[rowCount, key.Length];
            int cipherTextCharIndex = 0;

            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < key.Length; col++)
                {
                    char currentChar = cipherTextCharIndex < cipherText.Length ? cipherText[cipherTextCharIndex++] : '\0';
                    mainMatrix[row, col] = currentChar != 0 ? currentChar : ' ';
                }
            }

            string result = "";

            for (int currentKeyCol = 1; currentKeyCol < availableNumber; currentKeyCol++)
            {
                int col = Array.IndexOf(keyIndexes, currentKeyCol);

                for (int row = 0; row < rowCount; row++)
                {
                    result += mainMatrix[row, col]; 
                }
            }

            return result;

        }

        public static string DecipherMatrixTransp_B(string ciphertext, string key)
        {
            throw new NotImplementedException();
        }
    }
}



        

