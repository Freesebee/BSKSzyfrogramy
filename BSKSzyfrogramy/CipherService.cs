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

            //utworzenie macierzy i wpisanie liter
            for (int col = 0; col < ciphertext.Length; col++)
            {
                array[col, row] = ciphertext[col];

                if (row == rowCount - 1) increment = -1;
                else if (row == 0) increment = 1;
                
                row += increment;
            }

            //odczytanie szyfru wiersz po wierszu
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

            //oznaczenie komórek macierzy,
            //w które należy wprowadzić litery szyfru
            for (int col = 0; col < cipher.Length; col++)
            {
                array[col, row] = 'x';

                if (row == rowCount - 1) increment = -1;
                else if (row == 0) increment = 1;

                row += increment;
            }

            //wprowadzenie liter w oznaczone komórki
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

            //odczytanie zakodowanego słowa
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

        public static string CipherMatrixTransp(string ciphertext, int[] keyArray)
        {
            List<int> key = new List<int>() {};
            key = keyArray.ToList();
            string result = "";
            double maxRowDouble = Math.Ceiling((double)ciphertext.Length / key.Count);
            int maxRow = (int)maxRowDouble;
            char[,] array = new char[key.Count, maxRow];

            //wpisanie oryginalnego tekstu do tabeli
            for (int i = 0; i < key.Count; i++)
            {
                for (int j = 0; j < maxRow; j++)
                {

                    if (i * maxRow + j < ciphertext.Length)
                        array[i, j] = ciphertext[i * maxRow + j];
                }
            }

            //skonstruowanie zaszyfrowanego tekstu
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

        public static string DecipherMatrixTransp(string ciphertext, int[] keyArray)
        {
            List<int> key = new List<int>() {};
            key = keyArray.ToList();

            string result = "";

            double maxRowDouble = Math.Ceiling((double)ciphertext.Length / key.Count);
            int maxRow = (int)maxRowDouble;

            char[,] array = new char[key.Count, maxRow];

            //oznaczenie miejsc w tabeli, ktore powinny zostac zapelnione literami
            for (int i = 0; i < key.Count; i++)
            {
                for (int j = 0; j < maxRow; j++)
                {

                    if (i * maxRow + j < ciphertext.Length)
                        array[i, j] = '1';
                }
            }

            int counter = 0;
            //wpisanie zaszyfrowanego slowa do tabeli
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

            //odczytanie oryginalnej wiadomosci
            for (int i = 0; i < key.Count; i++)
            {
                for (int j = 0; j < maxRow; j++)
                {
                    if (i * (maxRow - 1) + j < ciphertext.Length)
                        result += array[i, j];
                }
            }

            return result;
        }

        public static string CipherMatrixTransp(string cipherText, string key)
        {
            int[] keyIndexes = new int[key.Length];
            key = key.ToLower(); //zabezpieczenie, gdyż iterujemy po wartościach ASCII
            int availableNumber = 1;

            //utworzenie macierzy zawierającej indeksy kolumn do odczytania szyfrogramu
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

            //wprowadzenie liter słowa do macierzy
            for (int row = 0; row < rowCount; row++)
            {
                for (int col = 0; col < key.Length; col++)
                {
                    char currentChar = cipherTextCharIndex < cipherText.Length ? cipherText[cipherTextCharIndex++] : '\0';
                    mainMatrix[row, col] = currentChar;
                }
            }

            //odczytanie szyfrogramu zgodnie z indeksem kolumn
            string result = "";
            for (int currentKeyCol = 1; currentKeyCol < availableNumber; currentKeyCol++)
            {
                int col = Array.IndexOf(keyIndexes, currentKeyCol);

                for (int row = 0; row < rowCount; row++)
                {
                    if(mainMatrix[row, col] != (char)0) result += mainMatrix[row, col]; 
                }
            }

            return result;
        }

        public static string DecipherMatrixTransp(string ciphertext, string key)
        {
            int[] keyIndexes = new int[key.Length];
            key = key.ToLower(); //zabezpieczenie, bo iterujemy po wartościach ASCII
            int availableNumber = 1;

            for (int @char = 'a'; @char < 'z'; @char++)
            {
                for (int cellIndex = 0; cellIndex < keyIndexes.Length; cellIndex++)
                {
                    if (key[cellIndex] == @char)
                    {
                        keyIndexes[cellIndex] = availableNumber++;
                    }
                }
            }

            int maxRow = (int)Math.Ceiling((double)ciphertext.Length / keyIndexes.Length);

            char[,] array = new char[keyIndexes.Length, maxRow];

            //oznaczenie miejsc w tabeli, ktore powinny zostac zapelnione literami
            int cellsToTag = ciphertext.Length;
            for (int row = 0; row < maxRow; row++)
            {
                for (int col = 0; col < keyIndexes.Length; col++)
                {
                    array[col, row] = cellsToTag-- > 0 ? (char)1 : (char)0;
                }
            }

            //wpisanie zaszyfrowanego slowa do tabeli
            int counter = 0;
            for (int keyIndex = 1; keyIndex <= keyIndexes.Length; keyIndex++)
            {
                int col = Array.IndexOf(keyIndexes, keyIndex);

                for (int row = 0; row < maxRow; row++)
                {
                    if (array[col, row] == (char)1)
                    {
                        array[col, row] = ciphertext[counter++];
                    }
                }
            }

            //odczytanie oryginalnej wiadomosci
            string result = "";

            for (int row = 0; row < maxRow; row++)
            {
                for (int col = 0; col < keyIndexes.Length; col++)
                {
                    if(array[col, row] != (char)0) result += array[col, row];
                }
            }

            return result;
        }
    }
}
