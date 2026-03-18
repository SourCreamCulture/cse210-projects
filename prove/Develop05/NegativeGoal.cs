using System;

public class NegativeGoal : Goal
{
    private int _timesRecorded;

    public NegativeGoal(string name, string description, int penaltyPoints) 
        : base(name, description, penaltyPoints)
    {
        _timesRecorded = 0;
    }

    public NegativeGoal(string name, string description, int penaltyPoints, int timesRecorded) 
        : base(name, description, penaltyPoints, false)
    {
        _timesRecorded = timesRecorded;
    }

    public int TimesRecorded => _timesRecorded;

    public override int RecordEvent()
    {
        _timesRecorded++;
        Console.WriteLine($"⚠️  You recorded a negative event: '{Name}'. {-Points} points deducted.");
        return -Points;
    }

    public override string GetStringRepresentation()
    {
        return $"NegativeGoal:{Name},{Description},{Points},{_timesRecorded}";
    }

    public override string GetDetailsString()
    {
        return $"[!] {Name} ({Description}) -- Times recorded: {_timesRecorded} (BAD HABIT - lose {Points} points each time)";
    }

    public override string GetStatusIcon()
    {
        return "[!]";
    }
}
