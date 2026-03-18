using System;

public abstract class Goal
{
    private string _name;
    private string _description;
    private int _points;
    private bool _isComplete;

    public Goal(string name, string description, int points)
    {
        _name = name;
        _description = description;
        _points = points;
        _isComplete = false;
    }

    protected Goal(string name, string description, int points, bool isComplete)
    {
        _name = name;
        _description = description;
        _points = points;
        _isComplete = isComplete;
    }

    public string Name => _name;
    public string Description => _description;
    public int Points => _points;
    public bool IsComplete => _isComplete;

    protected void SetComplete(bool complete)
    {
        _isComplete = complete;
    }

    public abstract int RecordEvent();
    public abstract string GetStringRepresentation();
    public abstract string GetDetailsString();

    public virtual string GetStatusIcon()
    {
        return _isComplete ? "[X]" : "[ ]";
    }
}
