using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationManagementMenu : MonoBehaviour
{
    [SerializeField]
    private FinanceController FC;
    [SerializeField]
    private FinanceMenu FM;

    private void Start()
    {
        TimeController.OnHourEnd += UpdateAllFinanceMenus;
        TimeController.OnDayEnd += UpdateAllFinanceMenus;
        TimeController.OnMonthEnd += UpdateAllFinanceMenus;
        TimeController.OnYearEnd += UpdateAllFinanceMenus;
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
    }

    public void OnFinanceOverviewOpened()
    {
        FinanceOverview[] FOs = FC.WholeOverview();
        FM.WriteCurrentOverview(FOs[0]);
        FM.WriteLastOverview(FOs[1]);
        FM.WriteTotalOverview(FOs[2]);
    }

    public void OnGuestOverview()
    {

    }

    public void OnBuildingOverview()
    {

    }
}
