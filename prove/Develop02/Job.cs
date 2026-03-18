public class Job
{
    public string _startDate;
    public string _title;
    public string _endDate;

    public string GetDescription()
    {
        return $"Job {_title} starting {_startDate} and ending {_endDate}";
    }
}