using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using UnityEngine.UI;

public class FinanceMenu : MonoBehaviour
{
    [Header("Finance Overview: Small")]
    public Text ShopRev;
    public Text Upkeep;
    public Text Revenue;
    public Text BuildCosts;
    public Text Sum;

    [Header("Finance Overview: Last Month")]
    public Text ShopRevLast;
    public Text UpkeepLast;
    public Text RevenueLast;
    public Text BuildCostsLast;
    public Text SumLast;

    [Header("Finance Overview: Current Month")]
    public Text ShopRevCur;
    public Text UpkeepCur;
    public Text RevenueCur;
    public Text BuildCostsCur;
    public Text SumCur;

    [Header("Finance Overview: Total since start")]
    public Text ShopRevTotal;
    public Text UpkeepTotal;
    public Text RevenueTotal;
    public Text BuildCostsTotal;
    public Text SumTotal;

    public void WriteSmallOverview(FinanceOverview FO)
    {
        ShopRev.text = FO.monthlyIncome.ToString() + "€";
        Upkeep.text = FO.monthlyUpkeep.ToString() + "€";
        Revenue.text = FO.monthlyRevenue.ToString() + "€";
        BuildCosts.text = FO.monthlyBuildCosts.ToString() + "€";
        Sum.text = FO.monthlySum.ToString() + "€";
    }

    public void WriteCurrentOverview(FinanceOverview FO)
    {
        ShopRevCur.text = FO.monthlyIncome.ToString() + "€";
        UpkeepCur.text = FO.monthlyUpkeep.ToString() + "€";
        RevenueCur.text = FO.monthlyRevenue.ToString() + "€";
        BuildCostsCur.text = FO.monthlyBuildCosts.ToString() + "€";
        SumCur.text = FO.monthlySum.ToString() + "€";
    }   
    
    public void WriteLastOverview(FinanceOverview FO)
    {
        ShopRevLast.text = FO.monthlyIncome.ToString() + "€";
        UpkeepLast.text = FO.monthlyUpkeep.ToString() + "€";
        RevenueLast.text = FO.monthlyRevenue.ToString() + "€";
        BuildCostsLast.text = FO.monthlyBuildCosts.ToString() + "€";
        SumLast.text = FO.monthlySum.ToString() + "€";
    }

    public void WriteTotalOverview(FinanceOverview FO)
    {
        ShopRevTotal.text = FO.monthlyIncome.ToString() + "€";
        UpkeepTotal.text = FO.monthlyUpkeep.ToString() + "€";
        RevenueTotal.text = FO.monthlyRevenue.ToString() + "€";
        BuildCostsTotal.text = FO.monthlyBuildCosts.ToString() + "€";
        SumTotal.text = FO.monthlySum.ToString() + "€";
    }
}
