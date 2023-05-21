using System;
using Timer = System.Timers.Timer;

namespace PasswortGenerator;
public class PasswortMain
{
    private static int length = 0;
    private static bool specialChar;
    private Random rand = new Random(DateTime.Now.Millisecond);
    private static string password;
    
    public static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        while (true) //this loop asks the user if he wants to use white-mode. The code could would work without the loop.
        {
            Console.WriteLine("Do you want to use white-mode (y/n))?: ");
            string input = Console.ReadLine();
            if (input.Contains('y'))
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.Clear();
                break;
            } else if (input.Contains('n'))
            {
                Console.Clear();
                break;
            }
            else
            {
                Console.WriteLine(input + " is not a valid respond. Please use y/n");
            }
        }

        while (true) 
        {
            try
            {
                Console.WriteLine("How long should the password be?: ");
                int input = int.Parse(Console.ReadLine());
                if (input < 0) throw new FormatException(input + " has to be positive!");
                length = input;
                break;
            }
            catch (FormatException e)
            {
                if (!e.Message.Contains(" has to be positive!"))
                {
                    Console.WriteLine("Please provide a valid number!");
                }
                else
                {
                    Console.WriteLine("Please provide a positive number! (" + e.Message.Split()[0] + " < 0)");
                }
            }
        }

        while (true)
        {
            Console.WriteLine("Should you password contain special characters? ");
            string input = Console.ReadLine();
            if (input.Contains('y')) {specialChar = true; break;}
            else if (input.Contains('n')) {specialChar = false; break;}
        }

        PasswortMain pswGen = new PasswortMain();
        Console.WriteLine("Your password is:");
        Console.WriteLine(password = pswGen.Generator(length,specialChar));
        Console.ReadKey();
    }

    public string Generator(int length, bool specialCharacters)
    {
        string retString = "";
        for (int i = 0; i < length; i++)
        {
            if (specialChar) retString += SymbolsList.getSymbols(rand.Next(0, SymbolsList.symbolLength + 1));
            else retString += SymbolsList.getSymbols(rand.Next(0, SymbolsList.symbolLengthNoSpec + 1));
        }

        return retString;
    }
}

public class SymbolsList
{
    private static readonly string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!\"#$%&'()*+,-./:;<=>?@[]^_`{|}~";
    public static readonly int symbolLength = symbols.Length;
    public static readonly int symbolLengthNoSpec = symbols.Length-32; //gives the length without special symbols like (!, ?, @...,)

    public static char? getSymbols(int index)
    {
        if (index < symbols.Length && index >= 0)
        {
            return symbols[index];
        }

        return null;
    }
}