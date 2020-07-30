using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionController : MonoBehaviour
{
    [SerializeField]
    private int curSatisfaction;
    [SerializeField]
    private int maxSatisfaction;
    [SerializeField]
    private int guestsInStationToday;

    public static event System.Action<int> OnSatisfactionPercentageChange = delegate { };

    private void Start()
    {
        TimeController.OnDayEnd += ResetSatisfaction;
        GuestController.OnNewVisitorsArrived += RecalculateMaxSatisfaction;
        RecalculateMaxSatisfaction();
    }

    public void ShopBuild(SupplyStores store)
    {
        store.OnSatisfactionGain += IncreaseSatisfaction;
    }

    public void ShopDestroyed(SupplyStores store)
    {
        store.OnSatisfactionGain -= IncreaseSatisfaction;
    }

    private void ResetSatisfaction()
    {
        curSatisfaction = 0;
        guestsInStationToday = 0;
        RecalculateMaxSatisfaction();
    }

    private void RecalculateMaxSatisfaction(int arrivingGuests = 0)
    {
        guestsInStationToday += arrivingGuests;
        maxSatisfaction = guestsInStationToday > 0 ? guestsInStationToday / 3 : 0;
        OnSatisfactionPercentageChange(SatisfactionPercentage());
    }

    private void IncreaseSatisfaction(int amount)
    {
        curSatisfaction += amount;
        OnSatisfactionPercentageChange(SatisfactionPercentage());
    }

    private int SatisfactionPercentage()
    {
        if (maxSatisfaction <= 0 || curSatisfaction <= 0)
            return 0;
        else if (maxSatisfaction <= curSatisfaction)
            return 100;

        float percentage = (float)maxSatisfaction /((float)curSatisfaction / (float)maxSatisfaction);
        //print("float % = " + percentage);
        return Mathf.RoundToInt(percentage) >= 100 ? 100 : Mathf.RoundToInt(percentage);
    }
}
