using System;

class Program
{
    static void Main(string[] args)
    {
        List<Scripture> scriptures = new List<Scripture>
        {
            new Scripture(
                new Reference("John", 3, 16),
                "For God so loved the world that he gave his only begotten Son"
            ),
            new Scripture(
                new Reference("Proverbs", 3, 5, 6),
                "Trust in the Lord with all thine heart and lean not unto thine own understanding"
            ),
            new Scripture(
                new Reference("Philippians", 4, 13),
                "I can do all things through Christ which strengtheneth me"
            ),
            new Scripture(
                new Reference("Psalm", 23, 1),
                "The Lord is my shepherd I shall not want"
            ),
            new Scripture(
                new Reference("Moroni", 10, 4, 5),
                "And when ye shall receive these things I would exhort you that ye would ask God"
            )
        };

        Random random = new Random();
        Scripture scripture = scriptures[random.Next(scriptures.Count)];

        int wordsToHide = 2;

        while (true)
        {
            Console.Clear();

            scripture.Display();

            if (scripture.IsCompletelyHidden())
            {
                Console.WriteLine("\nAll words are now hidden! Memorization complete.");
                break;
            }

            Console.WriteLine("\nPress Enter to hide more words, or type 'quit' to exit:");

            string input = Console.ReadLine();

            if (input?.ToLower() == "quit")
            {
                break;
            }

            scripture.HideRandomWords(wordsToHide);

            if (wordsToHide < 5)
            {
                wordsToHide++;
            }
        }

        Console.WriteLine("\nThank you for using the Scripture Memorizer!");
    }
}
