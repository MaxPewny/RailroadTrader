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
    //public BuildingType building;
    [UnityEngine.Tooltip("NPC type")]
    public Passanger type;
    [UnityEngine.Tooltip("Maximum of these type allowed in building")]
    public int maxCapacity;
    [UnityEngine.Tooltip("How much currency is earned if this NPC visits the store")]
    public int earningGain;
    [UnityEngine.Tooltip("How much satisfaction is earned if this NPC visits the store")]
    public int satisfactionGain;
    [UnityEngine.Tooltip("Currently in this building")]
    [ReadOnly]
    public int curAmount;
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

[System.Serializable]
public class BuildingRessource
{
    [UnityEngine.Tooltip("Resource type")]
    public Resource type;
    [UnityEngine.Tooltip("How much the building can hold at max")]
    public int maxCapacity;
    [UnityEngine.Tooltip("How much the building currently has")]
    [ReadOnly]
    public int curAmount;

    public BuildingRessource(Resource type, int maxCapacity)
    {
        this.type = type;
        this.maxCapacity = maxCapacity;
    }
}

//[System.Serializable]
//public class BuildOptionValues
//{
//    public UnityEngine.GameObject prefab;
//    public UnityEngine.Sprite picture;

//    public SupplystoreValues storeValues;
//    public PassengerTrackValues trackValues;
//    public GenericValues Values;
//}

[System.Serializable]
public class BuildOptionValues
{
    public UnityEngine.UI.Button Button;
    public UnityEngine.UI.Image ImageUI;
    public UnityEngine.GameObject Prefab;
    public UnityEngine.Sprite Preview;
    public BuildCostValues Values;
}

//public class Cargo
//{
//    public Resource type;
//    public int maxCapacity;
//    public int curAmount;

//    public Cargo(Resource type, )
//}

[System.Serializable]
public class MovementProbabilities
{
    public Passanger type { get; protected set; }

    [UnityEngine.Tooltip("Eating probability")]
    [UnityEngine.Range(0.0f, 1.0f)]
    public float eatProb = 0.75f;
    [UnityEngine.Tooltip("Drinking probability")]
    [UnityEngine.Range(0.0f, 1.0f)]
    public float drinkProb = 0.75f;

    [UnityEngine.Header("Specific shop probabilities")]
    [UnityEngine.Tooltip("Bakery visit probability")]
    [UnityEngine.Range(0.0f, 1.0f)]
    public float bakeryProb = 0.75f;
    [UnityEngine.Tooltip("Cafe visit probability")]
    [UnityEngine.Range(0.0f, 1.0f)]
    public float cafeProb = 0.75f;
    [UnityEngine.Tooltip("Pub visit probability")]
    [UnityEngine.Range(0.0f, 1.0f)]
    public float pubProb = 0.75f;
    [UnityEngine.Tooltip("Bar visit probability")]
    [UnityEngine.Range(0.0f, 1.0f)]
    public float barProb = 0.75f;
    [UnityEngine.Tooltip("Lounge visit probability")]
    [UnityEngine.Range(0.0f, 1.0f)]
    public float loungeProb = 0.75f;
    [UnityEngine.Tooltip("Restaurant visit probability")]
    [UnityEngine.Range(0.0f, 1.0f)]
    public float restaurantProb = 0.75f;
    [UnityEngine.Tooltip("5-Star Restaurant visit probability")]
    [UnityEngine.Range(0.0f, 1.0f)]
    public float highRestaurantProb = 0.75f;

    //public float shopProb;
    //public float bookstoreProb;
    //public float flowerstoreProb;
    //public float travelInfoProb;
    //public float travelAgencyProb;
    //public float hairSalonProb;
    //public float bankProb;

    public MovementProbabilities()
    {

    }

    public MovementProbabilities(Passanger type)
    {
        this.type = type;
    }
}
