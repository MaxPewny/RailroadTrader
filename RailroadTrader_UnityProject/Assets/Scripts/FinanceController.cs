using System.Collections.Generic;
using UnityEngine;

public class FinanceController : MonoBehaviour
{
    public GenericValues Values;
    public int Currency { get { return Mathf.FloorToInt(_currency); } }
    private int _currency;
    //int is the number of the month, starting with 1 for the first obv
    private Dictionary<int, FinanceOverview> monthlyFinances = new Dictionary<int, FinanceOverview>();
    private int curFinanceID = 0;

    public static event System.Action<int> OnCurrencyValueChange = delegate { };
    public static event System.Action<Passanger, int> OnPassangerSpendMoney = delegate { };

    private void Awake()
    {
        IncreaseMonthCounterByOne();
    }

    private void Start()
    {
        TimeController.OnMonthEnd += IncreaseMonthCounterByOne;
        BuildingManager.OnUpkeepDue += UpdateMonthlyUpkeep;
        BuildingManager.OnUpkeepDue += SubtractUpkeep;
        ObjectPlacement.OnBuildingCanceled += ReturnBuildingCosts;
    }

    public FinanceOverview CurrentFO()
    {
        return GetFinances();
    }

    public FinanceOverview[] WholeOverview()
    {
        FinanceOverview[] FOs = new FinanceOverview[3];
        UpdateMonthlySum();
        FOs[0] = GetFinances();
        FOs[1] = GetFinances(curFinanceID - 1);
        FOs[2] = TotalFinances();
        return FOs;
    }    

    //Gets the current FinanceOverview for THIS month
    private FinanceOverview GetFinances()
    {
        return monthlyFinances[curFinanceID];
    }

    private FinanceOverview GetFinances(int month)
    {
        if (month <= curFinanceID && month > 0)
            return monthlyFinances[month];
        else
        {
            FinanceOverview fo = new FinanceOverview();
            return fo;
        }
    }

    private FinanceOverview TotalFinances()
    {
        FinanceOverview newFO = new FinanceOverview();
        foreach(KeyValuePair<int, FinanceOverview> fo in monthlyFinances)
        {
            newFO.monthlyIncome += fo.Value.monthlyIncome;
            newFO.monthlyUpkeep += fo.Value.monthlyUpkeep;
            newFO.monthlyRevenue += fo.Value.monthlyRevenue;
            newFO.monthlyBuildCosts += fo.Value.monthlyBuildCosts;
            newFO.monthlySum = CalculateMonthlySum(curFinanceID);
            //newFO.monthlySum += fo.Value.monthlySum;
        }
        return newFO;
    }

    public void IncreaseMonthCounterByOne()
    {
        curFinanceID += 1;
        monthlyFinances.Add(curFinanceID, new FinanceOverview());
        UpdateMonthlySum();
    }

    public void AddCurrency(int valueToAdd)
    {
        _currency += valueToAdd;
        OnCurrencyValueChange(Currency);
        //print("new currency: " + Currency);
    }
    public void AddShopIncome(Passanger pType, int valueToAdd)
    {
        AddCurrency(valueToAdd);
        UpdateMonthlyIncome(valueToAdd);
        OnPassangerSpendMoney(pType, valueToAdd);
    }

    public void SubtractUpkeep(int upKeep)
    {
        _currency -= upKeep;
        OnCurrencyValueChange(Currency);
    }

    public bool PayBuildingCost(int valueToSubtract)
    {
        if ( _currency >= valueToSubtract)
        {
            _currency -= valueToSubtract;
            UpdateMonthlyBuildCosts(valueToSubtract);
            OnCurrencyValueChange(Currency);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ReturnBuildingCosts(int valueToReturn)
    {
        _currency += valueToReturn;
        UpdateMonthlyBuildCosts(-valueToReturn);
        OnCurrencyValueChange(Currency);
    }

    public void UpdateMonthlyIncome(int incomeGain)
    {
        FinanceOverview FO = GetFinances();
        FO.monthlyIncome += incomeGain;
        UpdateMonthlyRevenue();
        UpdateMonthlySum();
    }

    public void UpdateMonthlyUpkeep(int upkeep)
    {
        FinanceOverview FO = GetFinances();
        FO.monthlyUpkeep -= upkeep;
        UpdateMonthlyRevenue();
        UpdateMonthlySum();
    }

    public void UpdateMonthlyRevenue()
    {
        FinanceOverview FO = GetFinances();
        FO.monthlyRevenue = FO.monthlyIncome + FO.monthlyUpkeep;
        UpdateMonthlySum();
    }

    public void UpdateMonthlyBuildCosts(int buildCost)
    {
        FinanceOverview FO = GetFinances();
        FO.monthlyBuildCosts -= buildCost;
        UpdateMonthlySum();
    }

    public void UpdateMonthlySum()
    {
        FinanceOverview FO = GetFinances();
        FO.monthlySum = CalculateMonthlySum(curFinanceID);
    }

    public int CalculateMonthlySum(int financeID)
    {
        FinanceOverview FO = GetFinances(financeID);
        return FO.monthlyRevenue + FO.monthlyBuildCosts;            
        //FO.monthlyBuildCosts + FO.monthlyIncome + FO.monthlyRevenue + FO.monthlyUpkeep;
    }
}
