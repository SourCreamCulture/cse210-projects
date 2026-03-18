using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        string[] wordArray = text.Split(' ');
        foreach (string wordText in wordArray)
        {
            if (!string.IsNullOrWhiteSpace(wordText))
            {
                _words.Add(new Word(wordText));
            }
        }
    }

    public void HideRandomWords(int count)
    {
        Random random = new Random();

        for (int i = 0; i < count; i++)
        {
            int index = random.Next(_words.Count);
            _words[index].Hide();
        }
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }

    public void Display()
    {
        Console.WriteLine(_reference.GetDisplayText());
        Console.WriteLine();

        List<string> displayWords = new List<string>();
        foreach (Word word in _words)
        {
            displayWords.Add(word.GetDisplayText());
        }

        string scriptureText = string.Join(" ", displayWords);
        Console.WriteLine(scriptureText);
    }
}
