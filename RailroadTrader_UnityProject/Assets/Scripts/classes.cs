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

[System.Serializable]
public class FinanceOverview
{
    public int monthlyIncome = 0;
    public int monthlyUpkeep = 0;
    public int monthlyRevenue = 0;
    public int monthlyBuildCosts = 0;
    public int monthlySum = 0;
}

[System.Serializable]
public class VisitorStats
{
    public BuildingType building;
    public Passanger type;
    public int maxCapacity;
    public int curAmount;
    public int earningGain;
    public int satisfactionGain;
}

public class PassengerStats
{
    public Passanger passenger;
    public int totalAmount = 0;
    public int curAmount = 0;
    public int totalSpendings = 0;

    public PassengerStats(Passanger pType)
    {
        passenger = pType;
    }
}

public class Cargo
{
    public Resource type;
    public float maxCapacity;
    public float curAmount;
}

