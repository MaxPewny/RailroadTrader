using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestUI : MonoBehaviour
{
    [Header("--- Guest Overview ---")]

    [SerializeField]
    private Slider SatisfactionSlider;
    [SerializeField]
    private Text SatisfactionPercentage;
    [SerializeField]
    private Text CommutersTotal;
    [SerializeField]
    private Text CommutersCur;
    [SerializeField]
    private Text TouristsTotal;
    [SerializeField]
    private Text TouristsCur; 
    [SerializeField]
    private Text BusinessTotal;
    [SerializeField]
    private Text BusinessCur;

    [SerializeField]
    private Text CommuterSpendings;
    [SerializeField]
    private Text TouristSpendings;
    [SerializeField]
    private Text BusinessSpendings;

    [Header("--- General Overview ---")]

    [SerializeField]
    private Text CommutersCurO;
    [SerializeField]
    private Text TouristsCurO;
    [SerializeField]
    private Text BusinessCurO;
    [SerializeField]
    private Slider SatisfactionSliderO;

    [Header("--- Tooltip ---")]

    [SerializeField]
    private Text CommutersCurTT;
    [SerializeField]
    private Text TouristsCurTT;
    [SerializeField]
    private Text BusinessCurTT;   
    [SerializeField]
    private Text VisiterTotalTT;


    public void WriteGuestOverview(PassengerStats commuter, PassengerStats tourist, PassengerStats business)
    {
        CommutersCur.text = commuter.curAmount.ToString();
        CommutersTotal.text = commuter.totalAmount.ToString();
        CommuterSpendings.text = commuter.totalSpendings.ToString();

        TouristsCur.text = tourist.curAmount.ToString();
        TouristsTotal.text = tourist.totalAmount.ToString();
        TouristSpendings.text = tourist.totalSpendings.ToString();

        BusinessCur.text = business.curAmount.ToString();
        BusinessTotal.text = business.totalAmount.ToString();
        BusinessSpendings.text = business.totalSpendings.ToString();
    }

    public void WriteGuestSmallOverview(PassengerStats commuter, PassengerStats tourist, PassengerStats business)
    {
        CommutersCurO.text = commuter.curAmount.ToString();
        TouristsCurO.text = tourist.curAmount.ToString();
        BusinessCurO.text = business.curAmount.ToString();
    }

    public void WriteSatisfactionSlider(int percentage)
    {
        SatisfactionSlider.value = percentage;
        SatisfactionSliderO.value = percentage;
        SatisfactionPercentage.text = percentage.ToString()+"%";
    }

    public void WriteGuestToolTip(PassengerStats commuter, PassengerStats tourist, PassengerStats business)
    {
        CommutersCurTT.text = commuter.curAmount.ToString();
        TouristsCurTT.text = tourist.curAmount.ToString();
        BusinessCurTT.text = business.curAmount.ToString();
        int total = commuter.curAmount + tourist.curAmount + business.curAmount;
        VisiterTotalTT.text = total.ToString();
    }
}
