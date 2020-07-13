using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UI;

/// <summary>
/// Handles the GUI values of the status bar
/// e.g. Ingame Time, Game Speed, Money, current Visitor Count, Satisfaction
/// </summary>
public class StatusBar : MonoBehaviour
{
    public Text TimeTxt;
    public Text DateTxt;
    public Text MoneyTxt;
    public Text VisitorCountTxt;
    public Text SatisfactionTxt;

    [Tooltip("Order: paused, normal, fast, super fast")]
    public Button[] GameSpeedBtn;

    private string[] monthnames = new string[] { "ND", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

    // Start is called before the first frame update
    void Start()
    {
        TimeController.OnHourChange += UpdateTime;
        FinanceController.NewCurrencyValue += UpdateCurrency;
    }

    private void UpdateTime(IngameTime curTime)
    {
        string hour = curTime.hour < 10 ? "0" + curTime.hour + ":00" : curTime.hour + ":00";
        string day = curTime.day < 10 ? "0" + curTime.day : curTime.day.ToString();
        day += ". "+ monthnames[curTime.month];
        TimeTxt.text = hour;        
        DateTxt.text = day;
    }

    private void UpdateCurrency(int newAmount)
    {
        MoneyTxt.text = newAmount.ToString();
    }

    private void UpdateVisiterCount(int newAmount)
    {
        VisitorCountTxt.text = newAmount.ToString();
    }
}
