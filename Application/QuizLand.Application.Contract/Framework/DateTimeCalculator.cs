using System.Globalization;

namespace QuizLand.Application.Contract.Framework;

public static class DateTimeCalculator
{
    private static readonly CultureInfo FaPersian;
    static DateTimeCalculator()
    {
        FaPersian = new CultureInfo("fa-IR");
        FaPersian.DateTimeFormat.Calendar = new PersianCalendar();
    }

    public static string ToShamsiString(this DateTime dt)
        => dt.ToString("yyyy/MM/dd h:mm:ss tt", FaPersian);

    public static string ToShamsiDate(this DateTime dt)
        => dt.ToString("yyyy/MM/dd", FaPersian);
}