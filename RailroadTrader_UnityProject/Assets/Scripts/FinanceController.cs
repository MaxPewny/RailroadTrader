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
    private int curMonth = 0;

    public static event System.Action<int> NewCurrencyValue = delegate { };

    private void Start()
    {
        UpdateMonthCounter(1);
        AddCurrency(100);
    }

    private FinanceOverview GetFinances()
    {
        return monthlyFinances[curMonth];
    }

    public FinanceOverview GetFinances(int month)
    {
        return monthlyFinances[month];
    }

    public void UpdateMonthCounter(int month)
    {
        curMonth = month;
        monthlyFinances.Add(curMonth, new FinanceOverview());
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
