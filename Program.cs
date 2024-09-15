using System;
using System.IO;
using System.Reflection.Emit;
using System.Xml.Linq;

class Program
{
    static string[] lines;

    static void Main()
    {
        string filePath = "input.csv";
        lines = File.ReadAllLines(filePath);

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters(lines);
                    break;
                case "2":
                    AddCharacter(ref lines);
                    break;
                case "3":
                    LevelUpCharacter(lines);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        static void DisplayAllCharacters(string[] lines)
        {
            Console.WriteLine("\n=== Characters ===");
            // Skip the header row
            for (int i = 1; i < lines.Length; i++)
            {
                lines = File.ReadAllLines("input.csv");
                string line = lines[i];

                int commaIndex = lines[i].IndexOf(',');

                if (line.StartsWith('"'))
                {
                    string name = lines[i].Substring(0, commaIndex);
                    var firstQuotePos = lines[i].IndexOf('"');
                    lines[i] = lines[i].Substring(firstQuotePos + 1);
                    var lastQuotePos = lines[i].IndexOf('"');
                    name = lines[i].Substring(firstQuotePos, lastQuotePos - firstQuotePos);
                    commaIndex = lines[i].IndexOf('"') + 1;
                    Console.WriteLine($"\nName: {name}");

                    lines[i] = lines[i].Substring(name.Length + 2);
                    commaIndex = lines[i].IndexOf(",");
                    var characterClass = lines[i].Substring(0, commaIndex);
                    Console.WriteLine($"Class: {characterClass}");

                    lines[i] = lines[i].Substring(characterClass.Length + 1);
                    var splits = lines[i].Split(",");
                    var level = splits[0];
                    var HP = splits[1];
                    string[] equipment = splits[2].Split("|");

                    Console.WriteLine($"Level: {level}");
                    Console.WriteLine($"HP: {HP}");
                    Console.WriteLine($"Equipment: {string.Join(", ", equipment)}");
                }
                else
                {
                    lines = File.ReadAllLines("input.csv");
                    line = lines[i];
                    var splits = lines[i].Split(",");
                    var name = splits[0];
                    var characterClass = splits[1];
                    var level = splits[2];
                    var HP = splits[3];
                    string[] equipment = splits[4].Split("|");

                    Console.WriteLine($"\nName: {name}");
                    Console.WriteLine($"Class: {characterClass}");
                    Console.WriteLine($"Level: {level}");
                    Console.WriteLine($"HP: {HP}");
                    Console.WriteLine($"Equipment: {string.Join(", ", equipment)}");

                }
            }
        }

        static void AddCharacter(ref string[] lines)
        {
            // TODO: Implement logic to add a new character
            // Prompt for character details (name, class, level, hit points, equipment)
            // DO NOT just ask the user to enter a new line of CSV data or enter the pipe-separated equipment string
            // Append the new character to the lines array


            Console.Write("\n=== Add a Character ===\n");

            Console.Write("Enter your character's name: ");
            string charName = Console.ReadLine();

            Console.Write("Enter your character's class: ");
            string charClass = Console.ReadLine();

            Console.Write("Enter your character's level: ");
            int level = int.Parse(Console.ReadLine());

            Console.Write("Enter your character's HP: ");
            int hp = int.Parse(Console.ReadLine());

            Console.Write("Enter your character's equipment (separate items with a '|'): ");
            string[] equipment = Console.ReadLine().Split('|');


            StreamWriter sw = new StreamWriter("input.csv", true);
            sw.WriteLine($"{charName},{charClass},{level},{hp},{string.Join("|", equipment)}");
            sw.Flush();
            sw.Close();
            

            Console.WriteLine($"Welcome, {charName} the {charClass}! You are level {level} with {hp} HP, and your equipment includes: {string.Join(", ", equipment)}.\n");
        }

        static void LevelUpCharacter(string[] lines)
        {
            Console.Write("Enter the name of the character to level up: ");
            string nameToLevelUp = Console.ReadLine();

            // Loop through characters to find the one to level up
            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];

                // TODO: Check if the name matches the one to level up
                // Do not worry about case sensitivity at this point
                if (line.Contains(nameToLevelUp))
                {
                    int commaIndex = lines[i].IndexOf(',');
                
                    var splits = lines[i].Split(",");
                    var name = splits[0];
                    var characterClass = splits[1];
                    var level = splits[2];
                    var HP = splits[3];
                    string[] equipment = splits[4].Split("|");

                    int j = int.Parse(level);
                    var newlevel = Convert.ToString(j + 1);


                    lines[i] = ($"{name},{characterClass},{level},{HP},{string.Join("|", equipment)}");

                    StreamWriter sw = new StreamWriter("input.csv", true);
                    sw.WriteLine($"{name},{characterClass},{newlevel},{HP},{string.Join("|", equipment)}");
                    sw.Flush();
                    sw.Close();

                    Console.WriteLine($"{name} is now Level {newlevel}");
                    break;
                }
                // TODO: Split the rest of the fields locating the level field
                // string[] fields = ...
                // int level = ...

                // TODO: Level up the character
                // level++;
                // Console.WriteLine($"Character {name} leveled up to level {level}!");

                // TODO: Update the line with the new level
                // lines[i] = ...
            }
        }
    }
}

    