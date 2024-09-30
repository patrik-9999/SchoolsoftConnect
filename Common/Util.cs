using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Util
    {
        public static string Normalize12(string personnummer)
        // Returnerar personummer som yyyymmddxxxx
        {
            var trimmed = personnummer.Trim();
            if (trimmed.Length == 11)
            {
                // yymmdd-xxxx
                if (int.TryParse(trimmed.AsSpan(0, 2), out var year))
                {
                    if (year > (DateTime.Now.Year - 2000) && year <= 99)
                    {
                        return string.Concat("19", trimmed.AsSpan(0, 6), trimmed.AsSpan(7, 4));
                    }
                    else
                    {
                        return string.Concat("20", trimmed.AsSpan(0, 6), trimmed.AsSpan(7, 4));
                    }
                }
                else
                {
                    // Om det inte går att tolka årtalet, returnera som det är
                    return trimmed;
                }
            }
            else if (trimmed.Length == 10)
            {
                // yymmddxxxx
                if (int.TryParse(trimmed.AsSpan(0, 2), out var year))
                {
                    if (year > (DateTime.Now.Year - 2000) && year <= 99)
                    {
                        return string.Concat("19", trimmed);
                    }
                    else
                    {
                        return string.Concat("20", trimmed);
                    }
                }
                else
                {
                    // Om det inte går att tolka årtalet, returnera som det är
                    return trimmed;
                }
            }
            else if (trimmed.Length == 13)
            {
                // yyyymmdd-xxxx
                return string.Concat(trimmed.AsSpan(0, 8), trimmed.AsSpan(9, 4));
            }
            else
            {
                return trimmed;
            }
        }

        public static string Normalize13(string personnummer)
        // Returnerar personummer som yyyymmdd-xxxx
        {
            var trimmed = personnummer.Trim();
            if (trimmed.Length == 12)
            {
                // yyyymmddxxxx
                return string.Concat(trimmed.AsSpan(0, 8), "-", trimmed.AsSpan(8, 4));
            }
            else if (trimmed.Length == 11)
            {
                // yymmdd-xxxx
                if (int.TryParse(trimmed.AsSpan(0, 2), out var year))
                {
                    if (year > (DateTime.Now.Year - 2000) && year <= 99)
                    {
                        return string.Concat("19", trimmed);
                    }
                    else
                    {
                        return string.Concat("20", trimmed);
                    }
                }
                else
                {
                    // Om det inte går att tolka årtalet, returnera som det är
                    return trimmed;
                }
            }
            else if (trimmed.Length == 10)
            {
                // yymmddxxxx
                if (int.TryParse(trimmed.AsSpan(0, 2), out var year))
                {
                    if (year > (DateTime.Now.Year - 2000) && year <= 99)
                    {
                        return string.Concat("19", trimmed.AsSpan(0,6),"-",trimmed.AsSpan(6,4));
                    }
                    else
                    {
                        return string.Concat("20", trimmed.AsSpan(0, 6), "-", trimmed.AsSpan(6, 4));
                    }
                }
                else
                {
                    // Om det inte går att tolka årtalet, returnera som det är
                    return trimmed;
                }
            }

            else
            {
                return trimmed;
            }
        }

        public static string GeneratePassword(int length)
        {
            if (length < 4)
                length = 4;
            if (length > 128)
                length = 128;
            var upperCaseChars = "ABCDEFGHJKLMNPRSTUVWXYZÅÄÖ";      // "IOQ" är inte med för att undvika förväxling
            var lowerCaseChars = "abcdefghijkmnopqrstuvwxyzåäö";    // "l" är inte med
            var numbers = "23456789";                               // "01" är inte med
            var specialChars = "!@#$%&()_+-*/-";                    // specialtecken som är någorlunda enkla att skriva
            var allChars = upperCaseChars + lowerCaseChars + numbers + specialChars;
            var password = new char[length];
            var random = new Random();

            // Första tecknet är alltid en liten bokstav
            password[0] = lowerCaseChars[random.Next(lowerCaseChars.Length)];
            // Minst ett specialtecken
            password[1] = specialChars[random.Next(specialChars.Length)];
            // Minst en siffra
            password[2] = numbers[random.Next(numbers.Length)];
            // Minst en stor bokstav
            password[3] = upperCaseChars[random.Next(upperCaseChars.Length)];
            for(int i = 4; i < length; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }
            // Byt plats på tecken 1-3 så de inte alltid hamnar på samma plats
            for (int i = 1; i < 4; i++)
            {
                int j = random.Next(1, length);
                (password[j], password[i]) = (password[i], password[j]);
            }
            return new string(password);
        }
    }
}
