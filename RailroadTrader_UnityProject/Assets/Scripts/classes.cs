public class IngameTime
{
    public int hour { get; private set; }
    public int day { get; private set; }
    public int month { get; private set; }
    public int year { get; private set; }

    public IngameTime(int hour, int day, int month, int year)
    {
        this.hour = hour;
        this.day = day;
        this.month = month;
        this.year = year;
    }

    /// <summary>
    /// Adds an hour to the count and returns true if 24 hour mark was surpassed
    /// </summary>
    public bool ChangeDay(int amount = 1)
    {
        hour += amount;
        if (hour > 24)
        {
            hour = 1;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds a day to the count and returns true if 30 days mark was surpassed
    /// </summary>
    public bool ChangeMonth(int amount = 1)
    {
        day += amount;
        if (day > 30)
        {
            day = 1;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Adds a month to the count and returns true if 12 month mark was surpassed
    /// </summary>
    public bool ChangeYear(int amount = 1)
    {
        month += amount;
        if (month > 12)
        {
            month = 1;
            year += 1;
            return true;
        }
        return false;
    }
}

public class FinanceOverview
{
    public int monthlyIncome = 0;
    public int monthlyUpkeep = 0;
    public int monthlyRevenue = 0;
    public int monthlyBuildCosts = 0;
    public int monthlySum = 0;
}
