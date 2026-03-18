using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private int _level;
    private List<string> _badges;

    private static readonly int[] LevelThresholds = { 0, 1000, 2500, 5000, 10000, 20000, 50000 };
    private static readonly string[] LevelTitles = { 
        "Novice", "Apprentice", "Journeyman", "Expert", "Master", "Grandmaster", "Legend" 
    };

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _level = 0;
        _badges = new List<string>();
    }

    public void Start()
    {
        bool running = true;

        while (running)
        {
            DisplayScore();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. View Badges");
            Console.WriteLine("  7. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    CreateGoal();
                    break;
                case "2":
                    ListGoals();
                    break;
                case "3":
                    SaveGoals();
                    break;
                case "4":
                    LoadGoals();
                    break;
                case "5":
                    RecordEvent();
                    break;
                case "6":
                    DisplayBadges();
                    break;
                case "7":
                    running = false;
                    Console.WriteLine("Goodbye! Keep working on your Eternal Quest!");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    private void DisplayScore()
    {
        UpdateLevel();
        Console.WriteLine($"╔════════════════════════════════════════════════════════╗");
        Console.WriteLine($"║  You have {_score} points.                                    ║");
        Console.WriteLine($"║  Level: {_level} - {LevelTitles[Math.Min(_level, LevelTitles.Length - 1)]}                                  ║");
        Console.WriteLine($"╚════════════════════════════════════════════════════════╝");
    }

    private void UpdateLevel()
    {
        int oldLevel = _level;
        for (int i = LevelThresholds.Length - 1; i >= 0; i--)
        {
            if (_score >= LevelThresholds[i])
            {
                _level = i;
                break;
            }
        }

        if (_level > oldLevel)
        {
            Console.WriteLine();
            Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine($"║  ★ LEVEL UP! You are now Level {_level} - {LevelTitles[_level]}! ★              ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════╝");
            
            string badgeName = $"{LevelTitles[_level]} Badge";
            if (!_badges.Contains(badgeName))
            {
                _badges.Add(badgeName);
                Console.WriteLine($">>> You earned the {badgeName}! <<<");
            }
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("The types of goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        Console.WriteLine("  4. Negative Goal (lose points for bad habits)");
        Console.Write("Which type of goal would you like to create? ");

        string type = Console.ReadLine();
        Console.WriteLine();

        Console.Write("What is the name of your goal? ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? ");
        string description = Console.ReadLine();

        Goal newGoal = null;

        switch (type)
        {
            case "1":
                Console.Write("What is the amount of points associated with this goal? ");
                int simplePoints = int.Parse(Console.ReadLine());
                newGoal = new SimpleGoal(name, description, simplePoints);
                break;

            case "2":
                Console.Write("What is the amount of points associated with this goal? ");
                int eternalPoints = int.Parse(Console.ReadLine());
                newGoal = new EternalGoal(name, description, eternalPoints);
                break;

            case "3":
                Console.Write("What is the amount of points associated with this goal? ");
                int checklistPoints = int.Parse(Console.ReadLine());
                Console.Write("How many times does this goal need to be accomplished for a bonus? ");
                int targetCount = int.Parse(Console.ReadLine());
                Console.Write("What is the bonus for accomplishing it that many times? ");
                int bonusPoints = int.Parse(Console.ReadLine());
                newGoal = new ChecklistGoal(name, description, checklistPoints, targetCount, bonusPoints);
                break;

            case "4":
                Console.Write("What is the amount of points to LOSE when you record this bad habit? ");
                int penaltyPoints = int.Parse(Console.ReadLine());
                newGoal = new NegativeGoal(name, description, penaltyPoints);
                break;

            default:
                Console.WriteLine("Invalid goal type.");
                return;
        }

        if (newGoal != null)
        {
            _goals.Add(newGoal);
            Console.WriteLine();
            Console.WriteLine($"Goal '{name}' created successfully!");
        }
    }

    private void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals created yet.");
            return;
        }

        Console.WriteLine("Your Goals:");
        Console.WriteLine();

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available to record.");
            return;
        }

        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"  {i + 1}. {_goals[i].Name}");
        }

        Console.Write("Which goal did you accomplish? ");
        if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= _goals.Count)
        {
            Goal goal = _goals[choice - 1];
            int pointsEarned = goal.RecordEvent();
            _score += pointsEarned;

            if (pointsEarned > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"Congratulations! You have earned {pointsEarned} points!");
                CheckAchievements(goal);
            }
            else if (pointsEarned < 0)
            {
                _score = Math.Max(0, _score);
                Console.WriteLine($"Your score is now {_score} points.");
            }
            else
            {
                Console.WriteLine("This goal has already been completed.");
            }
        }
        else
        {
            Console.WriteLine("Invalid selection.");
        }
    }

    private void CheckAchievements(Goal goal)
    {
        if (!_badges.Contains("First Steps") && _score > 0)
        {
            _badges.Add("First Steps");
            Console.WriteLine($">>> You earned the First Steps Badge! <<<");
        }

        if (!_badges.Contains("High Achiever") && _score >= 5000)
        {
            _badges.Add("High Achiever");
            Console.WriteLine($">>> You earned the High Achiever Badge! <<<");
        }

        if (goal is EternalGoal eternal && eternal.TimesRecorded >= 30 && !_badges.Contains("Eternal Dedication"))
        {
            _badges.Add("Eternal Dedication");
            Console.WriteLine($">>> You earned the Eternal Dedication Badge for completing an eternal goal 30+ times! <<<");
        }

        if (goal is ChecklistGoal checklist && checklist.IsComplete && !_badges.Contains("Checklist Master"))
        {
            _badges.Add("Checklist Master");
            Console.WriteLine($">>> You earned the Checklist Master Badge! <<<");
        }
    }

    private void DisplayBadges()
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                   YOUR BADGES                          ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════╝");

        if (_badges.Count == 0)
        {
            Console.WriteLine("No badges earned yet. Keep working on your goals!");
        }
        else
        {
            foreach (string badge in _badges)
            {
                Console.WriteLine($"  ★ {badge}");
            }
        }
        Console.WriteLine();
        Console.WriteLine($"Total Badges: {_badges.Count}");
    }

    private void SaveGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            outputFile.WriteLine(_score);
            outputFile.WriteLine(string.Join(",", _badges));

            foreach (Goal goal in _goals)
            {
                outputFile.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine($"Goals saved to {filename}");
    }

    private void LoadGoals()
    {
        Console.Write("What is the filename for the goal file? ");
        string filename = Console.ReadLine();

        if (!File.Exists(filename))
        {
            Console.WriteLine("File not found.");
            return;
        }

        string[] lines = File.ReadAllLines(filename);
        _goals.Clear();

        _score = int.Parse(lines[0]);

        string badgesLine = lines[1];
        _badges = string.IsNullOrEmpty(badgesLine) 
            ? new List<string>() 
            : badgesLine.Split(',').ToList();

        for (int i = 2; i < lines.Length; i++)
        {
            string line = lines[i];
            string[] parts = line.Split(":");
            string goalType = parts[0];
            string[] details = parts[1].Split(",");

            Goal goal = null;

            switch (goalType)
            {
                case "SimpleGoal":
                    goal = new SimpleGoal(
                        details[0],
                        details[1],
                        int.Parse(details[2]),
                        bool.Parse(details[3])
                    );
                    break;

                case "EternalGoal":
                    goal = new EternalGoal(
                        details[0],
                        details[1],
                        int.Parse(details[2]),
                        int.Parse(details[3])
                    );
                    break;

                case "ChecklistGoal":
                    goal = new ChecklistGoal(
                        details[0],
                        details[1],
                        int.Parse(details[2]),
                        bool.Parse(details[3]),
                        int.Parse(details[4]),
                        int.Parse(details[5]),
                        int.Parse(details[6])
                    );
                    break;

                case "NegativeGoal":
                    goal = new NegativeGoal(
                        details[0],
                        details[1],
                        int.Parse(details[2]),
                        int.Parse(details[3])
                    );
                    break;
            }

            if (goal != null)
            {
                _goals.Add(goal);
            }
        }

        Console.WriteLine($"Goals loaded from {filename}");
    }
}
