using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class FinanceController : MonoBehaviour
{
    public class FinanceOverview
    {
        public int monthlyIncome = 0;
        public int monthlyUpkeep = 0;
        public int monthlyRevenue = 0;
        public int monthlyBuildCosts = 0;
        public int monthlySum = 0;
    }

    public int Currency { get { return Mathf.FloorToInt(_currency); } }
    private float _currency = 500.0f;
    //int is the number of the month, starting with 1 for the first obv
    private Dictionary<int, FinanceOverview> monthlyFinances = new Dictionary<int, FinanceOverview>();
    private int curFinanceID = 0;

    public static event System.Action<int> NewCurrencyValue = delegate { };

    private void Start()
    {
        IncreaseMonthCounterByOne();
        AddCurrency(100);
        TimeController.OnMonthEnd += IncreaseMonthCounterByOne;
    }

    private FinanceOverview GetFinances()
    {
        return monthlyFinances[curFinanceID];
    }

    public FinanceOverview GetFinances(int month)
    {
        return monthlyFinances[month];
    }

    public void IncreaseMonthCounterByOne()
    {
        curFinanceID += 1;
        monthlyFinances.Add(curFinanceID, new FinanceOverview());
    }

    public void AddCurrency(float valueToAdd)
    {
        _currency += valueToAdd;
        NewCurrencyValue(Currency);
    }

    public bool SubtractCurrency(float valueToSubtract)
    {
        if ( _currency >= valueToSubtract)
        {
            _currency += valueToSubtract;
            NewCurrencyValue(Currency);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void UpdateMonthlyIncome(int incomeGain)
    {
        FinanceOverview FO = GetFinances();
        FO.monthlyIncome += incomeGain;

    }

    public void UpdateMonthlyUpkeep(int curUpkeep)
    {
        FinanceOverview FO = GetFinances();
        FO.monthlyUpkeep = curUpkeep;
    }

    public void UpdateMonthlyRevenue(int curRevenue)
    {
        FinanceOverview FO = GetFinances();
        FO.monthlyRevenue += curRevenue;
    }

    public void UpdateMonthlyBuildCosts(int buildCostGain)
    {
        FinanceOverview FO = GetFinances();
        FO.monthlyBuildCosts += buildCostGain;
    }

    public void UpdateMonthlySum()
    {
        FinanceOverview FO = GetFinances();
        //calculate sum
    }

}
