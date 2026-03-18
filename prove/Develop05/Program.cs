using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                    ETERNAL QUEST                             ║");
        Console.WriteLine("║         Track your goals, earn points, level up!               ║");
        Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
        Console.WriteLine();

        GoalManager manager = new GoalManager();
        manager.Start();
    }
}
