using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManagementMenu : MonoBehaviour
{
    [SerializeField]
    private FinanceController FC;
    [SerializeField]
    private FinanceMenu FM;
    [SerializeField]
    private GuestController GC;
    [SerializeField]
    private GuestUI guestUI;
    [SerializeField]
    private BuildingOverview BO;
    [SerializeField]
    private BuildingManager BM;
    [SerializeField]
    private SatisfactionController SC;


    private void Start()
    {
        TimeController.OnHourEnd += UpdateAllFinanceMenus;
        TimeController.OnDayEnd += UpdateAllFinanceMenus;
        TimeController.OnMonthEnd += UpdateAllFinanceMenus;
        TimeController.OnYearEnd += UpdateAllFinanceMenus;
        SatisfactionController.OnSatisfactionPercentageChange += UpdateSatisfactionSliders;
    }

    private void UpdateSatisfactionSliders(int percentage)
    {
        guestUI.WriteSatisfactionSlider(percentage);
    }

    private void UpdateAllFinanceMenus()
    {
        OnFinanceToolTip();
        OnOverviewOpened();
        OnFinanceOverviewOpened();
    }

    public void OnFinanceToolTip()
    {
        FM.WriteToolTipFinances(FC.CurrentFO());
    }

    public void OnOverviewOpened()
    {
        FM.WriteSmallOverview(FC.CurrentFO());
        guestUI.WriteGuestSmallOverview(
            GC.Stats(Passanger.COMMUTER),
            GC.Stats(Passanger.TOURIST),
            GC.Stats(Passanger.BUSINESS));
        BO.WriteTotalBuildingCount(BM.AllBuildings());        
    }

    public void OnFinanceOverviewOpened()
    {
        FinanceOverview[] FOs = FC.WholeOverview();
        FM.WriteCurrentOverview(FOs[0]);
        FM.WriteLastOverview(FOs[1]);
        FM.WriteTotalOverview(FOs[2]);

        FM.WriteIncomeGraph(FC.YearOverview());
    }

    public void OnFinanceIncomeButton()
    {
        FinanceOverview[] FOs = FC.WholeOverview();
        FM.WriteCurrentOverview(FOs[0]);
        FM.WriteLastOverview(FOs[1]);
        FM.WriteTotalOverview(FOs[2]);

        FM.WriteIncomeGraph(FC.YearOverview());
    }

    public void OnFinanceUpkeepButton()
    {
        FinanceOverview[] FOs = FC.WholeOverview();
        FM.WriteCurrentOverview(FOs[0]);
        FM.WriteLastOverview(FOs[1]);
        FM.WriteTotalOverview(FOs[2]);

        FM.WriteUpkeepGraph(FC.YearOverview());
    }

    public void OnFinanceRevenueButton()
    {
        FinanceOverview[] FOs = FC.WholeOverview();
        FM.WriteCurrentOverview(FOs[0]);
        FM.WriteLastOverview(FOs[1]);
        FM.WriteTotalOverview(FOs[2]);

        FM.WriteRevenueGraph(FC.YearOverview());
    }

    public void OnFinanceBuildCostsButton()
    {
        FinanceOverview[] FOs = FC.WholeOverview();
        FM.WriteCurrentOverview(FOs[0]);
        FM.WriteLastOverview(FOs[1]);
        FM.WriteTotalOverview(FOs[2]);

        FM.WriteBuildGraph(FC.YearOverview());
    }

    public void OnGuestOverview()
    {
        guestUI.WriteGuestOverview(
            GC.Stats(Passanger.COMMUTER), 
            GC.Stats(Passanger.TOURIST), 
            GC.Stats(Passanger.BUSINESS));
    }

    public void OnGuestTooltip()
    {
        guestUI.WriteGuestToolTip(
            GC.Stats(Passanger.COMMUTER),
            GC.Stats(Passanger.TOURIST),
            GC.Stats(Passanger.BUSINESS));
    }

    public void OnBuildingOverview()
    {
        //BO.WriteTotalBuildingCount(BM.AllBuildings());
    }
}
