using System.Collections.Generic;
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

    [Header("Finance Overview: Tooltip")]
    public Text ShopRevTT;
    public Text UpkeepTT;
    public Text SumTT;

    [Header("Finance Overview: Income Graphs")]
    public List<Slider> IncomeGraphs = new List<Slider>();

    [Header("Finance Overview: Upkeep Graphs")]
    public List<Slider> UpkeepGraphs = new List<Slider>();

    [Header("Finance Overview: Revenue Graphs")]
    public List<Slider> RevenueGraphs = new List<Slider>();

    [Header("Finance Overview: Buildcost Graphs")]
    public List<Slider> BuildGraphs = new List<Slider>();

    public void WriteToolTipFinances(FinanceOverview FO)
    {
        ShopRevTT.text = FO.monthlyIncome.ToString() + " €";
        UpkeepTT.text = FO.monthlyUpkeep.ToString() + " €";
        int profit = FO.monthlyIncome + FO.monthlyUpkeep;
        SumTT.text = profit + " €";
    }

    public void WriteSmallOverview(FinanceOverview FO)
    {
        ShopRev.text = FO.monthlyIncome.ToString() + " €";
        Upkeep.text = FO.monthlyUpkeep.ToString() + " €";
        Revenue.text = FO.monthlyRevenue.ToString() + " €";
        BuildCosts.text = FO.monthlyBuildCosts.ToString() + " €";
        Sum.text = FO.monthlySum.ToString() + " €";
    }

    public void WriteCurrentOverview(FinanceOverview FO)
    {
        ShopRevCur.text = FO.monthlyIncome.ToString() + " €";
        UpkeepCur.text = FO.monthlyUpkeep.ToString() + " €";
        RevenueCur.text = FO.monthlyRevenue.ToString() + " €";
        BuildCostsCur.text = FO.monthlyBuildCosts.ToString() + " €";
        SumCur.text = FO.monthlySum.ToString() + " €";
    }   
    
    public void WriteLastOverview(FinanceOverview FO)
    {
        ShopRevLast.text = FO.monthlyIncome.ToString() + " €";
        UpkeepLast.text = FO.monthlyUpkeep.ToString() + " €";
        RevenueLast.text = FO.monthlyRevenue.ToString() + " €";
        BuildCostsLast.text = FO.monthlyBuildCosts.ToString() + " €";
        SumLast.text = FO.monthlySum.ToString() + " €";
    }

    public void WriteTotalOverview(FinanceOverview FO)
    {
        ShopRevTotal.text = FO.monthlyIncome.ToString() + " €";
        UpkeepTotal.text = FO.monthlyUpkeep.ToString() + " €";
        RevenueTotal.text = FO.monthlyRevenue.ToString() + " €";
        BuildCostsTotal.text = FO.monthlyBuildCosts.ToString() + " €";
        SumTotal.text = FO.monthlySum.ToString() + " €";
    }

    public void WriteIncomeGraph(List<FinanceOverview> FOs)
    {
        for (int i = 0; i < 12; i++)
        {
            if (i >= FOs.Count)
            {
                IncomeGraphs[i].value = 0;
            }
            else 
            {
                if(IncomeGraphs[i].maxValue < FOs[i].monthlyIncome) 
                {
                    IncomeGraphs[i].value = IncomeGraphs[i].maxValue;
                }
                else
                {
                    IncomeGraphs[i].value = FOs[i].monthlyIncome;
                }
            }
        }
    }

    public void WriteUpkeepGraph(List<FinanceOverview> FOs)
    {
        for (int i = 0; i < 12; i++)
        {
            if (i >= FOs.Count)
            {
                UpkeepGraphs[i].value = 0;
            }
            else
            {
                if (UpkeepGraphs[i].maxValue < -FOs[i].monthlyUpkeep)
                {
                    UpkeepGraphs[i].value = UpkeepGraphs[i].maxValue;
                }
                else
                {
                    UpkeepGraphs[i].value = -FOs[i].monthlyUpkeep;
                }
            }
        }
    }

    public void WriteRevenueGraph(List<FinanceOverview> FOs)
    {
        for (int i = 0; i < 12; i++)
        {
            if (i >= FOs.Count)
            {
                RevenueGraphs[i].value = 0;
            }
            else
            {
                if (RevenueGraphs[i].maxValue < FOs[i].monthlyRevenue)
                {
                    RevenueGraphs[i].value = RevenueGraphs[i].maxValue;
                }
                else
                {
                    RevenueGraphs[i].value = FOs[i].monthlyRevenue;
                }
            }
        }
    }

    public void WriteBuildGraph(List<FinanceOverview> FOs)
    {
        for (int i = 0; i < 12; i++)
        {
            if (i >= FOs.Count)
            {
                BuildGraphs[i].value = 0;
            }
            else
            {
                if (BuildGraphs[i].maxValue < -FOs[i].monthlyBuildCosts)
                {
                    BuildGraphs[i].value = BuildGraphs[i].maxValue;
                }
                else
                {
                    BuildGraphs[i].value = -FOs[i].monthlyBuildCosts;
                }
            }
        }
    }


}
