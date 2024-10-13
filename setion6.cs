using System;

public class String
{
    public static void Main(string[] args)
    {
        //string01();
        //string02();
        //string03();
        //string04();
        //string05();
        //string06();
        string07();
        //string08();
        //string09();
    }
    static void string01()
    {
        Console.WriteLine("input the string: ");
        string a = Console.ReadLine();
        Console.WriteLine($"the string you entered is:{a} ");
    }
    static void string02()
    {
        Console.WriteLine("input the string: ");
        string a = Console.ReadLine();
        int b = 0;
        foreach (char chr in a)
        {
            b += 1;
            if (chr == ' ') b -= 1;
        }
        Console.WriteLine($"length of the string is : {b}");
    }
    static void string03()
    {
        Console.WriteLine("input the string: ");
        string a = Console.ReadLine();
        int b = 0;
        Console.WriteLine("The characters of the string is:");
        while (b <= a.Length - 1)
        {
            Console.Write("{0} ", a[b]);
            b++;
        }
        Console.WriteLine("\n\n");
    }
    static void string04()
    {
        Console.WriteLine("input the string: ");
        string a = Console.ReadLine();
        int b = a.Length - 1;
        Console.WriteLine("The characters of the string is:");
        while (b >= 0)
        {
            Console.Write("{0} ", a[b]);
            b--;
        }
        Console.WriteLine("\n\n");
    }
    static void string05()
    {
        Console.WriteLine("input the string: ");
        string a = Console.ReadLine();
        int b = 0;
        int c = 0;
        a = a.Trim();
        foreach (char chr in a)
        {
            c++;
            if (chr == ' ')
                b += 1;
        }
        if (!string.IsNullOrEmpty(a)) b++;
        int space = b + 1;
        Console.WriteLine($"The total number of words in string {a} is: {space}");
    }
    static void string06()
    {
        Console.WriteLine("input the string 1: ");
        string a = Console.ReadLine();
        Console.WriteLine("input the string 2: ");
        string b = Console.ReadLine();
        if (a.Length == b.Length)
        {

            foreach (char chr in a)
            {
                foreach (char c in b)
                    if (chr != c)
                    {
                        Console.WriteLine("two strings are not equal");
                        break;
                    }
            }
            Console.WriteLine("Two string are equal");
        }
        else if (a.Length < b.Length) Console.WriteLine("string 1 is shorter than string 2");
        else Console.WriteLine("string 1 is longer than string 2");

    }
    static void string07()
    {
        Console.WriteLine("input the string: ");
        string a = Console.ReadLine();
        int b = 0;
        int c = 0;
        int e = 0;
        a = a.ToLower();
        foreach (int d in a)
        {
            if (d >= '0' && d <= '9') b++;
            else if (d >= 'a' && d <= 'z') c++;
            else e++;
        }
        Console.WriteLine($"the number of alphabets: {c}; the number of digits: {b}; the number of special characters: {e}");
    }
    static void string08()
    {
        Console.WriteLine("input the string: ");
        string a = Console.ReadLine();
        int b = 0;
        int vowels = 0;
        int conso = 0;
        foreach (char chr in a)
        {
            b++;
            if (chr == ' ') b--;
            if (chr == 'a' || chr == 'i' || chr == 'e' || chr == 'u' || chr == 'o') vowels++;
        }
        conso = b - vowels;
        Console.WriteLine($"the number of vowels: {vowels}; the number of consonants: {conso}");
    }
    static void string09()
    {

    }
    static void string10()
    {
        
    }
}
