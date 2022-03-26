

using BSKSzyfrogramy;

string ciphertext = Console.ReadLine();

Console.WriteLine(CipherService.CipherRailFence(ciphertext, 3));


Console.WriteLine(CipherService.CipherMatrixTransp(ciphertext));
Console.WriteLine(CipherService.DecipherMatrixTransp("ypctrraopghy"));
