using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestUI : MonoBehaviour
{
    [Header("--- Finance Overview ---")]

    [SerializeField]
    private Text SatisfactionPercent;
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

    [Header("--- Tooltip ---")]

    [SerializeField]
    private Text CommutersCurTT;
    [SerializeField]
    private Text TouristsCurTT;
    [SerializeField]
    private Text BusinessCurTT;


    public void WriteGuestOverview(PassengerStats commuter, PassengerStats tourist, PassengerStats business)
    {
        //SatisfactionPercent

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

    public void WriteGuestToolTip(PassengerStats commuter, PassengerStats tourist, PassengerStats business)
    {
        CommutersCurTT.text = commuter.curAmount.ToString();
        TouristsCurTT.text = tourist.curAmount.ToString();
        BusinessCurTT.text = business.curAmount.ToString();
    }

}
