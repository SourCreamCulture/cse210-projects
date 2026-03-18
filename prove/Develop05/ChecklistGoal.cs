using System;

public class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonusPoints;

    public ChecklistGoal(string name, string description, int points, int targetCount, int bonusPoints) 
        : base(name, description, points)
    {
        _targetCount = targetCount;
        _currentCount = 0;
        _bonusPoints = bonusPoints;
    }

    public ChecklistGoal(string name, string description, int points, bool isComplete, 
                         int targetCount, int currentCount, int bonusPoints) 
        : base(name, description, points, isComplete)
    {
        _targetCount = targetCount;
        _currentCount = currentCount;
        _bonusPoints = bonusPoints;
    }

    public int TargetCount => _targetCount;
    public int CurrentCount => _currentCount;
    public int BonusPoints => _bonusPoints;

    public override int RecordEvent()
    {
        if (!IsComplete)
        {
            _currentCount++;
            int totalPoints = Points;

            if (_currentCount >= _targetCount)
            {
                SetComplete(true);
                totalPoints += _bonusPoints;
                Console.WriteLine($"*** BONUS! You completed '{Name}' and earned an extra {_bonusPoints} points! ***");
            }

            return totalPoints;
        }
        return 0;
    }

    public override string GetStringRepresentation()
    {
        return $"ChecklistGoal:{Name},{Description},{Points},{IsComplete},{_targetCount},{_currentCount},{_bonusPoints}";
    }

    public override string GetDetailsString()
    {
        return $"{GetStatusIcon()} {Name} ({Description}) -- Completed {_currentCount}/{_targetCount} times";
    }
}
