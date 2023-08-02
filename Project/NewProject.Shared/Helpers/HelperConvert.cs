using System;
using System.Globalization;

namespace VacanciesTask.Shared.Helpers;

public static partial class Helper
{
    public static int RandomNum(int min = 1000, int max = 9999)
    {
        var random = new Random();
        return random.Next(min, max);
    }

    public static string? cutString(string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value)) return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
    }

    public static string CapitalizeFirstLetter(string text)
    {
        return char.ToUpper(text[0]) + text.Substring(1);
    }
    
    public static string LowerFirstLetter(string text)
    {
        return char.ToLower(text[0]) + text.Substring(1);
    }

    public static string DateToString(DateTime dateTime, string format = "dd/MM/yyyy")
    {
        return dateTime.ToString(format);
    }
    
    public static string diffDate(DateTime From, DateTime To)
    {
        TimeSpan span = (To - From);
        return span.Days == 0 || span.Hours ==0?
         String.Format("{2} minutes",
            span.Hours, span.Minutes, span.Seconds):
         String.Format("{0} days & {1} hours",
            span.Days, span.Hours, span.Minutes, span.Seconds);
    }
    
    public static string[] WeekDays()
    {
        string[] dayWeek = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
        var d = new DateTime().ToString("Wednesday", new CultureInfo("ar-EG"));
        return dayWeek;
    }
}