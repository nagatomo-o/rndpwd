using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RndPwd
{
    class Program
    {
        static void Main(string[] args)
        {
            // Default values
            int length = 8;
            bool useNumbers = false;
            bool useLower = false;
            bool useUpper = false;
            bool useSymbols = false;
            bool noDuplicates = false;
            string symbolChars = "!#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            string prefix = "";

            // Parse options
            for (int i = 0; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.StartsWith("--length=") || arg.StartsWith("-l"))
                {
                    string val = arg.Contains("=") ? arg.Split('=')[1] : args[++i];
                    int.TryParse(val, out length);
                }
                else if (arg == "--numbers" || arg == "-n")
                {
                    useNumbers = true;
                }
                else if (arg == "--lower-alfabet" || arg == "-la")
                {
                    useLower = true;
                }
                else if (arg == "--upper-alfabet" || arg == "-ua")
                {
                    useUpper = true;
                }
                else if (arg == "--symbol" || arg == "-s")
                {
                    useSymbols = true;
                    // Specify symbol characters
                    if (i + 1 < args.Length && !args[i + 1].StartsWith("-"))
                    {
                        symbolChars = args[++i];
                    }
                }
                else if (arg.StartsWith("--prefix=") || arg.StartsWith("-p"))
                {
                    prefix = arg.Contains("=") ? arg.Split('=')[1] : args[++i];
                }
                else if (arg == "--no-duplicates" || arg == "-nd")
                {
                    noDuplicates = true;
                }
            }

            // If none specified, use lowercase letters + numbers by default
            if (!useNumbers && !useLower && !useUpper && !useSymbols)
            {
                useNumbers = useLower = true;
            }

            // Build character set
            string chars = "";
            if (useNumbers) chars += "0123456789";
            if (useLower) chars += "abcdefghijklmnopqrstuvwxyz";
            if (useUpper) chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (useSymbols) chars += symbolChars;

            if (string.IsNullOrEmpty(chars))
            {
                Console.Error.WriteLine("No character sets specified for password generation.");
                Environment.Exit(1);
            }

            // Generate password
            string password = GeneratePassword(length, chars, prefix, noDuplicates);

            // Output password
            Console.WriteLine(password);
        }

        static string GeneratePassword(int length, string chars, string prefix, bool noDuplicates)
        {
            Random rand = new Random();
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(prefix))
            {
                sb.Append(prefix);
            }

            int remain = length - sb.Length;
            List<char> charList = chars.ToList();

            if (noDuplicates && remain > charList.Count)
            {
                throw new ArgumentException("Not enough unique characters for the requested length without duplicates.");
            }

            for (int i = 0; i < remain; i++)
            {
                char c;
                if (noDuplicates)
                {
                    int idx = rand.Next(charList.Count);
                    c = charList[idx];
                    charList.RemoveAt(idx);
                }
                else
                {
                    c = chars[rand.Next(chars.Length)];
                }
                sb.Append(c);
            }
            return sb.ToString();
        }
    }
}
