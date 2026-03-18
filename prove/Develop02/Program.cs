using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Develop02 World!");

        Resume myResume = new Resume();
        myResume._name = "Joe";
        myResume._education = new List<Education>();

        Job job1 = new Job();
        job1._title = "Software Developer";
        job1._startDate = "Jan 02 2003";
        job1._endDate = "Feb 01 2008";

        Job job2 = new Job();
        job2._title = "Analyst";
        job2._startDate = "Jan 02 2010";

        myResume._experience.Add(job1);
        myResume._experience.Add(job2);

        Education educ1 = new Education();
        educ1._school = "BYU";

        myResume._education.Add(educ1);

        myResume.DisplayFullResume();
    }
}