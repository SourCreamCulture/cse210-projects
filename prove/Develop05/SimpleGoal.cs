using System;

public class SimpleGoal : Goal
{
    public SimpleGoal(string name, string description, int points) 
        : base(name, description, points)
    {
    }

    public SimpleGoal(string name, string description, int points, bool isComplete) 
        : base(name, description, points, isComplete)
    {
    }

    public override int RecordEvent()
    {
        if (!IsComplete)
        {
            SetComplete(true);
            return Points;
        }
        return 0;
    }

    public override string GetStringRepresentation()
    {
        return $"SimpleGoal:{Name},{Description},{Points},{IsComplete}";
    }

    public override string GetDetailsString()
    {
        return $"{GetStatusIcon()} {Name} ({Description})";
    }
}
