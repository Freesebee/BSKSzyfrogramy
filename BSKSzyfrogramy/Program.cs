

using BSKSzyfrogramy;

string ciphertext = Console.ReadLine();

string cipher = CipherService.CipherRailFence(ciphertext, 3);

Console.WriteLine(cipher);

string decipheredText = CipherService.DecipherRailFence(cipher, 3);

Console.WriteLine(decipheredText);
