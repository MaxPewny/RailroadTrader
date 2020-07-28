using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SatisfactionController : MonoBehaviour
{
    [SerializeField]
    private int curSatisfaction;
    [SerializeField]
    private int maxSatisfaction;

    public static event System.Action<int> OnSatisfactionPercentageChange = delegate { };

    private void Start()
    {
        TimeController.OnDayEnd += ResetSatisfaction;
        GuestController.OnCurVisiterCountChange += CalculateMaxSatisfaction;
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
    }

    private void CalculateMaxSatisfaction(int curGuestsInStation)
    {
        maxSatisfaction = curGuestsInStation / 3;
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
