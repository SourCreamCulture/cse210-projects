using System;

public class EternalGoal : Goal
{
    private int _timesRecorded;

    public EternalGoal(string name, string description, int points) 
        : base(name, description, points)
    {
        _timesRecorded = 0;
    }

    public EternalGoal(string name, string description, int points, int timesRecorded) 
        : base(name, description, points, false)
    {
        _timesRecorded = timesRecorded;
    }

    public int TimesRecorded => _timesRecorded;

    public override int RecordEvent()
    {
        _timesRecorded++;
        return Points;
    }

    public override string GetStringRepresentation()
    {
        return $"EternalGoal:{Name},{Description},{Points},{_timesRecorded}";
    }

    public override string GetDetailsString()
    {
        return $"[∞] {Name} ({Description}) -- Times completed: {_timesRecorded}";
    }

    public override string GetStatusIcon()
    {
        return "[∞]";
    }
}
