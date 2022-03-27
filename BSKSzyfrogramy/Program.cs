using BSKSzyfrogramy;

public static class Program
{
    public delegate string Operation(string ciphertext, List<string> additionalArgs);

    static void Main(string[] args)
    //.exe
    //cipher-text
    //OPTIONAL [
    //[-m, c (cipher) | d (decipher)],
    //-a, rf (railfence)| mtn (matrix-transposition-number key) | mtk (matrix-transposition with text key)]
    //],
    //-p, additionalArgs:{}
    {
        if (args == null || args.Count() < 2)
        {
            throw new ArgumentException("Invalid number of arguments.\nRequired syntax:\n" + CorrectSyntax);
        }

        string ciphertext = args[0];
        bool? cipherMode = null;
        Operation alghorithm = null;
        List<string> additionalArgs = new List<string>();
        
        for (int i = 1; i < args.Count(); i++)
        {
            switch(args[i])
            {
                case "-m":

                    if(cipherMode != null) throw new ArgumentException("Alghorithm mode is already specified");

                    switch (args[++i])
                    {
                        case "c": cipherMode = true; break;
                        case "d": cipherMode = false; break;
                        default: throw new ArgumentException("Invalid cipher mode");
                    }


                    break;

                case "-a":

                    if (alghorithm != null) throw new ArgumentException("Alghorithm is already specified");

                    switch (args[++i])
                    {
                        case "rf":

                            alghorithm = (bool)cipherMode!
                                ? delegate (string text, List<string> additionalArguments)
                                {
                                    return CipherService.CipherRailFence(text, int.Parse(additionalArguments.First()));
                                }
                                : delegate (string text, List<string> additionalArguments)
                                {
                                    return CipherService.DecipherRailFence(text, int.Parse(additionalArguments.First()));
                                };

                            break;

                        case "mtn":

                            alghorithm = (bool)cipherMode!
                                ? delegate (string text, List<string> additionalArguments)
                                {
                                    return CipherService.CipherMatrixTransp(text, additionalArguments.Select(int.Parse).ToArray());
                                }
                                : delegate (string text, List<string> additionalArguments)
                                {
                                    return CipherService.DecipherMatrixTransp(text, additionalArguments.Select(int.Parse).ToArray());
                                };

                            break;

                        case "mtk":

                            alghorithm = (bool)cipherMode!
                                ? delegate (string text, List<string> additionalArguments)
                                {
                                    return CipherService.CipherMatrixTransp(text, additionalArguments.First());
                                }
                                : delegate (string text, List<string> additionalArguments)
                                {
                                    return CipherService.DecipherMatrixTransp(text, additionalArguments.First());
                                };

                            break;
                    }
                    break;

                case "-p":

                    if(alghorithm == null) throw new ArgumentException("Required syntax:\n" + CorrectSyntax);
                    i++;
                    do
                    {
                        additionalArgs.Add(args[i++]);
                    }
                    while (i < args.Count());
                    break;
                
                default:
                    throw new ArgumentException("Required syntax:\n" + CorrectSyntax);
            }
        }

        if (alghorithm == null) throw new ArgumentException("Alghorithm was not specified");
        if (additionalArgs == null) throw new ArgumentException("Additional arguments were not specified");
        string result = alghorithm(args[0], additionalArgs);
        Console.WriteLine(result);
    }

    private const string CorrectSyntax = 
        "cipher-text\n" +
        "[[-m, c(cipher) | d(decipher)],\n" +
        "-a, rf (railfence)| mtn (matrix-transp.number key)| mtk(matrix-transp.text key)]],\n" +
        "-p, params:{}";
}

